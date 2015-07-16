using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FConnection;

namespace FlyeyeMounterControlPanel
{
        
    public partial class MounterControlPanelForm : Form
    {
        public TFMounter fMounter;
        int baudRate = 115200;

        public MounterControlPanelForm()
        {
            InitializeComponent();            

            cbPort.SelectedIndex = 0;

            fMounter = new TFMounter(false, this);

            fMounter.OnPingEvent += new EventHandler(fMounter_OnPingEvent);
            fMounter.OnHandshakeEvent += new EventHandler(fMounter_OnHandshakeEvent);
            fMounter.OnFirmwareEvent += new EventHandler(fMounter_OnFirmwareEvent);
            fMounter.OnRPCEvent += new EventHandler(fMounter_OnRPCEvent);
            fMounter.OnDebugEvent += new EventHandler(fMounter_OnDebugEvent);
            fMounter.OnDebugMsgEvent += new EventHandler(fMounter_OnDebugMsgEvent);

            fMounter.RAStepper.OnPositionChangedEvent += new EventHandler(RAStepper_OnPositionChangedEvent);
            fMounter.DEStepper.OnPositionChangedEvent += new EventHandler(DEStepper_OnPositionChangedEvent);

            fMounter.RAStepper.OnStatusChangedEvent += new EventHandler(RAStepper_OnStatusChangedEvent);
            fMounter.DEStepper.OnStatusChangedEvent += new EventHandler(DEStepper_OnStatusChangedEvent);

            fMounter.RAStepper.OnPowerChangedEvent += new EventHandler(RAStepper_OnPowerChangedEvent);
            fMounter.DEStepper.OnPowerChangedEvent += new EventHandler(DEStepper_OnPowerChangedEvent);

            fMounter.OnRATurningEvent += new EventHandler(fMounter_OnRATurningEvent);
            fMounter.OnDETurningEvent += new EventHandler(fMounter_OnDETurningEvent);

            fMounter.OnNavMicrostepEvent += new EventHandler(fMounter_OnNavMicrostepEvent);
            fMounter.OnDailyMicrostepEvent += new EventHandler(fMounter_OnDailyMicrostepEvent);

            fMounter.OnNavSpeedEvent += new EventHandler(fMounter_OnNavSpeedEvent);
            fMounter.OnNavSpeedMinEvent += new EventHandler(fMounter_OnNavSpeedMinEvent);
            fMounter.OnNavSpeedMaxEvent += new EventHandler(fMounter_OnNavSpeedMaxEvent);
            fMounter.OnDailySpeedRAEvent += new EventHandler(fMounter_OnDailySpeedRAEvent);
            fMounter.OnDailySpeedDEEvent += new EventHandler(fMounter_OnDailySpeedDEEvent);

            fMounter.RTC.OnDateTimeChangedEvent += new EventHandler(RTC_OnDateTimeChangedEvent);
            fMounter.RTC.OnTemperatureChangedEvent += new EventHandler(RTC_OnTemperatureChangedEvent);

            fMounter.OnReleaseTimeChangeEvent += new EventHandler(fMounter_OnReleaseTimeChangeEvent);
            fMounter.OnGotoEvent += new EventHandler(fMounter_OnGotoEvent);

            cbPort.SelectedIndex = cbPort.Items.IndexOf(ASCOM.FMounterV1.Properties.Settings.Default.ComPort);
            tbRAGear.Text = ASCOM.FMounterV1.Properties.Settings.Default.RAGear.ToString();
            tbDEGear.Text = ASCOM.FMounterV1.Properties.Settings.Default.DEGear.ToString();
            cbIsTimeRunning.Checked = ASCOM.FMounterV1.Properties.Settings.Default.IsTimeRunning;

            tbAbout.Text = fMounter.DeviceDescription;
        }

        void fMounter_OnNavSpeedMaxEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Nav speed max:" + fMounter.NavSpeedMax.ToString() + " (" + fMounter.RAStepper.fModes[(byte)MODES.NAVIGATION].MinStepDelay.ToString() + ")\r\n");
            tbMaxNavSpeed.Text = fMounter.NavSpeedMax.ToString("0.#");
        }

