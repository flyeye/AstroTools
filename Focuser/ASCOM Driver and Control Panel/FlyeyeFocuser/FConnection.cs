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
        FBC_RTC_GET = 90,              //  Get device current datetime 
        FBC_RTC_SET = 91,              //  Set device datetime
        FBC_RTC_TEMP = 92,
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
   

    public enum FDEV {
        FD_UNKNOWN = 100,                // Unknown device
        FD_FOCUSER = 101,                // Focuser
        FD_MOUNTER = 102,                // Mounter
        FD_GROWER = 103                 // Grower
    };


    public class TFBasicCom
    {                
        volatile protected bool fIsConnected = false;
        protected System.IO.Ports.SerialPort fComPort;
        protected Thread fReadThread;
        volatile protected Form fParent;
        volatile protected bool fIsASCOM;
        private ByteBuffer fBuffer;

        volatile protected String fDeviceVersion, fUnknownMessage;
        volatile public ConcurrentQueue<String> fDeviceDbgMsgArray;
        volatile protected FDEV fDeviceType;
        volatile protected bool fIsDebug = false;
        volatile protected bool fIsRCP_ON = true;  //  блокировка пульта ДУ, false - заблокировано

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

//-------------        

        protected long fPosition = 0, fMinPosition = 0, fMaxPosition = 1;
        public long Position
        {
            get { return fPosition; }
            set { SendData((byte)FocuserCmd.FOCUSER_SET_POSITION, Convert.ToString(value)); }
        }
        public long MaxPosition
        {
            get { return fMaxPosition; }
            set { SendData((byte)FocuserCmd.FOCUSER_SET_MAX_POSITION, Convert.ToString(value)); }

        }
        public long MinPosition
        {
            get { return fMinPosition; }
            set { SendData((byte)FocuserCmd.FOCUSER_SET_MIN_POSITION, Convert.ToString(value)); }
        }
        volatile protected bool fIsRangeCheck = false;
        public bool IsRangeCheck
        {
            get { return fIsRangeCheck; }
            set { SendData((byte)FocuserCmd.FOCUSER_RANGE_CHECK, Convert.ToByte(value)); }
        }

        volatile protected bool fIsPowerOn = false;
        public bool Power
        {
            get { return fIsPowerOn; }
            set 
            {
                if (value)
                    SendData((byte)FocuserCmd.FOCUSER_POWER_ON);
                else
                    SendData((byte)FocuserCmd.FOCUSER_RELEASE);
            }
        }
        volatile protected int fReleaseTime = 0;
        public int ReleaseTime
        {
            set { SendData((byte)FocuserCmd.FOCUSER_SET_RELAESE_TIME, Convert.ToString(value)); }
            get { return fReleaseTime; }
        }

        protected long fStepDelay=100, fMaxStepDelay=10, fMinStepDelay=1000000;  // microseconds
        public long StepDelay
        {
            get { return fStepDelay; }
            set { SendData((byte)FocuserCmd.FOCUSER_SET_SPEED, Convert.ToString(value));}
        }
        public long MaxStepDelay
        {
            get { return fMaxStepDelay; }
            set { ; }
        }
        public long MinStepDelay
        {
            get { return fMinStepDelay; }
            set { ; }
        }
        public double CurrentSpeed
        {
            get { return (double)1000000 / fStepDelay; }
        }
        public double MaxSpeed
        {
            get { return (double)1000000 / fMaxStepDelay; }
        }
        public double MinSpeed
        {
            get { return (double)1000000 / fMinStepDelay; }
        }

        protected byte fMicroStepMode = 0;
        public byte MicroStep
        {
            get { return fMicroStepMode; }
            set { SendData((byte)FocuserCmd.FOCUSER_SET_MICROSTEP, value); }
        }

        public void Stop()
        { SendData((byte)FocuserCmd.FOCUSER_STOP); }
        public void Ping()
        { SendData((byte)FBC.FBC_PING); }
        public void StepRight() 
        { 
            SendData((byte)FocuserCmd.FOCUSER_STEP_RIGHT);
        }
        public void StepLeft()
        {
            SendData((byte)FocuserCmd.FOCUSER_STEP_LEFT);
        }
        public void RollRight()
        {
            SendData((byte)FocuserCmd.FOCUSER_ROLL_RIGHT);
        }
        public void RollLeft()
        {
            SendData((byte)FocuserCmd.FOCUSER_ROLL_LEFT);
        }
        public void Goto(long new_pos)
        {
            SendData((byte)FocuserCmd.FOCUSER_GO_TO_POSITION, Convert.ToString(new_pos));
        }
        protected bool fIsRolling = false;
        public bool IsRolling
        {
            get { return fIsRolling; }            
        }
        

        public event EventHandler OnPositionChangedEvent = null;
        public delegate void OnChangePosistionHandler();

        public event EventHandler OnMotorStopEvent = null;
        public delegate void OnMotorStopHandler();

        public event EventHandler OnMotorStepRightEvent = null;
        public delegate void OnMotorStepRightHandler();

        public event EventHandler OnMotorStepLeftEvent = null;
        public delegate void OnMotorStepLeftHandler();

        public event EventHandler OnPowerEvent = null;
        public delegate void OnPowerHandler();

        public event EventHandler OnRollingEvent = null;
        public delegate void OnRollingHandler();
        
        public event OnRollingToNewPosHandler OnRollingToNewPosEvent = null;
        public delegate void OnRollingToNewPosHandler(Int64 is_rolling);

        public event EventHandler OnSpeedChangedEvent = null;
        public delegate void OnSpeedChangedHandler();

        public event EventHandler OnMaxSpeedChangedEvent = null;
        public delegate void OnMaxSpeedChangedHandler();

        public event EventHandler OnMinSpeedChangedEvent = null;
        public delegate void OnMinSpeedChangedHandler();

        public event EventHandler OnReleaseTimeChangeEvent = null;
        public delegate void OnReleaseTimeHandler();

        public event EventHandler OnRangeCheckEvent = null;
        public delegate void OnRangeCkeckHandler();

        public event EventHandler OnMaxPositionChangedEvent = null;
        public delegate void OnMaxPositionChangedHandler();

        public event EventHandler OnMinPositionChangedEvent = null;
        public delegate void OnMinPositionChangedHandler();

        public event EventHandler OnMicrostepEvent = null;
        public delegate void OnMicrostepHandler();

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


        public void SendData(byte par) //  отправка однобайтной команды 
        {

            if (!fIsConnected)
                return; 
            byte[] buf = { (byte)FBC.FBC_FIRST_BYTE, par, (byte)FBC.FBC_LAST_BYTE1, (byte)FBC.FBC_LAST_BYTE2 };
            lock (fComPort)
            {
                fComPort.Write(buf, 0, 4);
            }
        }

        public void SendData(byte par, byte data) //  отправка однобайтной команды c одним однобайтным параметром
        {
            if (!fIsConnected)
                return;
            byte[] buf = { (byte)FBC.FBC_FIRST_BYTE, par, data, (byte)FBC.FBC_LAST_BYTE1, (byte)FBC.FBC_LAST_BYTE2 };
            lock (fComPort)
            {
                fComPort.Write(buf, 0, 5);
            }
        }

        public void SendData(byte par, string data) //  отправка однобайтной команды c одним строковым параметром
        {
            if (!fIsConnected)
                return;
            byte[] b2 = System.Text.Encoding.ASCII.GetBytes(data);
            int cmd_length = 1 + 1 + b2.GetLength(0) + 2;
            byte[] buf = new byte[cmd_length];
            buf[0] = (byte)FBC.FBC_FIRST_BYTE;
            buf[1] = par;
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

        string GetStringFormArray(byte [] buf, int len, int index = 0)
        {
            byte[] t = new byte[len];
            for (int i = 0; i < len; i++)
                t[i] = buf[index+i];
            return System.Text.Encoding.ASCII.GetString(t);
        }

        protected void ParseCommand(byte[] cmd, int cmdlen)
        {
            if (cmd.Length == 0)
                return;

            switch ((byte)cmd[0])
            {
                case (byte)FBC.FBC_HANDSHAKE:

                    fDeviceType = (FDEV)cmd[1];                    
                    SendData((byte)FBC.FBC_GET_FIRMWARE_VERSION);
                    
                    SendData((byte)FBC.FBC_RCP, 1);
                    SendData((byte)FBC.FBC_DEBUG, 0);
                    SendData((byte)FocuserCmd.FOCUSER_RELEASE);
                    Thread.Sleep(10);
                    
                    SendData((byte)FocuserCmd.FOCUSER_GET_POSITION);   // Get motor current position
                    SendData((byte)FocuserCmd.FOCUSER_RANGE_CHECK, (byte)0);   // Get motor current speed
                    SendData((byte)FocuserCmd.FOCUSER_GET_RELAESE_TIME);  // Get motor release delay
                    SendData((byte)FocuserCmd.FOCUSER_GET_SPEED);   // Get motor current speed
                    Thread.Sleep(10);
                        
                    SendData((byte)FocuserCmd.FOCUSER_GET_MICROSTEP);  //  Get microstep mode
                    SendData((byte)FocuserCmd.FOCUSER_GET_MIN_SPEED_DELAY);   // Get motor max speed
                    SendData((byte)FocuserCmd.FOCUSER_GET_MAX_SPEED_DELAY);   // Get motor min speed

                    Thread.Sleep(10);
                    SendData((byte)FocuserCmd.FOCUSER_GET_MIN_POSITION);   // Get motor max speed
                    SendData((byte)FocuserCmd.FOCUSER_GET_MAX_POSITION);   // Get motor min speed                    

                    if ((OnHandshakeEvent != null) && (fParent != null))
                          fParent.BeginInvoke(OnHandshakeEvent);
                    break;

                case (byte)FBC.FBC_PING:
                    if ((OnPingEvent != null) && (fParent != null))
                       fParent.BeginInvoke(OnPingEvent);                    
                    break;

                case (byte)FBC.FBC_GET_FIRMWARE_VERSION:
                    fDeviceVersion = GetStringFormArray(cmd, cmdlen - 1, 1); 
                    if ((OnFirmwareEvent != null) && (fParent != null))
                        fParent.BeginInvoke(OnFirmwareEvent);
                    break;

                case (byte)FBC.FBC_RCP:
                    fIsRCP_ON = Convert.ToBoolean(cmd[1]);
                    if ((OnRPCEvent != null) && (fParent != null))
                        fParent.BeginInvoke(OnRPCEvent);
                    break;
                case (byte)FBC.FBC_DEBUG:
                    fIsDebug = Convert.ToBoolean(cmd[1]);
                    if ((OnDebugEvent != null) && (fParent != null))
                        fParent.BeginInvoke(OnDebugEvent);
                    break;
                case (byte)FBC.FBC_DEBUG_MSG:
                    {
                        fDeviceDbgMsgArray.Enqueue(GetStringFormArray(cmd, cmdlen-1, 1));
                        if ((OnDebugMsgEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnDebugMsgEvent);
                        break;
                    }
//---------------------
                case (byte)FocuserCmd.FOCUSER_GET_POSITION:
                    {
                        fPosition = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnPositionChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnPositionChangedEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MIN_POSITION:
                case (byte)FocuserCmd.FOCUSER_SET_MIN_POSITION:
                    {
                        fMinPosition = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMinPositionChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMinPositionChangedEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MAX_POSITION:
                case (byte)FocuserCmd.FOCUSER_SET_MAX_POSITION:
                    {
                        fMaxPosition = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMaxPositionChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMaxPositionChangedEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_STOP:
                    {
                        if ((OnMotorStopEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMotorStopEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_STEP_RIGHT:
                    {
                        if ((OnMotorStepRightEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMotorStepRightEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_STEP_LEFT:
                    {
                        if ((OnMotorStepLeftEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMotorStepLeftEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_RELEASE:
                    {
                        fIsPowerOn = false;
                        if ((OnPowerEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnPowerEvent);                        
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_POWER_ON:
                    {
                        fIsPowerOn = true;
                        if ((OnPowerEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnPowerEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_ROLLING:
                    {
//                        fIsPowerOn = true;
                        fIsRolling = Convert.ToBoolean(cmd[1]);
                        if ((OnRollingEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnRollingEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_SPEED:
                case (byte)FocuserCmd.FOCUSER_SET_SPEED:
                    {
                        fStepDelay = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnSpeedChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnSpeedChangedEvent);
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_SET_RELAESE_TIME:
                case (byte)FocuserCmd.FOCUSER_GET_RELAESE_TIME:
                    {
                        fReleaseTime = Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnReleaseTimeChangeEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnReleaseTimeChangeEvent);                        
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_GO_TO_POSITION:
                    {
                        int new_pos = Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnRollingToNewPosEvent != null) && (fParent != null)){
                            object[] par = new object[1];                            
                            par[0] = new_pos;
                            fParent.BeginInvoke(OnRollingToNewPosEvent, par);
                        }
                        break;
                    }

                case (byte)FocuserCmd.FOCUSER_GET_MAX_SPEED_DELAY:
                    {
                        fMaxStepDelay = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMaxSpeedChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMaxSpeedChangedEvent);                        
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MIN_SPEED_DELAY:
                    {
                        fMinStepDelay = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMinSpeedChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMinSpeedChangedEvent);                        
                        break;
                    }
                case (byte)FocuserCmd.FOCUSER_RANGE_CHECK:
                    {
                        fIsRangeCheck = Convert.ToBoolean(cmd[1]);
                        if ((OnRangeCheckEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnRangeCheckEvent);
                        break;                        
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MICROSTEP:
                case (byte)FocuserCmd.FOCUSER_SET_MICROSTEP:
                    {
                        fMicroStepMode = (byte)(cmd[1]);
                        if ((OnMicrostepEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMicrostepEvent);
                        break;
                    }
            }
        }

    };
 
    

}