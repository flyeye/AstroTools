using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.FMounterV1;

namespace ASCOM.FMounterV1
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile 
            textBox1.Text = Telescope.comPort;
            chkTrace.Checked = Telescope.traceState;
            tbBaudrate.Text = Convert.ToString(Telescope.baudRate);
            tbApertureArea.Text = Convert.ToString(Telescope.fApertureArea);
            tbApertureDiameter.Text = Convert.ToString(Telescope.fApertureDiameter);
            tbFocalLength.Text = Convert.ToString(Telescope.fFocalLength);
            tbElevation.Text = Convert.ToString(Telescope.fElevation);
            tbLatitude.Text = Convert.ToString(Telescope.fLatitude);
            tbLongitude.Text = Convert.ToString(Telescope.fLongitude);
            tbRAGear.Text = Convert.ToString(Telescope.fRAGear);
            tbDEGear.Text = Convert.ToString(Telescope.fDEGear);
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here

            Telescope.comPort = textBox1.Text; // Update the state variables with results from the dialogue
            Telescope.traceState = chkTrace.Checked;
            Telescope.baudRate = Convert.ToInt32(tbBaudrate.Text);
            Telescope.fApertureArea = Convert.ToDouble(tbApertureArea.Text);
            Telescope.fApertureDiameter = Convert.ToDouble(tbApertureDiameter.Text);
            Telescope.fFocalLength = Convert.ToDouble(tbFocalLength.Text);
            Telescope.fElevation = Convert.ToDouble(tbElevation.Text);
            Telescope.fLatitude = Convert.ToDouble(tbLatitude.Text);
            Telescope.fLongitude = Convert.ToDouble(tbLongitude.Text);
            Telescope.fRAGear = Convert.ToDouble(tbRAGear.Text);
            Telescope.fDEGear = Convert.ToDouble(tbDEGear.Text);
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}