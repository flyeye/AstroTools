using System;
using System.Windows.Forms;
using ASCOM;

namespace ASCOM.FlyeyeFocuserV1
{
    public partial class Form1 : Form
    {

        private ASCOM.DriverAccess.Focuser driver;

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
            Properties.Settings.Default.DriverId = ASCOM.DriverAccess.Focuser.Choose(Properties.Settings.Default.DriverId);
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
                driver = new ASCOM.DriverAccess.Focuser(Properties.Settings.Default.DriverId);
                try
                {
                    driver.Connected = true;
                }
                catch (ASCOM.NotConnectedException ex)
                {
                    //                    LogBox.AppendText(ex.Message + "\r\n");
                    return;
                }
            }
            SetUIState();
            timer1.Enabled = true;
        }

        private void SetUIState()
        {
            buttonConnect.Enabled = !string.IsNullOrEmpty(Properties.Settings.Default.DriverId);            
            buttonChoose.Enabled = !IsConnected;
            btnLeft.Enabled = IsConnected;
            btnRight.Enabled = IsConnected;
            btnStop.Enabled = IsConnected;
            tbPosition.Enabled = IsConnected;
            cbIsMoving.Enabled = IsConnected;
            buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
        }

        private bool IsConnected
        {
            get
            {
                return ((this.driver != null) && (driver.Connected == true));
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
               
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (driver.Connected == true)
            {
                tbPosition.Text = Convert.ToString(driver.Position);
                cbIsMoving.Checked = driver.IsMoving;
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (driver.Connected == true)
            {
                driver.Move(driver.Position - 8);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (driver.Connected == true)
            {
                driver.Halt();
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (driver.Connected == true)
            {
                driver.Move(driver.Position + 8);
            }
        }
    }
}
