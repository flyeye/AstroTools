//
//
//  FConnection.cs
//  2015.03.10  
//  Impementation of the FConnection  protocol on C#
//  Author: Alexey V. Popov, 9141866@gmail.com
//  
//
//

#define FConnection

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using ASCOM;
using RingBuffer;

namespace FConnection
{
#region Protocol definition

    public enum FBC {
//
        FBC_FIRST_BYTE = 168,
        FBC_LAST_BYTE1 = 13,
        FBC_LAST_BYTE2 = 10,
// General device commands        
        FBC_DEBUG_MSG = 250,           // Debug message 
        FBC_DEBUG = 251,               // Debug mode ON/OFF
        FBC_RCP = 252,                 // Remote Control Panel ON/OFF
        FBC_GET_FIRMWARE_VERSION = 253,// Firmware version 
        FBC_HANDSHAKE = 254,           // Handshake
        FBC_PING = 255,                 // ping
// RTC commands
        FBC_RTC_GET = 90,              //  Get device RTC current datetime 
        FBC_RTC_SET = 91,              //  Set device RTC datetime
        FBC_RTC_TEMP = 92,             //  Get device RTC temperature
//  Ground humidity sensor commands
        FBC_GH_GET_VALUE = 81,          //  Get humidity value from certain sensor;
        NONE = 0
    };

    public enum FocuserCmd {
        FOCUSER_STOP = 210,             // остановиться
        FOCUSER_STEP_RIGHT = 211,       // шаг вправо
        FOCUSER_STEP_LEFT = 209,        // шаг влево
        FOCUSER_ROLLING = 202,          // нотификация о вращении с заданной скоростью
        FOCUSER_RELEASE = 220,          // снять напряжение с двигателя
        FOCUSER_POWER_ON = 223,         // Принудительно подать питание на мотор
        FOCUSER_GET_POSITION = 225,     // получить текущее положение
        FOCUSER_SET_POSITION = 229,     // установить текущее положение
        FOCUSER_RANGE_CHECK = 215,        // вкл/выкл проверку выхода за границы по положению 
        FOCUSER_GET_SPEED = 240,        // получить текущую скорость
        FOCUSER_SET_SPEED = 241,        // установить текущую скорость
        FOCUSER_ROLL_RIGHT = 212,       // вращение вправо с заданной скоростью
        FOCUSER_ROLL_LEFT = 208,        // вращение влево с заданной скорос
        FOCUSER_GET_RELAESE_TIME = 221,  // таймаут автоматического снятия напряжение с двигател, 0 - никогда
        FOCUSER_SET_RELAESE_TIME = 222,  // таймаут автоматического снятия напряжение с двигател, 0 - никогда
        FOCUSER_GET_MIN_SPEED_DELAY = 239,// получить максимальную скорость
        FOCUSER_GET_MAX_SPEED_DELAY = 238,    // получить минимальную скорость
        FOCUSER_SET_MIN_POSITION = 227, // установить максимальное положение
        FOCUSER_SET_MAX_POSITION = 228, // установить максимальное положение
        FOCUSER_GET_MIN_POSITION = 233, // установить максимальное положение
        FOCUSER_GET_MAX_POSITION = 224, // установить максимальное положение
        FOCUSER_GO_TO_POSITION = 226,   // вращаться до заданной позиции
        FOCUSER_SET_MICROSTEP = 231,    // установить режим микрошага
        FOCUSER_GET_MICROSTEP = 230,    // получить режим микрошага        
        NONE = 0
     };
   
     public enum MounterCmd {
        MOUNTER_RANGE_CHECK = 215,
        MOUNTER_STEP = 211,
        MOUNTER_ROLL = 212,
        MOUNTER_GET_POWER = 220,
        MOUNTER_SET_POWER = 223,       
        MOUNTER_STOP = 210,

        MOUNTER_GET_NAV_SPEED = 240,
        MOUNTER_SET_NAV_SPEED = 241,
        MOUNTER_GET_DAILY_SPEED = 242,
        MOUNTER_SET_DAILY_SPEED = 243,

        MOUNTER_GET_NAV_MAX_SPEED = 239,
        MOUNTER_GET_NAV_MIN_SPEED = 238,
        MOUNTER_GET_DAILY_MAX_SPEED = 237,
        MOUNTER_GET_DAILY_MIN_SPEED = 236,

        MOUNTER_SET_NAV_MAX_SPEED = 235,
        MOUNTER_SET_NAV_MIN_SPEED = 234,
        MOUNTER_SET_DAILY_MAX_SPEED = 233,
        MOUNTER_SET_DAILY_MIN_SPEED = 232,

        FOCUSER_SET_MICROSTEP = 231,
        FOCUSER_GET_MICROSTEP = 230,