        void fMounter_OnNavSpeedMinEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Nav speed min:" + fMounter.NavSpeedMin.ToString() + " (" + fMounter.RAStepper.fModes[(byte)MODES.NAVIGATION].MaxStepDelay.ToString() + ")\r\n");
            tbMinNavSpeed.Text = fMounter.NavSpeedMin.ToString("0.#");
        }

        void fMounter_OnDailySpeedDEEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Daily speed DE:" + fMounter.DailySpeedDE.ToString() + " (" + fMounter.DEStepper.fModes[(byte)MODES.DAILY].fCurrentStepDelay.ToString() + ")\r\n");
            tbSpeedDEDaily.Text = fMounter.DailySpeedDE.ToString("0.######");
        }

        void fMounter_OnDailySpeedRAEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Daily speed RA:" + fMounter.DailySpeedRA.ToString() + " (" + fMounter.RAStepper.fModes[(byte)MODES.DAILY].fCurrentStepDelay.ToString() + ")\r\n");
            tbSpeedRADaily.Text = fMounter.DailySpeedRA.ToString("0.######");
        }

        void fMounter_OnNavSpeedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Nav speed:" + fMounter.NavSpeed.ToString() + " (" + fMounter.RAStepper.fModes[(byte)MODES.NAVIGATION].fCurrentStepDelay.ToString() + ")\r\n");
            tbNavSpeed.Text = fMounter.NavSpeed.ToString("0.#");
            pgSpeed.Value = Convert.ToInt32((pgSpeed.Maximum - pgSpeed.Minimum) * (fMounter.NavSpeed - fMounter.NavSpeedMin) / (fMounter.NavSpeedMax - fMounter.NavSpeedMin));

        }

        void fMounter_OnDailyMicrostepEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Daily microstep:" + fMounter.RAStepper.fModes[(byte)MODES.DAILY].fMicrostep.ToString() + "\r\n");
            cbMicroStepDaily.SelectedIndex = fMounter.RAStepper.fModes[(byte)MODES.DAILY].fMicrostep;
        }

        void fMounter_OnNavMicrostepEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Nav microstep:" + fMounter.RAStepper.fModes[(byte)MODES.NAVIGATION].fMicrostep.ToString() + "\r\n");
            cbMicroStepNav.SelectedIndex = fMounter.RAStepper.fModes[(byte)MODES.NAVIGATION].fMicrostep;
        }

        void fMounter_OnGotoEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Go to the new coordinates" + "\r\n");
        }

        void fMounter_OnDETurningEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("DE turning:" + fMounter.DETurning.ToString() + "\r\n");
            cbDETurning.Checked = fMounter.DETurning;
        }

        void fMounter_OnRATurningEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("RA turning:" + fMounter.RATurning.ToString() + "\r\n");
            cbRATurning.Checked = fMounter.RATurning;
        }

        void DEStepper_OnPowerChangedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("DE Power:" + fMounter.DEStepper.Power.ToString() + "\r\n");
            cbDEPower.Checked = fMounter.DEStepper.Power;
        }

        void RAStepper_OnPowerChangedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("RA Power:" + fMounter.RAStepper.Power.ToString() + "\r\n");
            cbRAPower.Checked = fMounter.RAStepper.Power;
        }
        

        void fMounter_OnReleaseTimeChangeEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Release time:" + fMounter.ReleaseTime.ToString() + "\r\n");
            tbReleaseTime.Text = fMounter.ReleaseTime.ToString();
        }

        void DEStepper_OnStatusChangedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("DE Status:" + fMounter.DEStepper.Status.ToString() + "\r\n");
        }

        void RAStepper_OnStatusChangedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("RA Status:" + fMounter.RAStepper.Status.ToString() + "\r\n");
        }

        void RTC_OnTemperatureChangedEvent(object sender, EventArgs e)
        {

            LogBox.AppendText("Mounter temperature:" + fMounter.RTC.DevTemperature.ToString("0.0") + "\r\n");
            tbTemp.Text = fMounter.RTC.DevTemperature.ToString("0.0");
        }

        void RTC_OnDateTimeChangedEvent(object sender, EventArgs e)
        {
            dateTimePicker.Value = fMounter.RTC.DeviceDateTime;
            LogBox.AppendText("Mounter datetime:" + fMounter.RTC.DateTimeToStrStd(fMounter.RTC.DeviceDateTime) + "\r\n");
        }

        void DEStepper_OnPositionChangedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("DE Pos: " + Convert.ToString(fMounter.PosDE_EQ) + " ("+ Convert.ToString(fMounter.DEStepper.Position) + ")"+ "\r\n");            
            tbDEPos.Text = Convert.ToString(fMounter.DEStepper.Position);
            tbDEPosEQ.Text = fMounter.PosDE_EQ.ToString("0.######");
        }

        void RAStepper_OnPositionChangedEvent(object sender, EventArgs e)
        {
            string pos = "";
            if (rbDegrees.Checked)
                pos = (fMounter.PosRA_EQ * 15).ToString("0.######");
            else
                pos = (fMounter.PosRA_EQ).ToString("0.######");
            LogBox.AppendText("RA Pos: " + pos + " (" + Convert.ToString(fMounter.RAStepper.Position) + ")"+ "\r\n");            
            tbRAPos.Text = Convert.ToString(fMounter.RAStepper.Position);
            tbRAPosEQ.Text = pos;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (!fMounter.IsConnected)
                return;
            fMounter.Disconnect();

            btnConnect.Enabled = true;
            cbPort.Enabled = true;
            btnDisconnect.Enabled = false;

            btnGoTo.Enabled = false;
            btnPing.Enabled = false;
//            btnSetMinPos.Enabled = false;
            btnRollLeft.Enabled = false;
            btnRollRight.Enabled = false;
            btnRollUp.Enabled = false;
            btnRollDown.Enabled = false;
            btnSetReleaseTime.Enabled = false;
            btnSetSpeedRATurning.Enabled = false;
            btnStepLeft.Enabled = false;
            btnStepRight.Enabled = false;
            btnStepUp.Enabled = false;
            btnStepDown.Enabled = false;
            btnStop.Enabled = false;
            tbCmd.Enabled = false;
            cbObjectPos.Enabled = false;
            tbRAPosEQ.Enabled = false;
            tbReleaseTime.Enabled = false;
            tbSpeedRADaily.Enabled = false;
            cbDebug.Enabled = false;
            cbMicroStepNav.Enabled = false;
            cbMicroStepNav.Enabled = false;
            cbRAPower.Enabled = false;
            cbDEPower.Enabled = false;
//            cbRangeCheck.Enabled = false;
            cbRCP.Enabled = false;
            btnSend.Enabled = false;
//            pgPosition.Enabled = false;
            pgSpeed.Enabled = false;
            btnSetNavSpeed.Enabled = false;
//            btnSetMinPos.Enabled = false;
            btnSetPosRA.Enabled = false;
            btnSetPosDE.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;

            tbRANewPos.Enabled = false;
            tbRAPos.Enabled = false;
            tbRAPosEQ.Enabled = false;
            tbDENewPos.Enabled = false;
            tbDEPos.Enabled = false;
            tbDEPosEQ.Enabled = false;

            btnSetDEGear.Enabled = false;
            btnSetRAGear.Enabled = false;
            btnSetRAMaxMinPos.Enabled = false;
            btnSetDEMaxMinPos.Enabled = false;

            tbRAMinDailySpeed.Enabled = false; tbDEMinDailySpeed.Enabled = false;
            tbRAMaxDailySpeed.Enabled = false; tbDEMaxDailySpeed.Enabled = false;
            btnSetRADailySpeed.Enabled = false; btnSetDEDailySpeed.Enabled = false;
            cbMicroStepDaily.Enabled = false;
            tbMinNavSpeed.Enabled = false; tbMaxNavSpeed.Enabled = false;
            btnSetNavSpeed.Enabled = false;
            cbMicroStepNav.Enabled = false;

            btnTime.Enabled = false;
            rbDegrees.Enabled = false; rbHours.Enabled = false;

            cbRATurning.Enabled = false; cbDETurning.Enabled = false;
            tbSpeedRADaily.Enabled = false; tbSpeedDEDaily.Enabled = false;
            btnSetSpeedRATurning.Enabled = false; btnSetSpeedDETurning.Enabled = false;

            pgSpeed.Enabled = false;
            tbNavSpeed.Enabled = false;
            btnSetCurrentNavSpeed.Enabled = false;

            ASCOM.FMounterV1.Properties.Settings.Default.Save();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                fMounter.Connect(cbPort.Text, baudRate);              
            }
            catch (System.IO.IOException ex)
            {
                LogBox.AppendText(ex.Message + "\r\n");
            }
            catch (UnauthorizedAccessException ex)
            {
                LogBox.AppendText(ex.Message + "\r\n");
            }
            catch (System.InvalidOperationException ex)
            {
                LogBox.AppendText(ex.Message + "\r\n");
            }

            if (fMounter.IsConnected)
            {

                fMounter.fGearRatioRA = Convert.ToDouble(tbRAGear.Text);
                fMounter.fGearRatioDE = Convert.ToDouble(tbDEGear.Text);
                fMounter.IsTimeRunning = cbIsTimeRunning.Checked;

                btnConnect.Enabled = false;
                cbPort.Enabled = false;
                btnDisconnect.Enabled = true;

                btnGoTo.Enabled = true;
                btnPing.Enabled = true;
//                btnSetMinPos.Enabled = true;
                btnRollLeft.Enabled = true;
                btnRollRight.Enabled = true;
                btnRollUp.Enabled = true;
                btnRollDown.Enabled = true;
                btnSetReleaseTime.Enabled = true;
                btnSetSpeedRATurning.Enabled = true;
                btnStepLeft.Enabled = true;
                btnStepRight.Enabled = true;
                btnStepUp.Enabled = true;
                btnStepDown.Enabled = true;
                btnStop.Enabled = true;
                tbCmd.Enabled = true;
                cbObjectPos.Enabled = true;
                tbRAPosEQ.Enabled = true;
                tbReleaseTime.Enabled = true;
                tbSpeedRADaily.Enabled = true;
                cbDebug.Enabled = true;                
                cbRAPower.Enabled = true;
                cbDEPower.Enabled = true;
                //cbRangeCheck.Enabled = true;
                cbRCP.Enabled = true;
                btnSend.Enabled = true;
                //pgPosition.Enabled = true;
                pgSpeed.Enabled = true;
                btnSetNavSpeed.Enabled = true;
//                btnSetMinPos.Enabled = true;
                btnSetPosRA.Enabled = true; btnSetPosDE.Enabled = true;                
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
                tbRANewPos.Enabled = true; tbDENewPos.Enabled = true;
                tbRAPos.Enabled = true; tbDEPos.Enabled = true;
                tbRAPosEQ.Enabled = true; tbDEPosEQ.Enabled = true;                                              

                btnSetRAGear.Enabled = true; btnSetDEGear.Enabled = true;                
                btnSetRAMaxMinPos.Enabled = true; btnSetDEMaxMinPos.Enabled = true;                

                tbRAGear.Enabled = true; tbDEGear.Enabled = true;
                tbRAPosMin.Enabled = true; tbDEPosMin.Enabled = true;
                tbRAPosMax.Enabled = true; tbDEPosMax.Enabled = true;

                tbRAMinDailySpeed.Enabled = true; tbDEMinDailySpeed.Enabled = true;
                tbRAMaxDailySpeed.Enabled = true; tbDEMaxDailySpeed.Enabled = true;
                btnSetRADailySpeed.Enabled = true; btnSetDEDailySpeed.Enabled = true;
                cbMicroStepDaily.Enabled = true;
                tbMinNavSpeed.Enabled = true; tbMaxNavSpeed.Enabled = true;
                btnSetNavSpeed.Enabled = true;
                cbMicroStepNav.Enabled = true;

                btnTime.Enabled = true;
                rbDegrees.Enabled = true; rbHours.Enabled = true;

                cbRATurning.Enabled = true; cbDETurning.Enabled = true;
                tbSpeedRADaily.Enabled = true; tbSpeedDEDaily.Enabled = true;
                btnSetSpeedRATurning.Enabled = true; btnSetSpeedDETurning.Enabled = true;

                pgSpeed.Enabled = true;
                tbNavSpeed.Enabled = true;
                btnSetCurrentNavSpeed.Enabled = true;

                LoadCoordinates();                
                UpdatePosBtn();

                ASCOM.FMounterV1.Properties.Settings.Default.ComPort = cbPort.Text;
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            btnDisconnect_Click(sender, e);
            Application.Exit();
        }

        void fMounter_OnDebugMsgEvent(object sender, EventArgs e)
        {
            string s = "";
            while (fMounter.GetDebugMsg(out s))
                LogBox.AppendText("Debug msg: " + s + "\r\n");
        }

        void fMounter_OnDebugEvent(object sender, EventArgs e)
        {
            string s = "Debug mode is ";
            if (fMounter.IsDebugOn)
                s += "ON";
            else
                s += "OFF";
            LogBox.AppendText(s + "\r\n");
            cbDebug.Checked = fMounter.IsDebugOn;
        }

        void fMounter_OnRPCEvent(object sender, EventArgs e)
        {
            string s = "Remote panel is ";
            if (fMounter.IsRCP_On)
                s += "ON";
            else
                s += "OFF";
            LogBox.AppendText(s + "\r\n");
            cbRCP.Checked = fMounter.IsRCP_On;
        }

        void fMounter_OnFirmwareEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Firmware version: " + fMounter.DeviceVersion + "\r\n");
        }

        void fMounter_OnHandshakeEvent(object sender, EventArgs e)
        {
            switch (fMounter.DeviceType)
            {
                case FDEV.FD_FOCUSER:
                    LogBox.AppendText("Focuser is attached! Please, re-connect Focuser.\n");
                    btnDisconnect_Click(this, new EventArgs());
                    break;
                case FDEV.FD_MOUNTER:
                    LogBox.AppendText("Mounter is connected.\n");                    
                    break;
                case FDEV.FD_GROWER:
                    LogBox.AppendText("Grower is attached! Please, re-connect Focuser!\n");
                    btnDisconnect_Click(this, new EventArgs());
                    break;
                default:
                    LogBox.AppendText("Unknown device is attached! Please, re-connect Focuser!\n");
                    btnDisconnect_Click(this, new EventArgs());
                    break;

            };

        }

        void fMounter_OnPingEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("+ \r\n");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LogBox.Clear();
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)            
                fMounter.Ping();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.SendData(Convert.ToByte(tbCmd.Text));
        }

        private void cbDebug_CheckedChanged(object sender, EventArgs e)
        {
            fMounter.IsDebugOn = cbDebug.Checked;
        }

        private void cbRCP_CheckedChanged(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.IsRCP_On = cbRCP.Checked;
        }

        private void btnStepRight_Click(object sender, EventArgs e)
        {
            fMounter.Step(MOTOR.RA, STEPDIR.STEP_FORWARD);
        }

        private void btnStepLeft_Click(object sender, EventArgs e)
        {
            fMounter.Step(MOTOR.RA, STEPDIR.STEP_BACKWARD);
        }

        private void btnStepUp_Click(object sender, EventArgs e)
        {
            fMounter.Step(MOTOR.DE, STEPDIR.STEP_FORWARD);
        }

        private void btnStepDown_Click(object sender, EventArgs e)
        {
            fMounter.Step(MOTOR.DE, STEPDIR.STEP_BACKWARD);
        }

        private void btnSetPos_Click(object sender, EventArgs e)
        {
            if (rbHours.Checked)
                fMounter.PosRA_EQ =  Convert.ToDouble(tbRANewPos.Text);
            else
                fMounter.PosRA_EQ = Convert.ToDouble(tbRANewPos.Text)/15;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fMounter.PosDE_EQ = Convert.ToInt32(tbDENewPos.Text);
        }

        private void btnTime_Click(object sender, EventArgs e)
        {
            fMounter.RTC.DeviceDateTime = DateTime.Now;
        }

        private void DateTimeTimer_Tick(object sender, EventArgs e)
        {
            fMounter.RTC.GetDevDateTime();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPageModes.SelectedIndex == 3)
            {
                fMounter.RTC.DTTimer = 1000;
                fMounter.RTC.TemperatureTimer = 60000;
            }
            else  
            {
                if (fMounter.RTC.DTTimer!=0)
                    fMounter.RTC.DTTimer = 0;
                if (fMounter.RTC.TemperatureTimer !=0)
                    fMounter.RTC.TemperatureTimer = 0;
            }
        }


        private void btnRollRight_MouseDown(object sender, MouseEventArgs e)
        {
            fMounter.Roll(MOTOR.RA, STEPDIR.STEP_FORWARD);
        }

        private void btnRollRight_MouseUp(object sender, MouseEventArgs e)
        {
            fMounter.Stop(MOTOR.RA);
        }

        private void btnRollLeft_MouseDown(object sender, MouseEventArgs e)
        {
            fMounter.Roll(MOTOR.RA, STEPDIR.STEP_BACKWARD);
        }

        private void btnRollLeft_MouseUp(object sender, MouseEventArgs e)
        {
            fMounter.Stop(MOTOR.RA);
        }

        private void btnRollUp_MouseDown(object sender, MouseEventArgs e)
        {
            fMounter.Roll(MOTOR.DE, STEPDIR.STEP_FORWARD);
        }

        private void btnRollUp_MouseUp(object sender, MouseEventArgs e)
        {
            fMounter.Stop(MOTOR.DE);
        }

        private void btnRollDown_MouseDown(object sender, MouseEventArgs e)
        {
            fMounter.Roll(MOTOR.DE, STEPDIR.STEP_BACKWARD);
        }

        private void btnRollDown_MouseUp(object sender, MouseEventArgs e)
        {
            fMounter.Stop(MOTOR.DE);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.fGearRatioRA = Convert.ToDouble(tbRAGear.Text);
            ASCOM.FMounterV1.Properties.Settings.Default.RAGear = fMounter.fGearRatioRA;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)            
                fMounter.fGearRatioDE = Convert.ToDouble(tbDEGear.Text);
            ASCOM.FMounterV1.Properties.Settings.Default.DEGear = fMounter.fGearRatioDE;
        }

        private void cbIsTimeRunning_CheckedChanged(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)            
                fMounter.IsTimeRunning = cbIsTimeRunning.Checked;
            ASCOM.FMounterV1.Properties.Settings.Default.IsTimeRunning = cbIsTimeRunning.Checked;
        }

        private void btnSetReleaseTime_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.ReleaseTime = Convert.ToInt32(tbReleaseTime.Text);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            fMounter.Stop(MOTOR.RA);
            fMounter.Stop(MOTOR.DE);
            fMounter.Release(MOTOR.RA);
            fMounter.Release(MOTOR.DE);
        }

        private void rbDegrees_CheckedChanged(object sender, EventArgs e)
        {            
            tbRAPosEQ.Text = (fMounter.PosRA_EQ * 15).ToString("0.######");            
        }

        private void rbHours_CheckedChanged(object sender, EventArgs e)
        {
            tbRAPosEQ.Text = (fMounter.PosRA_EQ).ToString("0.######");            
        }

        private void cbRATurning_CheckedChanged(object sender, EventArgs e)
        {
            fMounter.RATurning = cbRATurning.Checked;
        }

        private void cbDETurning_CheckedChanged(object sender, EventArgs e)
        {
            fMounter.DETurning = cbDETurning.Checked;
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            double ra = 0, de = 0;
            if (rbHours.Checked) 
                ra = Convert.ToDouble(tbRANewPos.Text);        
            else 
                ra = Convert.ToDouble(tbRANewPos.Text)/15;        
    
            de = Convert.ToDouble(tbDENewPos.Text);

            fMounter.GotoPositionEQ(ra, de);
        }

        private void cbMicroStepNav_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.NavMicroStep = (byte)cbMicroStepNav.SelectedIndex;
        }

        private void cbMicroStepDaily_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.DailyMicroStep = (byte)cbMicroStepDaily.SelectedIndex;
        }

        private void btnSetCurrentNavSpeed_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.NavSpeed = Convert.ToDouble(tbNavSpeed.Text);
        }

        private void btnSetSpeedRATurning_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.DailySpeedRA = Convert.ToDouble(tbSpeedRADaily.Text);
        }

        private void btnSetSpeedDETurning_Click(object sender, EventArgs e)
        {
            if (fMounter.IsConnected)
                fMounter.DailySpeedDE = Convert.ToDouble(tbSpeedDEDaily.Text);
        }

        private bool IsChangingSpeed = false;
        private void pgSpeed_MouseDown(object sender, MouseEventArgs e)
        {
            if (!pgSpeed.Enabled)
                return;
            IsChangingSpeed = true;
            pgSpeed_MouseMove(sender, e);
        }

        private void pgSpeed_MouseMove(object sender, MouseEventArgs e)
        {
            if ((pgSpeed.Enabled) && (IsChangingSpeed) && (e.X>=0) && (e.X<=pgSpeed.Width))
            {                
                pgSpeed.Value = Convert.ToInt32(pgSpeed.Minimum + (Convert.ToDouble(e.X) / pgSpeed.Width) * (pgSpeed.Maximum - pgSpeed.Minimum));
            }
        }

        private void pgSpeed_MouseUp(object sender, MouseEventArgs e)
        {
            if (!pgSpeed.Enabled)
                return;
            IsChangingSpeed = false;            
            fMounter.NavSpeed = fMounter.NavSpeedMin + (fMounter.NavSpeedMax - fMounter.NavSpeedMin) * Convert.ToDouble(pgSpeed.Value) / (pgSpeed.Maximum - pgSpeed.Minimum);
        }

        private void UpdatePosBtn()
        {
            if (cbObjectPos.SelectedIndex==-1) 
            {
                btnDelete.Enabled = false;
                if (cbObjectPos.Text.Length==0) 
                    btnSave.Enabled = false;
                else
                    btnSave.Enabled = true;
            }
            else 
            {
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
            }

        }


        private void LoadCoordinates()
        {
            cbObjectPos.Items.Clear();
            if (ASCOM.FMounterV1.Properties.Settings.Default.PosList.Count == 0)
                return;
            for (int i = 0; i < ASCOM.FMounterV1.Properties.Settings.Default.PosList.Count; i++)
            {
                cbObjectPos.Items.Add(Convert.ToString(ASCOM.FMounterV1.Properties.Settings.Default.PosList[i]));
            }
        }

        private void SaveCoordinates()
        {

            ASCOM.FMounterV1.Properties.Settings.Default.PosList.Clear();
            for (int i = 0; i < cbObjectPos.Items.Count; i++)
                ASCOM.FMounterV1.Properties.Settings.Default.PosList.Add(Convert.ToString(cbObjectPos.Items[i]));

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbObjectPos.Text.Length==0) 
                    return;
            if (cbObjectPos.SelectedIndex < 0)
            {
                cbObjectPos.SelectedIndex = cbObjectPos.Items.Add(cbObjectPos.Text + " (" + fMounter.PosRA_EQ.ToString("0.######") + ";" + fMounter.PosDE_EQ.ToString("0.######") + ")");
            }
            else
            {
                string s;
                int i = cbObjectPos.Text.IndexOf("(");
                if (i==0) 
                    s = cbObjectPos.Text;
                else
                    s = cbObjectPos.Text.Substring(0, i-1);

                s = s + " (" + fMounter.PosRA_EQ.ToString("0.######") + ";" + fMounter.PosDE_EQ.ToString("0.######") + ")";
                cbObjectPos.Items[cbObjectPos.SelectedIndex] = s;
                cbObjectPos.SelectedIndex = cbObjectPos.Items.IndexOf(s);
            }
            SaveCoordinates();
            UpdatePosBtn();
        }

        private void cbObjectPos_TextChanged(object sender, EventArgs e)
        {
            UpdatePosBtn();
        }

        private void cbObjectPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbObjectPos.SelectedIndex!=-1)
            {
                string s;
                if (rbHours.Checked)
                {
                    s = (System.String)cbObjectPos.Items[cbObjectPos.SelectedIndex];
                    tbRANewPos.Text = s.Substring(s.IndexOf('('), s.IndexOf(';'));
                }
                else
                {
                    s = (System.String)cbObjectPos.Items[cbObjectPos.SelectedIndex];
                    s = s.Substring(s.IndexOf('(') + 1, s.IndexOf(';') - s.IndexOf('(')-1);
                    tbRANewPos.Text = (Convert.ToDouble(s) * 15).ToString("0.######");                    
                }
                s = (System.String)cbObjectPos.Items[cbObjectPos.SelectedIndex];
                tbDENewPos.Text = s.Substring(s.IndexOf(';') + 1, s.IndexOf(')')-s.IndexOf(';')-1);
                UpdatePosBtn();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbObjectPos.SelectedIndex < 0)
                return;
            
            cbObjectPos.Items.RemoveAt(cbObjectPos.SelectedIndex);

            if (cbObjectPos.Items.Count==0) 
                cbObjectPos.Text = "";
            else
                cbObjectPos.SelectedIndex =0;

            SaveCoordinates();
            UpdatePosBtn();
        }

        private void cbRAPower_CheckedChanged(object sender, EventArgs e)
        {
            fMounter.RAStepper.Power = cbRAPower.Checked;
        }

        private void cbDEPower_CheckedChanged(object sender, EventArgs e)
        {
            fMounter.DEStepper.Power = cbDEPower.Checked;
        }        

    }
}
