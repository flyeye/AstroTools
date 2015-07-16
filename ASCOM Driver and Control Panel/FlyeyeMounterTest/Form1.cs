using System;
using System.Windows.Forms;

namespace ASCOM.FMounterV1
{
    public partial class Form1 : Form
    {

        private ASCOM.DriverAccess.Telescope driver;

        public Form1()
        {
            InitializeComponent();
            SetUIState();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
                driver.Connected = false;

            Properties.Settings.Default.Save();
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DriverId = ASCOM.DriverAccess.Telescope.Choose(Properties.Settings.Default.DriverId);
            SetUIState();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                timer1.Enabled = false;
                driver.Connected = false;
            }
            else
            {
                try
                {
                    driver = new ASCOM.DriverAccess.Telescope(Properties.Settings.Default.DriverId);
                    driver.Connected = true;
                }
                catch (System.IO.IOException ex)
                {
                    //ShowDialog("Connected Set", ex.Message + "\r\n");
                }
                catch (UnauthorizedAccessException ex)
                {
                    //tl.LogMessage("Connected Set", ex.Message + "\r\n");
                }
                catch (System.InvalidOperationException ex)
                {
                   //.LogMessage("Connected Set", ex.Message + "\r\n");
                }
                
                if (driver.Connected)
                    timer1.Enabled = true;
            }
            SetUIState();
        }

        private void SetUIState()
        {
            buttonConnect.Enabled = !string.IsNullOrEmpty(Properties.Settings.Default.DriverId);
            buttonChoose.Enabled = !IsConnected;
            buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
        }

        private bool IsConnected
        {
            get
            {
                return ((this.driver != null) && (driver.Connected == true));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                cbSlewing.Checked = driver.Slewing;
                cbTracking.Checked = driver.Tracking;
                tbRAPos.Text = Convert.ToString(driver.RightAscension);
                tbDEPos.Text = Convert.ToString(driver.Declination);
                tbAzimuth.Text = Convert.ToString(driver.Azimuth);
                tbElevation.Text = Convert.ToString(driver.Altitude);
                if (cbTrackSpeed.SelectedIndex != (byte)driver.TrackingRate)
                    cbTrackSpeed.SelectedIndex = (byte)driver.TrackingRate;
                tbDESpeed.Text = Convert.ToString(driver.DeclinationRate);
                double speed = driver.RightAscensionRate;
                switch (driver.TrackingRate) { 
                    case DeviceInterface.DriveRates.driveSidereal:
                    case DeviceInterface.DriveRates.driveSolar:
                        speed += 15.0;
                        break;
                    case DeviceInterface.DriveRates.driveLunar:
                        speed += 14.685;
                        break;                        
                }
                tbRASpeed.Text = Convert.ToString(speed);
                tbSideral.Text = driver.SiderealTime.ToString();
                dateTimeUNC.Value = driver.UTCDate;
            }
        }

        private void cbTracking_CheckedChanged(object sender, EventArgs e)
        {
             if (IsConnected)
             {
                 driver.Tracking = cbTracking.Checked;
             }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            driver.AbortSlew();
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisPrimary, Convert.ToDouble(tbNavSpeed.Text));
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisPrimary, 0);
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisPrimary, -1*Convert.ToDouble(tbNavSpeed.Text));
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisPrimary, 0);
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisSecondary, Convert.ToDouble(tbNavSpeed.Text));
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisSecondary,-1 *Convert.ToDouble(tbNavSpeed.Text));
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisSecondary, 0);
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            driver.MoveAxis(DeviceInterface.TelescopeAxes.axisSecondary, 0);
        }

        private void cbTrackSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            driver.TrackingRate = (DeviceInterface.DriveRates)cbTrackSpeed.SelectedIndex;                        
        }

        private void btnSetOffset_Click(object sender, EventArgs e)
        {
            driver.RightAscensionRate = Convert.ToDouble(tbRAOffset.Text);
            driver.DeclinationRate = Convert.ToDouble(tbDEOffset.Text);
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            driver.SlewToCoordinatesAsync(Convert.ToDouble(tbNewRA.Text), Convert.ToDouble(tbNewDE.Text));            
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            driver.SyncToCoordinates(Convert.ToDouble(tbNewRA.Text), Convert.ToDouble(tbNewDE.Text));
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            driver.FindHome();
        }

        private void btnStepRight_Click(object sender, EventArgs e)
        {
            driver.GuideRateDeclination = Convert.ToDouble(tbNavSpeed.Text);
            driver.GuideRateRightAscension = Convert.ToDouble(tbNavSpeed.Text);
            driver.PulseGuide(DeviceInterface.GuideDirections.guideEast, 500);
        }

        private void btnStepLeft_Click(object sender, EventArgs e)
        {
            driver.GuideRateDeclination = Convert.ToDouble(tbNavSpeed.Text);
            driver.GuideRateRightAscension = Convert.ToDouble(tbNavSpeed.Text);
            driver.PulseGuide(DeviceInterface.GuideDirections.guideWest, 500);
        }

        private void btnStepDown_Click(object sender, EventArgs e)
        {
            driver.GuideRateDeclination = Convert.ToDouble(tbNavSpeed.Text);
            driver.GuideRateRightAscension = Convert.ToDouble(tbNavSpeed.Text);
            driver.PulseGuide(DeviceInterface.GuideDirections.guideSouth, 500);
        }

        private void btnStepUp_MouseClick(object sender, MouseEventArgs e)
        {
            driver.GuideRateDeclination = Convert.ToDouble(tbNavSpeed.Text);
            driver.GuideRateRightAscension = Convert.ToDouble(tbNavSpeed.Text);
            driver.PulseGuide(DeviceInterface.GuideDirections.guideNorth, 500);
        }
    }
}