        FOCUSER_GET_POSITION = 225,
        FOCUSER_GO_TO_POSITION = 226,
        FOCUSER_SET_MIN_POSITION = 227,     //  Не реализовано!
        FOCUSER_SET_MAX_POSITION = 228,
        FOCUSER_SET_POSITION = 229,
        FOCUSER_GET_MIN_POSITION = 223,
        FOCUSER_GET_MAX_POSITION = 224,

        MOUNTER_SET_DROTATION = 206,
        MOUNTER_GET_DROTATION = 213
     };

    public enum FDEV {
        FD_UNKNOWN = 100,                // Unknown device
        FD_FOCUSER = 101,                // Focuser
        FD_MOUNTER = 102,                // Mounter
        FD_GROWER = 103                 // Grower
    };


    public enum MOTOR 
    {
        NONE = 0,
        RA = 1,
        DE = 2
    };

    public enum STEPDIR 
    {
        STEP_BACKWARD = -1,
        STEP_FORWARD = 1,
        STEP_HOLD = 0
    };

   
    public enum MODES
    {
        DAILY = 1,
        NAVIGATION = 2
    };

#endregion

#region Helpfull routings    
    [StructLayout(LayoutKind.Explicit)]
    public struct TInt
    {
        [FieldOffset(0)]  public byte b1;
        [FieldOffset(1)]  public byte b2;
        [FieldOffset(2)]  public byte b3;
        [FieldOffset(3)]  public byte b4;      
        [FieldOffset(0)]  public short w1;
        [FieldOffset(2)]  public short w2;
        [FieldOffset(0)]  public int i;
        [FieldOffset(0)]  public float f;
    }


#endregion

#region TFBasic class

    public class TFBasicCom
    {                
        volatile protected bool fIsConnected = false;
        protected System.IO.Ports.SerialPort fComPort;
        protected Thread fReadThread;
        volatile protected Form fParent;
        public Form Parent {
            get { return fParent;  }
        }
        volatile protected bool fIsASCOM;
        private ByteBuffer fBuffer;

        protected string fDeviceDescription;
        public string DeviceDescription
        {
            get { return fDeviceDescription;  }
        }

        volatile protected String fDeviceVersion, fUnknownMessage;
        volatile public ConcurrentQueue<String> fDeviceDbgMsgArray;
        volatile protected FDEV fDeviceType;
        volatile protected bool fIsDebug = false;
        volatile protected bool fIsRCP_ON = true;  //  блокировка пульта ДУ, false - заблокировано

//  RTC
        public TFDeviceRTC RTC;

// -------

        public String DeviceVersion
        {
            get {return fDeviceVersion; }
        }
        public FDEV DeviceType
        {
            get { return fDeviceType; }
        }
        public bool IsRCP_On
        {
            get { return fIsRCP_ON;}
            set {
                SendData((byte)FBC.FBC_RCP, Convert.ToByte(value));
            }
        }
        public bool IsDebugOn
        {
            get { return fIsDebug; }
            set { SendData((byte)FBC.FBC_DEBUG, Convert.ToByte(value)); }
        }
        public bool GetDebugMsg(out string str)
        {        
                str = "";
                return fDeviceDbgMsgArray.TryDequeue(out str);                    
        }
        public void Ping()
        { SendData((byte)FBC.FBC_PING); }

        public event EventHandler OnPingEvent = null;
        public delegate void OnPingHandler();

        public event EventHandler OnHandshakeEvent = null;
        public delegate void OnHandshakeHandler();

        public event EventHandler OnFirmwareEvent = null;
        public delegate void OnFirmwareHandler();

        public event EventHandler OnRPCEvent = null;
        public delegate void OnRPC();

        public event EventHandler OnDebugEvent = null;
        public delegate void OnDebugHandler();

        public event EventHandler OnDebugMsgEvent = null;
        public delegate void OnDebugMsgHandler();


        public TFBasicCom(bool isASCOM = false, Form parent=null)
        {
            fDeviceVersion = "";
            fDeviceType = FDEV.FD_UNKNOWN;
            fIsASCOM = isASCOM;
            fParent = parent;
            fDeviceDbgMsgArray = new ConcurrentQueue<string>();       

            //fReadThread = new Thread(GetSerialData);
            fComPort = new SerialPort();
            fBuffer = new ByteBuffer();
            RTC = new TFDeviceRTC(this);
        }

        public bool IsConnected
        {
            get {
                return fIsConnected;
            }
        }
                                
