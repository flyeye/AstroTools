using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FConnection;

namespace WindowsFormsApplication1
{
    public partial class FocuserControlPanelForm : Form
    {

        public TFFocuser fFocuser;
        int baudRate = 115200;
        bool IsSpeedChanging = false;
        
        public FocuserControlPanelForm()
        {
            InitializeComponent();
            cbPort.SelectedIndex = 0;            

            fFocuser = new TFFocuser(false, this);

            fFocuser.OnPingEvent += new EventHandler(fFocuser_OnPingEvent);         
            fFocuser.OnHandshakeEvent += new EventHandler(fFocuser_OnHandshakeEvent);
            fFocuser.OnFirmwareEvent += new EventHandler(fFocuser_OnFirmwareEvent);
            fFocuser.OnRPCEvent += new EventHandler(fFocuser_OnRPCEvent);
            fFocuser.OnDebugEvent += new EventHandler(fFocuser_OnDebugEvent);
            fFocuser.OnDebugMsgEvent += new EventHandler(fFocuser_OnDebugMsgEvent);

            fFocuser.OnPositionChangedEvent += new EventHandler(fFocuser_OnChangePositionEvent);
            fFocuser.OnMinPositionChangedEvent += new EventHandler(fFocuser_OnMinPositionChangedEvent);
            fFocuser.OnMaxPositionChangedEvent += new EventHandler(fFocuser_OnMaxPositionChangedEvent);
            fFocuser.OnMotorStopEvent += new EventHandler(fFocuser_OnMotorStopEvent);
            fFocuser.OnMotorStepRightEvent += new EventHandler(fFocuser_OnMotorStepRightEvent);
            fFocuser.OnMotorStepLeftEvent += new EventHandler(fFocuser_OnMotorStepLeftEvent);
            fFocuser.OnPowerEvent += new EventHandler(fFocuser_OnPowerEvent);
            fFocuser.OnRollingEvent += new EventHandler(fFocuser_OnRollingEvent);
            fFocuser.OnRollingToNewPosEvent += new TFFocuser.OnRollingToNewPosHandler(fFocuser_OnRollingToNewPosEvent);
            fFocuser.OnSpeedChangedEvent += new EventHandler(fFocuser_OnSpeedChangedEvent);
            fFocuser.OnMaxSpeedChangedEvent += new EventHandler(fFocuser_OnMaxSpeedChangedEvent);
            fFocuser.OnMinSpeedChangedEvent += new EventHandler(fFocuser_OnMinSpeedChangedEvent);
            fFocuser.OnReleaseTimeChangeEvent += new EventHandler(fFocuser_OnReleaseTimeChange);
            fFocuser.OnRangeCheckEvent += new EventHandler(fFocuser_OnRangeCheckEvent);
            fFocuser.OnMicrostepEvent += new EventHandler(fFocuser_OnMicrostepEvent);
        }

        void fFocuser_OnRollingToNewPosEvent(Int64 is_rolling)
        {
            LogBox.AppendText("New position: " + Convert.ToString(is_rolling) + "\r\n");
        }

        void fFocuser_OnMicrostepEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Microstep: " + Convert.ToString(fFocuser.MicroStep) + "\r\n");
            cbMicroStep.SelectedIndex = fFocuser.MicroStep;
        }

        void fFocuser_OnMaxPositionChangedEvent(object sender, EventArgs e)
        {
            UpdatePositionBar();
            tbMaxPos.Text = Convert.ToString(fFocuser.MaxPosition);
            LogBox.AppendText("Max pos = " + Convert.ToString(fFocuser.MaxPosition) + "\r\n");
        }

        void fFocuser_OnMinPositionChangedEvent(object sender, EventArgs e)
        {
            UpdatePositionBar();
            tbMinPos.Text = Convert.ToString(fFocuser.MinPosition);
            LogBox.AppendText("Min pos = " + Convert.ToString(fFocuser.MinPosition) + "\r\n");            
        }

        void fFocuser_OnRangeCheckEvent(object sender, EventArgs e)
        {
            cbRangeCheck.Checked = fFocuser.IsRangeCheck;
            LogBox.AppendText("Range check: " + Convert.ToString(fFocuser.IsRangeCheck) + "\r\n");
        }

