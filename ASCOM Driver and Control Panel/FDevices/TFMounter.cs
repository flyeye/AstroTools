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
    
    public class TFMounter : TFBasicCom
    {        
   
        const double EARTH_TURN_SEC = 0.0002785383083;  //Звездная скорость в угловых часах за секунду звездного времени
// const EARTH_TURN = 86164.0905;   // звезные сутки, время полного оборота Земли в секундах

        public TFMounter(bool isASCOM = false, Form parent = null)            
            : base (isASCOM, parent)
        {
            fDeviceType = FDEV.FD_MOUNTER;
            RAStepper = new TFStepperMotor( MOTOR.RA, this, 3);
            DEStepper = new TFStepperMotor( MOTOR.DE, this, 3);

            fOffset = DateTime.Now;

            fDeviceDescription = " \r\n \r\n Mounter Control Panel \r\n ver 1.0.net \r\n \r\n Created by Alexey V. Popov \r\n 9141866@gmail.com \r\n St.-Petersburg \r\n Russia \r\n \r\n 2015 \r\n";

            fCheckPosFreq = 2000;
            fCheckClockFreq = 3000;
            fPosTimer = new System.Threading.Timer(CheckPosEQ, null, 1000, fCheckPosFreq);
            fClockTimer = new System.Threading.Timer(CheckClock, null, 1000, fCheckClockFreq);

            OnGotoWaitEvent = new EventWaitHandle(false, EventResetMode.ManualReset);
            OnStopWaitEvent = new EventWaitHandle(false, EventResetMode.ManualReset);
        }

        public TFStepperMotor RAStepper, DEStepper;

        public int fCheckPosFreq, fCheckClockFreq;
        protected System.Threading.Timer fPosTimer;
        protected System.Threading.Timer fClockTimer;
        public void CheckPosEQ(Object stateInfo)
        {
            if (!IsConnected)
                return;
            if ((RAStepper.Status == STEPDIR.STEP_HOLD) && (DEStepper.Status == STEPDIR.STEP_HOLD))
                UpdatePosition();    
        }

        public void CheckClock(Object stateInfo)
        {
            if (!IsConnected)
                return;
            RTC.GetDevDateTime();
        }

        protected bool fIsNewPositionMoving = false;
        public bool IsRolling
        {
            get 
            {
                if ((RAStepper.Status != STEPDIR.STEP_HOLD) || (DEStepper.Status != STEPDIR.STEP_HOLD) || fIsNewPositionMoving)
                    return true; 
                else 
                    return false;
            }
        }
        public bool IsTurning
        {
            get { return (RATurning || DETurning); }
            set {
                if (RATurning != value)
                    RATurning = value;
                if (DETurning != value)
                    DETurning = value;
            }
        }


        protected double fPosRA_EQ = 0, fPosDE_EQ = 0;
        protected DateTime fOffset;
        protected bool fIsTimeRunning = false;
        public bool IsTimeRunning
        {
            get { return fIsTimeRunning; }
            set { fIsTimeRunning = value; }
        }
        public double fGearRatioRA = 1.0, fGearRatioDE = 1.0;

        public double PosDE_EQ
        {
            get { return fPosDE_EQ; }
            set 
            {
                if (value > 90)
                    return;
                if (DEStepper.Position >= 0)
                    DEStepper.Position = Convert.ToInt32((90 - value) * fGearRatioDE);
                else
                    DEStepper.Position = -1*Convert.ToInt32((90 - value) * fGearRatioDE);         
            }
        }
        public double PosRA_EQ
        {
            get { return fPosRA_EQ; }
            set 
            {
                if (value > 24)
                    return;
                if (value>12)
                    value -= 24;
                RAStepper.Position = -1 * Convert.ToInt32(value * fGearRatioRA);
                fOffset = DateTime.Now;
            }
        }

        public event EventHandler OnGotoEvent = null;
        public delegate void OnGotoHandler();
        public EventWaitHandle OnGotoWaitEvent = null;
        public EventWaitHandle OnStopWaitEvent = null;

// -------  Release time        
        volatile protected int fReleaseTime = 0;  // in seconds
        public int ReleaseTime
        {
            get { return fReleaseTime; }
            set 
            {
                SendData((byte)FocuserCmd.FOCUSER_SET_RELAESE_TIME, Convert.ToString(value));                
            }
        }
        protected void SetReleaseTime(int value)
        {
            fReleaseTime = value;
            if ((OnReleaseTimeChangeEvent != null) && (fParent != null))
                fParent.BeginInvoke(OnReleaseTimeChangeEvent);
        }
        protected void GetReleaseTime()
        { 
            if (IsConnected)
                SendData((byte)FocuserCmd.FOCUSER_GET_RELAESE_TIME);                
        }

        public event EventHandler OnReleaseTimeChangeEvent = null;
        public delegate void OnReleaseTimeHandler();
        
//-----------------------------
        protected bool fRATurning = false;
        public bool RATurning
        {
            get { return fRATurning; }
            set 
            {
                if (value)
                    SendData((byte)MounterCmd.MOUNTER_SET_DROTATION, (byte)MOTOR.RA, (byte)1);
                else
                    SendData((byte)MounterCmd.MOUNTER_SET_DROTATION, (byte)MOTOR.RA, (byte)0);
            }
        }
        protected void SetRATurning(bool b)
        {
            fRATurning = b;
            if ((OnRATurningEvent != null) && (fParent != null))
                fParent.BeginInvoke(OnRATurningEvent);
        }
        protected void GetRATurning()
        {
            SendData((byte)MounterCmd.MOUNTER_GET_DROTATION, (byte)MOTOR.RA);
        }

        public event EventHandler OnRATurningEvent = null;
        public delegate void OnRATurningHandler();

        protected bool fDETurning = false;
        public bool DETurning
        {
            get { return fDETurning; }
            set
            {
                if (value)
                    SendData((byte)MounterCmd.MOUNTER_SET_DROTATION, (byte)MOTOR.DE, (byte)1);
                else
                    SendData((byte)MounterCmd.MOUNTER_SET_DROTATION, (byte)MOTOR.DE, (byte)0);
            }
        }
        protected void SetDETurning(bool b)
        {
            fDETurning = b;
            if ((OnDETurningEvent != null) && (fParent != null))
                fParent.BeginInvoke(OnDETurningEvent);
        }
        protected void GetDETurning()
        {
            SendData((byte)MounterCmd.MOUNTER_GET_DROTATION, (byte)MOTOR.DE);
        }

        public event EventHandler OnDETurningEvent = null;
        public delegate void OnDETurningHandler();

//============================= Modes        
//----------------------------- Microsteps 
        public byte NavMicroStep
        {
            get { return RAStepper.fModes[(byte)MODES.NAVIGATION].fMicrostep; }
            set { SendData((byte)MounterCmd.FOCUSER_SET_MICROSTEP, (byte)MODES.NAVIGATION, value); }
        }
        public void SetNavMicrostep(byte ms)
        {
            RAStepper.fModes[(byte)MODES.NAVIGATION].fMicrostep = ms;
            if ((OnNavMicrostepEvent != null) && (fParent != null))
                Parent.BeginInvoke(OnNavMicrostepEvent);
        }
        public void GetNavMicrostep()
        {
            SendData((byte)MounterCmd.FOCUSER_GET_MICROSTEP, (byte)MODES.NAVIGATION);  //  Get microstep mode
        }

        public event EventHandler OnNavMicrostepEvent = null;
        public delegate void OnNavMicrostepHandler();

        public byte DailyMicroStep
        {
            get { return RAStepper.fModes[(byte)MODES.DAILY].fMicrostep; }
            set { SendData((byte)MounterCmd.FOCUSER_SET_MICROSTEP, (byte)MODES.DAILY, value); }
        }
        public void SetDailyMicrostep(byte ms)
        {
            RAStepper.fModes[(byte)MODES.DAILY].fMicrostep = ms;
            if ((OnDailyMicrostepEvent != null) && (fParent != null))
                Parent.BeginInvoke(OnDailyMicrostepEvent);
        }
        public void GetDailyMicrostep()
        {
            SendData((byte)MounterCmd.FOCUSER_GET_MICROSTEP, (byte)MODES.DAILY);  //  Get microstep mode
        }

        public event EventHandler OnDailyMicrostepEvent = null;
        public delegate void OnDailyMicrostepHandler();
//----------------------------- Current speed
        public double NavSpeed
        {
            get 
            {
                return (1000000.0 / RAStepper.fModes[(byte)MODES.NAVIGATION].CurrentStepDelay) / (fGearRatioDE / 3600.0);                  
            }
            set { SendData((byte)MounterCmd.MOUNTER_SET_NAV_SPEED, Convert.ToString(Convert.ToInt32((1000000.0/value)/(fGearRatioDE/3600.0)))); }
        }
        public void SetNavSpeed(int stepdelay)
        {
            RAStepper.fModes[(byte)MODES.NAVIGATION].fCurrentStepDelay = stepdelay;
            if ((OnNavSpeedEvent != null) && (fParent != null))
                Parent.BeginInvoke(OnNavSpeedEvent);
        }
        public void GetNavSpeed()
        {
            SendData((byte)MounterCmd.MOUNTER_GET_NAV_SPEED);  //  Get microstep mode
        }

        public event EventHandler OnNavSpeedEvent = null;
        public delegate void OnNavSpeedHandler();

        public double NavSpeedMin
        {
            get
            {
                return (1000000.0 / RAStepper.fModes[(byte)MODES.NAVIGATION].fMaxStepDelay) / (fGearRatioDE / 3600.0);
            }
//            set { SendData((byte)MounterCmd.MOUNTER_SET_NAV_MAX_SPEED, Convert.ToString(Convert.ToInt32((1000000.0 / value) / (fGearRatioDE / 3600.0)))); }
        }
        public void SetNavSpeedMin(int stepdelay)
        {
            RAStepper.fModes[(byte)MODES.NAVIGATION].fMaxStepDelay = stepdelay;
            if ((OnNavSpeedMinEvent != null) && (fParent != null))
                Parent.BeginInvoke(OnNavSpeedMinEvent);
        }
        public void GetNavSpeedMin()
        {
            SendData((byte)MounterCmd.MOUNTER_GET_NAV_MAX_SPEED);  //  Get microstep mode
        }

        public event EventHandler OnNavSpeedMinEvent = null;
        public delegate void OnNavSpeedMinHandler();


        public double NavSpeedMax
        {
            get
            {
                return (1000000.0 / RAStepper.fModes[(byte)MODES.NAVIGATION].fMinStepDelay) / (fGearRatioDE / 3600.0);
            }
            //            set { SendData((byte)MounterCmd.MOUNTER_SET_NAV_MAX_SPEED, Convert.ToString(Convert.ToInt32((1000000.0 / value) / (fGearRatioDE / 3600.0)))); }
        }
        public void SetNavSpeedMax(int stepdelay)
        {
            RAStepper.fModes[(byte)MODES.NAVIGATION].fMinStepDelay = stepdelay;
            if ((OnNavSpeedMaxEvent != null) && (fParent != null))
                Parent.BeginInvoke(OnNavSpeedMaxEvent);
        }
        public void GetNavSpeedMax()
        {
            SendData((byte)MounterCmd.MOUNTER_GET_NAV_MIN_SPEED);  //  Get microstep mode
        }

        public event EventHandler OnNavSpeedMaxEvent = null;
        public delegate void OnNavSpeedMaxHandler();
//----------------------------- Current speed

        public double DailySpeedRA
        {
            get
            {
                return (1000000.0 / RAStepper.fModes[(byte)MODES.DAILY].CurrentStepDelay) / (fGearRatioRA / 54000.0);
            }
            set 
            {
                if (value < 0.0000001)
                    SendData((byte)MounterCmd.MOUNTER_SET_DAILY_SPEED, (byte)MOTOR.RA, Convert.ToString(1000000000)); 
                else 
                    SendData((byte)MounterCmd.MOUNTER_SET_DAILY_SPEED, (byte)MOTOR.RA, Convert.ToString(Convert.ToInt32((1000000.0 / value) / (fGearRatioRA / 54000.0)))); 
            }
        }
        public void SetDailySpeedRA(int stepdelay)
        {
            RAStepper.fModes[(byte)MODES.DAILY].fCurrentStepDelay = stepdelay;
            if ((OnDailySpeedRAEvent != null) && (fParent != null))
                Parent.BeginInvoke(OnDailySpeedRAEvent);
        }
        public void GetDailySpeedRA()
        {
            SendData((byte)MounterCmd.MOUNTER_GET_DAILY_SPEED, (byte)MOTOR.RA);  //  Get microstep mode
        }

        public event EventHandler OnDailySpeedRAEvent = null;
        public delegate void OnDailySpeedRAHandler();

        public double DailySpeedDE
        {
            get
            {
                return (1000000.0 / DEStepper.fModes[(byte)MODES.DAILY].CurrentStepDelay) / (fGearRatioDE / 3600.0);
            }
            set 
            { 
                if (value<0.0000001)                    
                    SendData((byte)MounterCmd.MOUNTER_SET_DAILY_SPEED, (byte)MOTOR.DE, Convert.ToString(1000000000)); 
                else 
                    SendData((byte)MounterCmd.MOUNTER_SET_DAILY_SPEED, (byte)MOTOR.DE, Convert.ToString(Convert.ToInt32((1000000.0 / value) / (fGearRatioDE / 3600.0)))); 
            }
        }
        public void SetDailySpeedDE(int stepdelay)
        {
            DEStepper.fModes[(byte)MODES.DAILY].fCurrentStepDelay = stepdelay;
            if ((OnDailySpeedDEEvent != null) && (fParent != null))
                Parent.BeginInvoke(OnDailySpeedDEEvent);
        }
        public void GetDailySpeedDE()
        {
            SendData((byte)MounterCmd.MOUNTER_GET_DAILY_SPEED, (byte)MOTOR.DE);  //  Get microstep mode
        }

        public event EventHandler OnDailySpeedDEEvent = null;
        public delegate void OnDailySpeedDEHandler();

//-----------------------------
        protected override void OnHandshakeRouting()
        {
            base.OnHandshakeRouting();
            Thread.Sleep(10);

            RAStepper.OnHandshake();
            DEStepper.OnHandshake();

            GetDETurning();   //  перенести в TFStepper
            GetRATurning();

            GetNavMicrostep();  //  перенести в TFStepper
            GetDailyMicrostep();
            GetNavSpeed();
            GetDailySpeedRA();
            GetDailySpeedDE();
            GetNavSpeedMin();
            GetNavSpeedMax();

            UpdatePosition();
            GetReleaseTime();
        }

        protected override bool ParseCommand(byte[] cmd, int cmdlen)
        {
            if (base.ParseCommand(cmd, cmdlen))
                return true;           

            switch ((byte)cmd[0])
            {
                case (byte)MounterCmd.FOCUSER_GET_POSITION:
                    {
                        int pos = GetIntFormArray(cmd, 2);
                        switch ((MOTOR)cmd[1]) 
                        { 
                            case MOTOR.RA:
                                RAStepper.SetPosition(pos);

                                fPosRA_EQ = -pos/fGearRatioRA;
                                if (fIsTimeRunning) 
                                {
                                    TimeSpan t = (DateTime.Now).Subtract(fOffset);                                                                        
                                    fPosRA_EQ = fPosRA_EQ + t.TotalSeconds * EARTH_TURN_SEC;
                                }
                                if (Math.Abs(fPosRA_EQ)>24)
                                    fPosRA_EQ = fPosRA_EQ - Math.Truncate(fPosRA_EQ / 24) * 24; 
                                if (fPosRA_EQ<0)
                                    fPosRA_EQ = fPosRA_EQ + 24;
                                break;
                            case MOTOR.DE:
                                DEStepper.SetPosition(pos);
                                fPosDE_EQ = 90 - Math.Abs(pos) / fGearRatioDE;
                                break;
                        }
                        return true;
                    }                                
                case (byte)FocuserCmd.FOCUSER_GET_RELAESE_TIME:
                case (byte)FocuserCmd.FOCUSER_SET_RELAESE_TIME:
                    {                        
                        SetReleaseTime(Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1)));
                        return true;                        
                    }
                case (byte)MounterCmd.MOUNTER_SET_DROTATION:
                case (byte)MounterCmd.MOUNTER_GET_DROTATION:
                    {
                        switch ((MOTOR)cmd[1])
                        {
                            case MOTOR.RA:
                                SetRATurning(Convert.ToBoolean(cmd[2]));
                                break;
                            case MOTOR.DE:
                                SetDETurning(Convert.ToBoolean(cmd[2]));
                                break;
                        }
                        return true;
                    }
                case (byte)MounterCmd.FOCUSER_GO_TO_POSITION:
                    {
                        string s = GetStringFormArray(cmd, cmdlen - 1, 1);
                        if (s.CompareTo("0") == 0)
                        {
                            OnGotoWaitEvent.Set();
                            fIsNewPositionMoving = false;
                        }
                        else
                        {
                            OnGotoWaitEvent.Reset();
                            fIsNewPositionMoving = true;                            
                        }
                        if ((OnGotoEvent != null) && (fParent.Parent != null))
                            fParent.Parent.BeginInvoke(OnGotoEvent);
                        return true;
                    }
                case (byte)MounterCmd.FOCUSER_SET_MICROSTEP:
                case (byte)MounterCmd.FOCUSER_GET_MICROSTEP:
                    {
                        switch ((MODES)cmd[1])
                        {
                            case MODES.NAVIGATION:
                                SetNavMicrostep(cmd[2]);
                                break;
                            case MODES.DAILY:
                                SetDailyMicrostep(cmd[2]);;
                                break;
                        }
                        return true;
                    }
                case (byte)MounterCmd.MOUNTER_GET_NAV_SPEED:
                case (byte)MounterCmd.MOUNTER_SET_NAV_SPEED:
                    {
                        SetNavSpeed(Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1)));
                        return true;
                    }
                case (byte)MounterCmd.MOUNTER_GET_DAILY_SPEED:
                case (byte)MounterCmd.MOUNTER_SET_DAILY_SPEED:
                    {
                        switch ((MOTOR)cmd[1])
                        {
                            case MOTOR.RA:
                                SetDailySpeedRA(Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 2, 2)));
                                break;
                            case MOTOR.DE:
                                SetDailySpeedDE(Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 2, 2)));
                                break;
                        }
                        return true;
                    }
                case (byte)MounterCmd.MOUNTER_GET_NAV_MAX_SPEED:
                case (byte)MounterCmd.MOUNTER_SET_NAV_MAX_SPEED:
                    {
                        SetNavSpeedMin(Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1)));
                        return true;
                    }
                case (byte)MounterCmd.MOUNTER_GET_NAV_MIN_SPEED:
                case (byte)MounterCmd.MOUNTER_SET_NAV_MIN_SPEED:
                    {
                        SetNavSpeedMax(Convert.ToInt32(GetStringFormArray(cmd, cmdlen - 1, 1)));
                        return true;
                    }
                case (byte)MounterCmd.MOUNTER_STOP:
                    {
                        OnStopWaitEvent.Set();
                        break; //
                    }
            };

            if (RAStepper.OnCommand(cmd, cmdlen))
                return true;
            if (DEStepper.OnCommand(cmd, cmdlen))
                return true;

            return false;
        }

        public void Step(MOTOR motor, STEPDIR dir)
        {
            byte[] buf = { (byte)motor, (byte)dir };
            SendData((byte)MounterCmd.MOUNTER_STEP, buf, 2);
        }

        public void Roll(MOTOR motor, STEPDIR dir, short autostop = 0)
        {
            OnStopWaitEvent.Reset();
            byte[] buf = { (byte)motor, (byte)dir, (byte)(autostop>>8), (byte)(autostop&255)};
            SendData((byte)MounterCmd.MOUNTER_ROLL, buf, 4);
        }

        public void GotoPosition(int ra, int de)
        {
            OnGotoWaitEvent.Reset();
            fIsNewPositionMoving = true;
            SendData((byte)MounterCmd.FOCUSER_GO_TO_POSITION, ra.ToString() + ';' + de.ToString());                   
        }


        public void GotoPositionEQ(double ra, double de)
        {                                                
            int ra_l=0, de_l=0; 
            if (fIsTimeRunning)
                ra -= (DateTime.Now).Subtract(fOffset).TotalSeconds* EARTH_TURN_SEC;

            if ((fPosRA_EQ>=0)&&(ra>12))             
                ra_l = Convert.ToInt32((24-ra)*fGearRatioRA);
            else if ((fPosRA_EQ>=0)&&(ra<=12))
                ra_l = -1*Convert.ToInt32(ra*fGearRatioRA);
            else if ((fPosRA_EQ<0)&&(ra>12)) 
                ra_l = Convert.ToInt32((24-ra)*fGearRatioRA);
            else
                ra_l = -1*Convert.ToInt32(ra*fGearRatioRA);

            if (fPosDE_EQ>=0)
                de_l = Convert.ToInt32((90-de)*fGearRatioDE);
            else
                de_l = -1*Convert.ToInt32((90-de)*fGearRatioDE);

            GotoPosition(ra_l, de_l);
        }


        public void Stop(MOTOR motor)
        {
            SendData((byte)MounterCmd.MOUNTER_STOP, (byte)motor);
        }

        public void Release(MOTOR motor)
        {
            SendData((byte)MounterCmd.MOUNTER_SET_POWER, (byte)motor, (byte)0);            
        }

        public void UpdatePosition()
        {
            SendData((byte)MounterCmd.FOCUSER_GET_POSITION);   // Get motor current position
        }
    }
}
