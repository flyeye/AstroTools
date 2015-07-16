using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FConnection;

namespace FConnection
{

    public class TFStepperMode
    {
        protected TFBasicCom fParent;
        public TFStepperMode(TFBasicCom parent)
        {
            fParent = parent;
        }

        
        volatile public int fMinStepDelay = 1, fMaxStepDelay = 1, fCurrentStepDelay = 1;
        public int MinStepDelay
        {
            get {return fMinStepDelay;}            
        }
        public int MaxStepDelay
        {
            get {return fMaxStepDelay;}            
        }
        public int CurrentStepDelay
        {
            get {return fCurrentStepDelay;}            
        }

        volatile public byte fMicrostep = 0;
        public byte Microstep
        {
            get { return fMicrostep; }
        }

    }


    public class TFStepperMotor: TFDeviceComponent
    {        
        protected MOTOR fMotorID = MOTOR.NONE;

        volatile protected sbyte fCurrentMode = -1;
        volatile protected byte fModeQuantity = 0;     
        public TFStepperMode[] fModes; 

        public TFStepperMotor( MOTOR id, TFBasicCom parent, byte modes_quantity = 1)
            :base(parent)
        {           
            fMotorID = id;

            fModeQuantity = modes_quantity;
            fModes = new TFStepperMode[modes_quantity];
            for (byte i = 0; i < fModeQuantity; i++)
            {
                fModes[i] = new TFStepperMode(fParent);
            }
        }

        public override void OnHandshake()
        {
            base.OnHandshake();

            GetMicrostepMode();
            GetPowerStatus();
           
        }

        public override bool OnCommand(byte[] cmd, int cmdlen)
        {
            if (cmdlen < 2)
                return false;

            if (fMotorID!=(MOTOR)cmd[1])
                return false;

            switch ((byte)cmd[0])
            {
                case (byte)MounterCmd.MOUNTER_ROLL:
                    {                        
                        SetStatus((STEPDIR)(sbyte)cmd[2]);                                                
                        return true;
                    }
                case (byte)MounterCmd.MOUNTER_STOP:
                    {
                        SetStatus(STEPDIR.STEP_HOLD);                        
                        return true;
                    }
                case (byte)MounterCmd.FOCUSER_SET_MICROSTEP:
                    {
                        SetMicrostep((byte)cmd[2]);
                        return true;
                    }
                case (byte)MounterCmd.MOUNTER_GET_POWER:
                case (byte)MounterCmd.MOUNTER_SET_POWER:
                    {
                        SetPowerStatus(Convert.ToBoolean(cmd[2]));
                        return true;
                    }
            }
            return false;
        }

        public event EventHandler OnPositionChangedEvent = null;
        public delegate void OnChangePositionHandler();

        public event EventHandler OnStatusChangedEvent = null;
        public delegate void OnChangeStatusHandler();

        public event EventHandler OnMicrostepEvent = null;
        public delegate void OnMicrostepHandler();

        public event EventHandler OnPowerChangedEvent = null;
        public delegate void OnPowerChangedHandler();

// --------  Power
        protected bool fIsPowerOn = false;
        public bool Power
        {
            get { return fIsPowerOn; }
            set {
                if (fIsPowerOn!=value)
                    fParent.SendData((byte)MounterCmd.MOUNTER_SET_POWER, (byte)fMotorID, Convert.ToByte(value)); 
            }
        }
        protected void GetPowerStatus()
        {
            fParent.SendData((byte)MounterCmd.MOUNTER_GET_POWER, (byte)fMotorID);
        }
        protected void SetPowerStatus(bool power)
        {
            fIsPowerOn = power;
            if ((OnPowerChangedEvent!= null) && (fParent.Parent != null))
                fParent.Parent.BeginInvoke(OnPowerChangedEvent);
        }

// --------  Position 
        protected int fPosition = 0, fMinPosition = 0, fMaxPosition = 1;
        public int Position
        {
            get { return fPosition; }
            set { fParent.SendData((byte)MounterCmd.FOCUSER_SET_POSITION, (byte)fMotorID, value); }
        }
        public void SetPosition(int pos)
        {
            fPosition = pos;
            if ((OnPositionChangedEvent != null) && (fParent.Parent != null))
                fParent.Parent.BeginInvoke(OnPositionChangedEvent);
        }

// --------  Status
        volatile protected STEPDIR fStatus = STEPDIR.STEP_HOLD;
        public STEPDIR Status
        {
            get { return fStatus; }            
        }
        public void SetStatus(STEPDIR status)
        {
            fStatus = status;
            if ((OnStatusChangedEvent != null) && (fParent.Parent != null))
                fParent.Parent.BeginInvoke(OnStatusChangedEvent);
        }

// --------  Microstep mode
        volatile protected byte fMicroStepMode = 0;
        public byte MicroStep
        {
            get { return fMicroStepMode; }
            set { fParent.SendData((byte)MounterCmd.FOCUSER_SET_MICROSTEP, (byte)fMotorID, value); }
        }
        public void SetMicrostep(byte ms)
        {
            fMicroStepMode = ms;
            if ((OnMicrostepEvent != null) && (fParent != null))
                fParent.Parent.BeginInvoke(OnMicrostepEvent);            
        }
        public void GetMicrostepMode()
        {
            fParent.SendData((byte)MounterCmd.FOCUSER_GET_MICROSTEP, (byte)fMotorID);  //  Get microstep mode
        }


// ---------------------  others...
        public long MaxPosition
        {
            get { return fMaxPosition; }
//            set { fParent.SendData((byte)FocuserCmd.FOCUSER_SET_MAX_POSITION, Convert.ToString(value)); }

        }
        public long MinPosition
        {
            get { return fMinPosition; }
//            set { fParent.SendData((byte)FocuserCmd.FOCUSER_SET_MIN_POSITION, Convert.ToString(value)); }
        }

        volatile protected bool fIsRangeCheck = false;
        public bool IsRangeCheck
        {
            get { return fIsRangeCheck; }
//            set { fParent.SendData((byte)FocuserCmd.FOCUSER_RANGE_CHECK, Convert.ToByte(value)); }
        }

        volatile protected int fReleaseTime = 0;
        public int ReleaseTime
        {
//            set { fParent.SendData((byte)FocuserCmd.FOCUSER_SET_RELAESE_TIME, Convert.ToString(value)); }
            get { return fReleaseTime; }
        }
    }
}
