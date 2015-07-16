using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ASCOM;
using FConnection;
using System.Threading;

namespace FConnection
{
    public class TFFocuser : TFBasicCom
    {
        public TFFocuser(bool isASCOM = false, Form parent = null)            
            : base (isASCOM, parent)
        {
            fDeviceType = FDEV.FD_FOCUSER;              
        }


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

        protected long fStepDelay = 100, fMaxStepDelay = 10, fMinStepDelay = 1000000;  // microseconds
        public long StepDelay
        {
            get { return fStepDelay; }
            set { SendData((byte)FocuserCmd.FOCUSER_SET_SPEED, Convert.ToString(value)); }
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
            set { SendData((byte)MounterCmd.FOCUSER_SET_MICROSTEP, value); }
        }        

        public void Stop()
        { SendData((byte)FocuserCmd.FOCUSER_STOP); }
       
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

        protected override void OnHandshakeRouting()
        {
            base.OnHandshakeRouting();
            Thread.Sleep(10);
            SendData((byte)FocuserCmd.FOCUSER_RELEASE);
            SendData((byte)FocuserCmd.FOCUSER_GET_POSITION);   // Get motor current position
            Thread.Sleep(10);
            SendData((byte)FocuserCmd.FOCUSER_RANGE_CHECK, (byte)0);   // Get motor current speed
            SendData((byte)FocuserCmd.FOCUSER_GET_RELAESE_TIME);  // Get motor release delay
            SendData((byte)FocuserCmd.FOCUSER_GET_SPEED);   // Get motor current speed                       
            SendData((byte)FocuserCmd.FOCUSER_GET_MICROSTEP);  //  Get microstep mode
            Thread.Sleep(10);
            SendData((byte)FocuserCmd.FOCUSER_GET_MIN_SPEED_DELAY);   // Get motor max speed
            SendData((byte)FocuserCmd.FOCUSER_GET_MAX_SPEED_DELAY);   // Get motor min speed
            SendData((byte)FocuserCmd.FOCUSER_GET_MIN_POSITION);   // Get motor max speed
            SendData((byte)FocuserCmd.FOCUSER_GET_MAX_POSITION);   // Get motor min speed                    
        }

        protected override bool ParseCommand(byte[] cmd, int cmdlen)
        {
            if (base.ParseCommand(cmd, cmdlen))
                return true;

            switch ((byte)cmd[0])
            {
                case (byte)FocuserCmd.FOCUSER_GET_POSITION:
                    {
                        fPosition = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnPositionChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnPositionChangedEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MIN_POSITION:
                case (byte)FocuserCmd.FOCUSER_SET_MIN_POSITION:
                    {
                        fMinPosition = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMinPositionChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMinPositionChangedEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MAX_POSITION:
                case (byte)FocuserCmd.FOCUSER_SET_MAX_POSITION:
                    {
                        fMaxPosition = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMaxPositionChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMaxPositionChangedEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_STOP:
                    {
                        if ((OnMotorStopEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMotorStopEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_STEP_RIGHT:
                    {
                        if ((OnMotorStepRightEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMotorStepRightEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_STEP_LEFT:
                    {
                        if ((OnMotorStepLeftEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMotorStepLeftEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_RELEASE:
                    {
                        fIsPowerOn = false;
                        if ((OnPowerEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnPowerEvent);                        
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_POWER_ON:
                    {
                        fIsPowerOn = true;
                        if ((OnPowerEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnPowerEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_ROLLING:
                    {
//                        fIsPowerOn = true;
                        fIsRolling = Convert.ToBoolean(cmd[1]);
                        if ((OnRollingEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnRollingEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_SPEED:
                case (byte)FocuserCmd.FOCUSER_SET_SPEED:
                    {
                        fStepDelay = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnSpeedChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnSpeedChangedEvent);
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_SET_RELAESE_TIME:
                case (byte)FocuserCmd.FOCUSER_GET_RELAESE_TIME:
                    {
                        fReleaseTime = Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnReleaseTimeChangeEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnReleaseTimeChangeEvent);                        
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_GO_TO_POSITION:
                    {
                        int new_pos = Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnRollingToNewPosEvent != null) && (fParent != null)){
                            object[] par = new object[1];                            
                            par[0] = new_pos;
                            fParent.BeginInvoke(OnRollingToNewPosEvent, par);
                        }
                        return true;
                    }

                case (byte)FocuserCmd.FOCUSER_GET_MAX_SPEED_DELAY:
                    {
                        fMaxStepDelay = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMaxSpeedChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMaxSpeedChangedEvent);                        
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MIN_SPEED_DELAY:
                    {
                        fMinStepDelay = Convert.ToInt64(GetStringFormArray(cmd, cmdlen - 1, 1));
                        if ((OnMinSpeedChangedEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMinSpeedChangedEvent);                        
                        return true;
                    }
                case (byte)FocuserCmd.FOCUSER_RANGE_CHECK:
                    {
                        fIsRangeCheck = Convert.ToBoolean(cmd[1]);
                        if ((OnRangeCheckEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnRangeCheckEvent);
                        return true;                        
                    }
                case (byte)FocuserCmd.FOCUSER_GET_MICROSTEP:
                case (byte)FocuserCmd.FOCUSER_SET_MICROSTEP:
                    {
                        fMicroStepMode = (byte)(cmd[1]);
                        if ((OnMicrostepEvent != null) && (fParent != null))
                            fParent.BeginInvoke(OnMicrostepEvent);
                        return true;
                    };
        }

            return false;
        }
    }
}