        public void Connect(String port, int baudrate)
        {
            if (fIsConnected)
                Disconnect();
            try
            {
                lock (fComPort)
                {
                    fComPort.PortName = port;
                    fComPort.BaudRate = baudrate;
                    fComPort.DataBits = 8;
                    fComPort.Parity = Parity.None;
                    fComPort.StopBits = StopBits.One;
                    fComPort.ReadTimeout = 0;

                    fComPort.DataReceived += new SerialDataReceivedEventHandler(GetSerialData);
                    fComPort.Open();
                }
                fIsConnected = fComPort.IsOpen;
                if (fIsConnected)
                {
                    SendData((byte)FBC.FBC_HANDSHAKE);
                    // послать handshake
                }
            }
            catch (System.IO.IOException) {
                string s = "Invalid port state: " + port;
                if (fIsASCOM)                
                    throw new ASCOM.NotConnectedException(s);                
                else
                    throw new System.IO.IOException(s);
            }
            catch (System.InvalidOperationException) {
                string s = "Port already opened: " + port;
                if (fIsASCOM)                
                    throw new ASCOM.NotConnectedException(s);
                else
                    throw new System.InvalidOperationException(s);
            }
            catch (UnauthorizedAccessException) {
                string s = "Access denied to serial port: " + port;
                if (fIsASCOM)
                    throw new ASCOM.NotConnectedException(s);                
                else
                    throw new UnauthorizedAccessException(s);
            }
        }

        public void Disconnect()
        {
            fIsConnected = false;
            lock (fComPort)
            {
                fComPort.Close();
            }
        }


        public void SendData(byte cmd) //  отправка однобайтной команды 
        {

            if (!fIsConnected)
                return; 
            byte[] buf = { (byte)FBC.FBC_FIRST_BYTE, cmd, (byte)FBC.FBC_LAST_BYTE1, (byte)FBC.FBC_LAST_BYTE2 };
            lock (fComPort)
            {
                fComPort.Write(buf, 0, buf.Length);
            }
        }

        public void SendData(byte cmd, byte data) //  отправка однобайтной команды c одним однобайтным параметром
        {
            if (!fIsConnected)
                return;
            byte[] buf = { (byte)FBC.FBC_FIRST_BYTE, cmd, data, (byte)FBC.FBC_LAST_BYTE1, (byte)FBC.FBC_LAST_BYTE2 };
            lock (fComPort)
            {
                fComPort.Write(buf, 0, buf.Length);
            }
        }

        public void SendData(byte cmd, byte  par, byte data) //  отправка однобайтной команды c одним однобайтным параметром
        {
            if (!fIsConnected)
                return;
            byte[] buf = { (byte)FBC.FBC_FIRST_BYTE, cmd, par, data, (byte)FBC.FBC_LAST_BYTE1, (byte)FBC.FBC_LAST_BYTE2 };
            lock (fComPort)
            {
                fComPort.Write(buf, 0, buf.Length);
            }
        }

        public void SendData(byte cmd, byte par, int data) //  отправка однобайтной команды c одним однобайтным параметром
        {
            if (!fIsConnected)
                return;
            TInt i = new TInt();
            i.i = data;
            byte[] buf = { (byte)FBC.FBC_FIRST_BYTE, cmd, par, i.b1, i.b2, i.b3, i.b4, (byte)FBC.FBC_LAST_BYTE1, (byte)FBC.FBC_LAST_BYTE2 };
            lock (fComPort)
            {
                fComPort.Write(buf, 0, buf.Length);
            }
        }

        public void SendData(byte cmd, string data) //  отправка однобайтной команды c одним строковым параметром
        {
            if (!fIsConnected)
                return;
            byte[] b2 = System.Text.Encoding.ASCII.GetBytes(data);
            int cmd_length = 1 + 1 + b2.GetLength(0) + 2;
            byte[] buf = new byte[cmd_length];
            buf[0] = (byte)FBC.FBC_FIRST_BYTE;
            buf[1] = cmd;
            int i;
            for (i=0; i<data.Length; i++)
                buf[i+2] = b2[i];
            buf[i + 2] = (byte)FBC.FBC_LAST_BYTE1;
            buf[i + 3] = (byte)FBC.FBC_LAST_BYTE2;
            lock (fComPort)
            {
                fComPort.Write(buf, 0, cmd_length);
            }
        }

        public void SendData(byte cmd, byte par, string data) //  отправка однобайтной команды c одним строковым параметром
        {
            if (!fIsConnected)
                return;
            byte[] b2 = System.Text.Encoding.ASCII.GetBytes(data);
            int cmd_length = 1 + 1 + 1 + b2.GetLength(0) + 2;
            byte[] buf = new byte[cmd_length];
            buf[0] = (byte)FBC.FBC_FIRST_BYTE;
            buf[1] = cmd;
            buf[2] = par;
            int i;
            for (i = 0; i < data.Length; i++)
                buf[i + 3] = b2[i];
            buf[i + 3] = (byte)FBC.FBC_LAST_BYTE1;
            buf[i + 4] = (byte)FBC.FBC_LAST_BYTE2;
            lock (fComPort)
            {
                fComPort.Write(buf, 0, cmd_length);
            }
        }

