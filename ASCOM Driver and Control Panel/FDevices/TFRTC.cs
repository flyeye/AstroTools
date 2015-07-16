using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace FConnection
{

#region DateTime

    public class TFDeviceRTC : TFDeviceComponent
    {

        protected DateTime fDateTime;
        protected float fTemperature;

        public string StdFormat;

        protected int fDTDelay = 0;
        System.Threading.Timer fDTTimer;
        protected int fTemperatureDelay = 0;
        System.Threading.Timer fTemperatureTimer;

        public TFDeviceRTC(TFBasicCom parent)
            : base(parent)
        {
            fDateTime = DateTime.Now;
            StdFormat = "yyyy-M-d H:m:s";
            fTemperature = 0;

            fDTTimer = new System.Threading.Timer(CheckDTStatus, null, 1000, Timeout.Infinite);
            fTemperatureTimer = new System.Threading.Timer(CheckTempStatus, null, 1000, Timeout.Infinite);
        }

        public void CheckDTStatus(Object stateInfo)
        {
            GetDevDateTime();
        }

        public void CheckTempStatus(Object stateInfo)
        {
            GetRTCTemperature();
        }

        public int DTTimer
        {
            get { return fDTDelay; }
            set
            {
                if (fDTDelay == value)
                    return;
                if (value == 0)
                {
                    fDTTimer.Change(0, Timeout.Infinite);
                    fDTDelay = 0;
                }
                else
                {
                    fDTDelay = value;
                    fDTTimer.Change(fDTDelay, fDTDelay);
                }

            }
        }

        public int TemperatureTimer
        {
            get { return fTemperatureDelay; }
            set
            {
                if (fTemperatureDelay == value)
                    return;
                if (value == 0)
                {
                    fTemperatureTimer.Change(0, Timeout.Infinite);
                    fTemperatureDelay = 0;
                }
                else
                {
                    fTemperatureDelay = value;
                    fTemperatureTimer.Change(fTemperatureDelay, fTemperatureDelay);
                }
            }
        }

        public override void OnHandshake()
        {
            GetDevDateTime();
            GetRTCTemperature();
        }

        public override bool OnCommand(byte[] cmd, int cmdlen)
        {

            if (cmd.Length == 0)
                return false;

            switch ((byte)cmd[0])
            {
                case (byte)FBC.FBC_RTC_TEMP:
                    {
                        SetTemperature(Convert.ToSingle(fParent.GetStringFormArray(cmd, cmdlen - 1, 1)));
                        return true;
                    }
                case (byte)FBC.FBC_RTC_GET:              //  Get device current datetime 
                case (byte)FBC.FBC_RTC_SET:
                    {
                        string str = fParent.GetStringFormArray(cmd, cmdlen - 1, 1);
                        //                        string str = "2015/6/27 2:5:3";
                        CultureInfo provider = CultureInfo.InvariantCulture;
                        SetDateTime(DateTime.ParseExact((string)str, (string)StdFormat, provider));                        
                        return true;
                    };
            }
            return false;
        }

        public void GetDevDateTime()
        {
            fParent.SendData((byte)FBC.FBC_RTC_GET);
        }


        public void GetRTCTemperature()
        {
            fParent.SendData((byte)FBC.FBC_RTC_TEMP);
        }

        public DateTime DeviceDateTime
        {
            get { return fDateTime; }
            set { fParent.SendData((byte)FBC.FBC_RTC_SET, (string)DateTimeToStrStd(value)); }
        }

        protected void SetDateTime(DateTime dt)
        {
            fDateTime = dt;
            if ((OnDateTimeChangedEvent != null) && (fParent.Parent != null))
                fParent.Parent.BeginInvoke(OnDateTimeChangedEvent);
        }

        public float DevTemperature
        {
            get { return fTemperature; }            
        }
        protected void SetTemperature(float temp)
        {
            fTemperature = temp;
            if ((OnTemperatureChangedEvent != null) && (fParent.Parent != null))
                fParent.Parent.BeginInvoke(OnTemperatureChangedEvent);
        }

        public string DateTimeToStrStd(DateTime dt)
        {
            return dt.ToString(StdFormat);
        }

        public event EventHandler OnDateTimeChangedEvent = null;
        public delegate void OnChangeDateTimeHandler();

        public event EventHandler OnTemperatureChangedEvent = null;
        public delegate void OnChangeTemperatureHandler();

    }
    #endregion
}