        void fFocuser_OnMinSpeedChangedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Min speed delay: " + Convert.ToString(fFocuser.MinStepDelay) + "\r\n");            
        }

        void fFocuser_OnMaxSpeedChangedEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Max speed delay: " + Convert.ToString(fFocuser.MaxStepDelay) + "\r\n");
        }

        void fFocuser_OnReleaseTimeChange(object sender, EventArgs e)
        {
            tbReleaseTime.Text = Convert.ToString(fFocuser.ReleaseTime);
            LogBox.AppendText("Release time: " + Convert.ToString(fFocuser.ReleaseTime) + "\r\n");
        }

        void fFocuser_OnSpeedChangedEvent(object sender, EventArgs e)
        {
            tbSpeed.Text = Convert.ToString((Int64)1000000 / fFocuser.StepDelay);
            pgSpeed.Value = Convert.ToInt32(100 * (Math.Max(fFocuser.MinSpeed, Math.Min(fFocuser.MaxSpeed, fFocuser.CurrentSpeed)) / (fFocuser.MaxSpeed - fFocuser.MinSpeed)));             
            LogBox.AppendText("Step delay: " + Convert.ToString(fFocuser.StepDelay) + " \r\n");
        }

        void fFocuser_OnRollingEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Rolling... \r\n");
        }

        void fFocuser_OnPowerEvent(object sender, EventArgs e)
        {
            string s = "Power is ";
            if (fFocuser.Power)
                s += "ON";
            else
                s += "OFF";
            LogBox.AppendText(s + "\r\n");
            cbPower.Checked = fFocuser.Power;
        }

        void fFocuser_OnMotorStepLeftEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Step left\r\n");            
        }

        void fFocuser_OnMotorStepRightEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Step right\r\n");
        }

        void fFocuser_OnMotorStopEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Stop\r\n");
        }

        void UpdatePositionBar()
        {
            try
            {
                pgPosition.Value = Math.Max(Math.Min(Convert.ToInt32(100 * (fFocuser.Position - fFocuser.MinPosition) / (fFocuser.MaxPosition - fFocuser.MinPosition)), 100), 0);
            }
            catch (Exception ex) { }
        }

        void fFocuser_OnChangePositionEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Pos: " + Convert.ToString(fFocuser.Position) + "\r\n");
            tbPosition.Text = Convert.ToString(fFocuser.Position);
            UpdatePositionBar();
        }

        void fFocuser_OnDebugMsgEvent(object sender, EventArgs e) 
        {
            string s = "";
            while (fFocuser.GetDebugMsg(out s))                           
                LogBox.AppendText("Debug msg: " + s + "\r\n");                                    
        }
        
        void fFocuser_OnDebugEvent(object sender, EventArgs e)
        {
            string s = "Debug mode is ";
            if (fFocuser.IsDebugOn)
                s += "ON";
            else
                s += "OFF";
            LogBox.AppendText(s + "\r\n");
            cbDebug.Checked = fFocuser.IsDebugOn;
        }

        void fFocuser_OnRPCEvent(object sender, EventArgs e)
        {
            string s = "Remote panel is ";
            if (fFocuser.IsRCP_On)
                s += "ON";
            else
                s += "OFF";
            LogBox.AppendText( s + "\r\n");
            cbRCP.Checked = fFocuser.IsRCP_On;          
        }

        void fFocuser_OnFirmwareEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("Firmware version: " + fFocuser.DeviceVersion + "\r\n");
        }

        void fFocuser_OnHandshakeEvent(object sender, EventArgs e)
        {            
            switch (fFocuser.DeviceType)
            {
                case FDEV.FD_FOCUSER:
                    LogBox.AppendText("Focuser connected.\n");
                    break;
                case FDEV.FD_MOUNTER:
                    LogBox.AppendText("Mounter is attached! Please, re-connect Focuser!\n");
                    btnDisconnect_Click(this, new EventArgs());              
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

        void fFocuser_OnPingEvent(object sender, EventArgs e)
        {
            LogBox.AppendText("+ \r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnDisconnect_Click(sender, e);
            Application.Exit();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                fFocuser.Connect(cbPort.Text, baudRate);
                //fFocuser.MaxPosition = 20000;
                //fFocuser.MinPosition = -20000;
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

            if (fFocuser.IsConnected)
            {
                btnConnect.Enabled = false;
                cbPort.Enabled = false;
                btnDisconnect.Enabled = true;

                btnGoTo.Enabled = true;
                btnPing.Enabled = true;
                btnSetMinPos.Enabled = true;
                btnRollLeft.Enabled = true;
                btnRollRight.Enabled = true;
                btnSetReleaseTime.Enabled = true;
                btnSetSpeed.Enabled = true;
                btnStepLeft.Enabled = true;
                btnStepRight.Enabled = true;
                btnStop.Enabled = true;
                tbCmd.Enabled = true;
                cbNewPos.Enabled = true;
                tbPosition.Enabled = true;
                tbReleaseTime.Enabled = true;
                tbSpeed.Enabled = true;
                cbDebug.Enabled = true;
                cbMicroStep.Enabled = true;
                cbMicroStep.Enabled = true;
                cbPower.Enabled = true;
                cbRangeCheck.Enabled = true;
                cbRCP.Enabled = true;
                btnSend.Enabled = true;
                pgPosition.Enabled = true;
                pgSpeed.Enabled = true;
                btnSetMax.Enabled = true;
                btnSetMinPos.Enabled = true;
                btnSetPos.Enabled = true;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;

                cbNewPos.Items.Clear();
                for (int i = 0; i < ASCOM.FlyeyeFocuserV1.Properties.Settings.Default.PosList.Count; i++)
                {
                    cbNewPos.Items.Add(Convert.ToString(ASCOM.FlyeyeFocuserV1.Properties.Settings.Default.PosList[i]));
                }
                ASCOM.FlyeyeFocuserV1.Properties.Settings.Default.ComPort = cbPort.Text;                
            }            
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (!fFocuser.IsConnected)
                return;
            fFocuser.Disconnect();

            btnConnect.Enabled = true;
            cbPort.Enabled = true;
            btnDisconnect.Enabled = false;

            btnGoTo.Enabled = false;
            btnPing.Enabled = false;
            btnSetMinPos.Enabled = false;
            btnRollLeft.Enabled = false;
            btnRollRight.Enabled = false;
            btnSetReleaseTime.Enabled = false;
            btnSetSpeed.Enabled = false;
            btnStepLeft.Enabled = false;
            btnStepRight.Enabled = false;
            btnStop.Enabled = false;
            tbCmd.Enabled = false;
            cbNewPos.Enabled = false;
            tbPosition.Enabled = false;
            tbReleaseTime.Enabled = false;
            tbSpeed.Enabled = false;
            cbDebug.Enabled = false;
            cbMicroStep.Enabled = false;
            cbMicroStep.Enabled = false;
            cbPower.Enabled = false;
            cbRangeCheck.Enabled = false;
            cbRCP.Enabled = false;
            btnSend.Enabled = false;
            pgPosition.Enabled = false;
            pgSpeed.Enabled = false;
            btnSetMax.Enabled = false;
            btnSetMinPos.Enabled = false;
            btnSetPos.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;

            ASCOM.FlyeyeFocuserV1.Properties.Settings.Default.PosList.Clear();
            for (int i = 0; i < cbNewPos.Items.Count; i++) 
            {
                ASCOM.FlyeyeFocuserV1.Properties.Settings.Default.PosList.Add(Convert.ToString(cbNewPos.Items[i]));
            }
            ASCOM.FlyeyeFocuserV1.Properties.Settings.Default.Save();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (fFocuser.IsConnected)
                fFocuser.SendData(Convert.ToByte(tbCmd.Text));
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            if (fFocuser.IsConnected)
                fFocuser.Ping();
        }

        private void FocuserControlPanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (fFocuser.IsConnected)
                fFocuser.Disconnect();
        }

        private void cbDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (fFocuser.IsConnected) 
                fFocuser.IsDebugOn = cbDebug.Checked;
        }

        private void cbRCP_CheckedChanged(object sender, EventArgs e)
        {
            if (fFocuser.IsConnected) 
                fFocuser.IsRCP_On = cbRCP.Checked;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (fFocuser.IsConnected)
                fFocuser.Position = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LogBox.Clear();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            fFocuser.Stop();
        }

        private void btnStepRight_Click(object sender, EventArgs e)
        {
            fFocuser.StepRight();
        }

        private void btnStepLeft_Click(object sender, EventArgs e)
        {
            fFocuser.StepLeft();
        }

        private void cbPower_CheckedChanged(object sender, EventArgs e)
        {
            fFocuser.Power = cbPower.Checked;
        }

        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            fFocuser.StepDelay = (Int64)1000000 / Convert.ToInt64(tbSpeed.Text);
        }

        private void btnRollRight_MouseDown(object sender, MouseEventArgs e)
        {
            fFocuser.RollRight();
        }

        private void btnRollRight_MouseUp(object sender, MouseEventArgs e)
        {
            fFocuser.Stop();
        }

        private void btnRollLeft_MouseDown(object sender, MouseEventArgs e)
        {
            fFocuser.RollLeft();
        }

        private void btnRollLeft_MouseUp(object sender, MouseEventArgs e)
        {
            fFocuser.Stop();
        }

        private void btnSetReleaseTime_Click(object sender, EventArgs e)
        {
            fFocuser.ReleaseTime = Convert.ToInt32(tbReleaseTime.Text);
        }

        private void cbRangeCheck_CheckedChanged(object sender, EventArgs e)
        {
            fFocuser.IsRangeCheck = cbRangeCheck.Checked;
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbNewPos.SelectedIndex == -1)
                    fFocuser.Goto(Convert.ToInt64(cbNewPos.Text));
                else
                {
                    String str = (cbNewPos.Text.Remove(cbNewPos.Text.IndexOf(')'))).Remove(0, cbNewPos.Text.IndexOf('(') + 1);
                    fFocuser.Goto(Convert.ToInt64(str));
                }
            }
            catch (Exception ex)
            { 
            } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fFocuser.MicroStep = (byte)cbMicroStep.SelectedIndex;
        }

        private void pgSpeed_MouseDown(object sender, MouseEventArgs e)
        {
            if (pgSpeed.Enabled)
            {
                IsSpeedChanging = true;
                pgSpeed_MouseMove(sender, e);
            }
        }

        private void pgSpeed_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsSpeedChanging)
            {
                if (e.X <= 0)
                    pgSpeed.Value = 0;
                else if (e.X > pgSpeed.Width)
                    pgSpeed.Value = 100;
                else 
                    pgSpeed.Value = Convert.ToInt32(100 * e.X / pgSpeed.Width);
            }
        }

        private void pgSpeed_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsSpeedChanging)
            {
                IsSpeedChanging = false;
                if (pgSpeed.Value > 0)
                    fFocuser.StepDelay = Convert.ToInt64(1000000.0 / (((double)pgSpeed.Value / 100.0) * (fFocuser.MaxSpeed - fFocuser.MinSpeed)));
                else
                    fFocuser.StepDelay = fFocuser.MinStepDelay;
            }
        }

        private void FocuserControlPanelForm_Shown(object sender, EventArgs e)
        {
            cbPort.SelectedIndex = cbPort.Items.IndexOf(ASCOM.FlyeyeFocuserV1.Properties.Settings.Default.ComPort);
        }

        private void btnSetMax_Click(object sender, EventArgs e)
        {
            fFocuser.MaxPosition = Convert.ToInt64(tbPosition.Text);
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            fFocuser.MinPosition = Convert.ToInt64(tbPosition.Text);
        }      

        private void btnSave_Click(object sender, EventArgs e)
        {                        
            String str = cbNewPos.Text.Remove(cbNewPos.Text.IndexOf('(')) + "(" + Convert.ToString(fFocuser.Position) + ')';
            if (cbNewPos.SelectedIndex != -1)
                cbNewPos.Items.RemoveAt(cbNewPos.SelectedIndex);
            cbNewPos.Items.Add(str);
            cbNewPos.SelectedIndex = cbNewPos.Items.IndexOf(str);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbNewPos.SelectedIndex >= 0)
            {
                cbNewPos.Items.RemoveAt(cbNewPos.SelectedIndex);                
            }
            if (cbNewPos.Items.Count==0)
                cbNewPos.Text = "0";

        }

        private void btnPosSet_Click(object sender, EventArgs e)
        {
            try
            {

                if (cbNewPos.SelectedIndex == -1)
                    fFocuser.Position = Convert.ToInt64(cbNewPos.Text);
                else
                {
                    String str = "";
                    if (cbNewPos.Text.IndexOf('(') == -1)
                        str = cbNewPos.Text;
                    else 
                        str = (cbNewPos.Text.Remove(cbNewPos.Text.IndexOf(')'))).Remove(0, cbNewPos.Text.IndexOf('(') + 1);
                    fFocuser.Position = Convert.ToInt64(str);
                }
            }
            catch (Exception ex) { }
        }

        private void cbNewPos_TextChanged(object sender, EventArgs e)
        {
            if (cbNewPos.SelectedIndex != -1)
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;
        }

        private void btnSetMinPos_Click(object sender, EventArgs e)
        {
            fFocuser.MinPosition = Convert.ToInt64(tbMinPos.Text);
        }

        private void btnSetMax_Click_1(object sender, EventArgs e)
        {
            fFocuser.MaxPosition = Convert.ToInt64(tbMaxPos.Text);
        }        
        
    }
}