        public void SendData(byte cmd, byte[] data, int len)
        {
            int cmd_length = 1 + 1 + len + 2;
            byte[] buf = new byte[cmd_length];
            buf[0] = (byte)FBC.FBC_FIRST_BYTE;
            buf[1] = cmd;
            int i;
            for (i = 0; i < len; i++)
                buf[i + 2] = data[i];
            buf[i + 2] = (byte)FBC.FBC_LAST_BYTE1;
            buf[i + 3] = (byte)FBC.FBC_LAST_BYTE2;
            lock (fComPort)
            {
                fComPort.Write(buf, 0, cmd_length);
            }
        }

        private void GetSerialData(object sender, SerialDataReceivedEventArgs e)
        {
            
            if ((fIsConnected) && (fComPort.BytesToRead>0))
            {
                try
                {
                    byte[] buf;
                    int read = 0;
                    lock (fComPort)
                    {
                        read = fComPort.BytesToRead;
                        buf = new byte[read];
                        read = fComPort.Read(buf, 0, read);                        
                    }
                    fBuffer.Put(buf, read);

                    int cmdlen = 0;
                    do
                    {
                        cmdlen = fBuffer.ExtractCommand(168, 13, 10, ref buf);
                        if (cmdlen>0)
                            ParseCommand(buf, cmdlen);

                    } while (cmdlen > 0);
                }
                catch (TimeoutException) { }
            }
        }        
    

        protected virtual void OnHandshakeRouting()
        {
            SendData((byte)FBC.FBC_GET_FIRMWARE_VERSION);
            SendData((byte)FBC.FBC_RCP, 1);
            SendData((byte)FBC.FBC_DEBUG, 0);

            RTC.OnHandshake();
        }


        public string GetStringFormArray(byte[] buf, int len, int index = 0)
        {
            byte[] t = new byte[len];
            for (int i = 0; i < len; i++)
                t[i] = buf[index + i];
            return System.Text.Encoding.ASCII.GetString(t);
        }

        public int GetIntFormArray(byte[] buf, int index = 0)
        {
            TInt i = new TInt();
            i.b1 = buf[index];
            i.b2 = buf[index + 1];
            i.b3 = buf[index + 2];
            i.b4 = buf[index + 3];
            return i.i;
        }

        public float GetFloatFormArray(byte[] buf, int index = 0)
        {
            TInt i = new TInt();
            i.b1 = buf[index];
            i.b2 = buf[index + 1];
            i.b3 = buf[index + 2];
            i.b4 = buf[index + 3];
            return i.f;
        }

        protected virtual bool ParseCommand(byte[] cmd, int cmdlen)
        {
            if (cmd.Length == 0)
                return false;

            if (RTC.OnCommand(cmd, cmdlen))
                return true;

            switch ((byte)cmd[0])
            {
                case (byte)FBC.FBC_HANDSHAKE:

                    fDeviceType = (FDEV)cmd[1];
                    OnHandshakeRouting();               
                    Thread.Sleep(10);
                    
                    if ((OnHandshakeEvent != null) && (fParent != null))
                          fParent.BeginInvoke(OnHandshakeEvent);
                    return true;

                case (byte)FBC.FBC_PING:
                    if ((OnPingEvent != null) && (fParent != null))
                       fParent.BeginInvoke(OnPingEvent);                    
                    return true;

                case (byte)FBC.FBC_GET_FIRMWARE_VERSION:
                    fDeviceVersion = GetStringFormArray(cmd, cmdlen - 1, 1); 
                    if ((OnFirmwareEvent != null) && (fParent != null))
                        fParent.BeginInvoke(OnFirmwareEvent);
                    return true;

                case (byte)FBC.FBC_RCP:
                    fIsRCP_ON = Convert.ToBoolean(cmd[1]);
                    if ((OnRPCEvent != null) && (fParent != null))
                        fParent.BeginInvoke(OnRPCEvent);
                    return true;

                case (byte)FBC.FBC_DEBUG:
                    fIsDebug = Convert.ToBoolean(cmd[1]);
                    if ((OnDebugEvent != null) && (fParent != null))
                        fParent.BeginInvoke(OnDebugEvent);
                    return true;

                case (byte)FBC.FBC_DEBUG_MSG:
                    {
                        fDeviceDbgMsgArray.Enqueue(GetStringFormArray(cmd, cmdlen-1, 1));
                        if ((OnDebugMsgEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnDebugMsgEvent);
                        return true;
                    };                

            }

            return false;
        }
        
    };

#endregion

#region TFDeviceComponent
    public class TFDeviceComponent
    {
        protected TFBasicCom fParent;
        public TFDeviceComponent(TFBasicCom parent)
        {
            fParent = parent;
        }

        public virtual bool OnCommand(byte[] cmd, int cmdlen)
        {
            return false;
        }

        public virtual void OnHandshake() { ; }
    }
#endregion

}