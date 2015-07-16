namespace FlyeyeMounterControlPanel
{
    partial class MounterControlPanelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MounterControlPanelForm));
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabPageModes = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSetPosDE = new System.Windows.Forms.Button();
            this.btnStepUp = new System.Windows.Forms.Button();
            this.btnStepDown = new System.Windows.Forms.Button();
            this.btnRollDown = new System.Windows.Forms.Button();
            this.btnRollUp = new System.Windows.Forms.Button();
            this.btnSetCurrentNavSpeed = new System.Windows.Forms.Button();
            this.tbNavSpeed = new System.Windows.Forms.TextBox();
            this.tbDENewPos = new System.Windows.Forms.TextBox();
            this.tbRANewPos = new System.Windows.Forms.TextBox();
            this.tbDEPos = new System.Windows.Forms.TextBox();
            this.tbRAPos = new System.Windows.Forms.TextBox();
            this.tbDEPosEQ = new System.Windows.Forms.TextBox();
            this.cbDETurning = new System.Windows.Forms.CheckBox();
            this.cbRATurning = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetSpeedDETurning = new System.Windows.Forms.Button();
            this.tbSpeedDEDaily = new System.Windows.Forms.TextBox();
            this.btnSetPosRA = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetSpeedRATurning = new System.Windows.Forms.Button();
            this.tbSpeedRADaily = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbObjectPos = new System.Windows.Forms.ComboBox();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.pgSpeed = new System.Windows.Forms.ProgressBar();
            this.btnStepLeft = new System.Windows.Forms.Button();
            this.btnRollLeft = new System.Windows.Forms.Button();
            this.btnStepRight = new System.Windows.Forms.Button();
            this.btnRollRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRAPosEQ = new System.Windows.Forms.TextBox();
            this.cbRCP = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbMaxNavSpeed = new System.Windows.Forms.TextBox();
            this.btnSetNavSpeed = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbMinNavSpeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMicroStepNav = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbDEMaxDailySpeed = new System.Windows.Forms.TextBox();
            this.btnSetDEDailySpeed = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.tbDEMinDailySpeed = new System.Windows.Forms.TextBox();
            this.tbRAMaxDailySpeed = new System.Windows.Forms.TextBox();
            this.btnSetRADailySpeed = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRAMinDailySpeed = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbMicroStepDaily = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRAPower = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSetRAGear = new System.Windows.Forms.Button();
            this.tbRAGear = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbRAPosMax = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btnSetRAMaxMinPos = new System.Windows.Forms.Button();
            this.tbRAPosMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbReleaseTime = new System.Windows.Forms.TextBox();
            this.btnSetReleaseTime = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDEPower = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSetDEGear = new System.Windows.Forms.Button();
            this.tbDEGear = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbDEPosMax = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSetDEMaxMinPos = new System.Windows.Forms.Button();
            this.tbDEPosMin = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.rbDegrees = new System.Windows.Forms.RadioButton();
            this.rbHours = new System.Windows.Forms.RadioButton();
            this.cbIsTimeRunning = new System.Windows.Forms.CheckBox();
            this.tbTemp = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnTime = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnPing = new System.Windows.Forms.Button();
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbAbout = new System.Windows.Forms.TextBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.tabPageModes.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPort
            // 
            this.cbPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.cbPort.Location = new System.Drawing.Point(146, 3);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(90, 21);
            this.cbPort.TabIndex = 7;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(242, 32);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 6;
            this.btnDisconnect.Text = "Close";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(242, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabPageModes
            // 
            this.tabPageModes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPageModes.Controls.Add(this.tabPage1);
            this.tabPageModes.Controls.Add(this.tabPage6);
            this.tabPageModes.Controls.Add(this.tabPage3);
            this.tabPageModes.Controls.Add(this.tabPage5);
            this.tabPageModes.Controls.Add(this.tabPage2);
            this.tabPageModes.Controls.Add(this.tabPage4);
            this.tabPageModes.Location = new System.Drawing.Point(4, 61);
            this.tabPageModes.Name = "tabPageModes";
            this.tabPageModes.SelectedIndex = 0;
            this.tabPageModes.Size = new System.Drawing.Size(313, 292);
            this.tabPageModes.TabIndex = 6;
            this.tabPageModes.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSetPosDE);
            this.tabPage1.Controls.Add(this.btnStepUp);
            this.tabPage1.Controls.Add(this.btnStepDown);
            this.tabPage1.Controls.Add(this.btnRollDown);
            this.tabPage1.Controls.Add(this.btnRollUp);
            this.tabPage1.Controls.Add(this.btnSetCurrentNavSpeed);
            this.tabPage1.Controls.Add(this.tbNavSpeed);
            this.tabPage1.Controls.Add(this.tbDENewPos);
            this.tabPage1.Controls.Add(this.tbRANewPos);
            this.tabPage1.Controls.Add(this.tbDEPos);
            this.tabPage1.Controls.Add(this.tbRAPos);
            this.tabPage1.Controls.Add(this.tbDEPosEQ);
            this.tabPage1.Controls.Add(this.cbDETurning);
            this.tabPage1.Controls.Add(this.cbRATurning);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnSetSpeedDETurning);
            this.tabPage1.Controls.Add(this.tbSpeedDEDaily);
            this.tabPage1.Controls.Add(this.btnSetPosRA);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnSetSpeedRATurning);
            this.tabPage1.Controls.Add(this.tbSpeedRADaily);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.cbObjectPos);
            this.tabPage1.Controls.Add(this.btnGoTo);
            this.tabPage1.Controls.Add(this.pgSpeed);
            this.tabPage1.Controls.Add(this.btnStepLeft);
            this.tabPage1.Controls.Add(this.btnRollLeft);
            this.tabPage1.Controls.Add(this.btnStepRight);
            this.tabPage1.Controls.Add(this.btnRollRight);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbRAPosEQ);
            this.tabPage1.Controls.Add(this.cbRCP);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(305, 266);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mounter";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSetPosDE
            // 
            this.btnSetPosDE.Enabled = false;
            this.btnSetPosDE.Location = new System.Drawing.Point(108, 232);
            this.btnSetPosDE.Name = "btnSetPosDE";
            this.btnSetPosDE.Size = new System.Drawing.Size(55, 23);
            this.btnSetPosDE.TabIndex = 40;
            this.btnSetPosDE.Text = "Set";
            this.btnSetPosDE.UseVisualStyleBackColor = true;
            this.btnSetPosDE.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnStepUp
            // 
            this.btnStepUp.Enabled = false;
            this.btnStepUp.Location = new System.Drawing.Point(61, 9);
            this.btnStepUp.Margin = new System.Windows.Forms.Padding(1);
            this.btnStepUp.Name = "btnStepUp";
            this.btnStepUp.Size = new System.Drawing.Size(43, 22);
            this.btnStepUp.TabIndex = 39;
            this.btnStepUp.Text = "^";
            this.btnStepUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStepUp.UseVisualStyleBackColor = true;
            this.btnStepUp.Click += new System.EventHandler(this.btnStepUp_Click);
            // 
            // btnStepDown
            // 
            this.btnStepDown.Enabled = false;
            this.btnStepDown.Location = new System.Drawing.Point(61, 104);
            this.btnStepDown.Margin = new System.Windows.Forms.Padding(1);
            this.btnStepDown.Name = "btnStepDown";
            this.btnStepDown.Size = new System.Drawing.Size(43, 22);
            this.btnStepDown.TabIndex = 38;
            this.btnStepDown.Text = "v";
            this.btnStepDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStepDown.UseVisualStyleBackColor = true;
            this.btnStepDown.Click += new System.EventHandler(this.btnStepDown_Click);
            // 
            // btnRollDown
            // 
            this.btnRollDown.Enabled = false;
            this.btnRollDown.Location = new System.Drawing.Point(61, 80);
            this.btnRollDown.Name = "btnRollDown";
            this.btnRollDown.Size = new System.Drawing.Size(43, 23);
            this.btnRollDown.TabIndex = 37;
            this.btnRollDown.Text = "vv";
            this.btnRollDown.UseVisualStyleBackColor = true;
            this.btnRollDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRollDown_MouseDown);
            this.btnRollDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRollDown_MouseUp);
            // 
            // btnRollUp
            // 
            this.btnRollUp.Enabled = false;
            this.btnRollUp.Location = new System.Drawing.Point(61, 32);
            this.btnRollUp.Name = "btnRollUp";
            this.btnRollUp.Size = new System.Drawing.Size(43, 23);
            this.btnRollUp.TabIndex = 36;
            this.btnRollUp.Text = "^^";
            this.btnRollUp.UseVisualStyleBackColor = true;
            this.btnRollUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRollUp_MouseDown);
            this.btnRollUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRollUp_MouseUp);
            // 
            // btnSetCurrentNavSpeed
            // 
            this.btnSetCurrentNavSpeed.Enabled = false;
            this.btnSetCurrentNavSpeed.Location = new System.Drawing.Point(264, 33);
            this.btnSetCurrentNavSpeed.Name = "btnSetCurrentNavSpeed";
            this.btnSetCurrentNavSpeed.Size = new System.Drawing.Size(35, 23);
            this.btnSetCurrentNavSpeed.TabIndex = 35;
            this.btnSetCurrentNavSpeed.Text = "Set";
            this.btnSetCurrentNavSpeed.UseVisualStyleBackColor = true;
            this.btnSetCurrentNavSpeed.Click += new System.EventHandler(this.btnSetCurrentNavSpeed_Click);
            // 
            // tbNavSpeed
            // 
            this.tbNavSpeed.Enabled = false;
            this.tbNavSpeed.Location = new System.Drawing.Point(201, 35);
            this.tbNavSpeed.Name = "tbNavSpeed";
            this.tbNavSpeed.Size = new System.Drawing.Size(61, 20);
            this.tbNavSpeed.TabIndex = 34;
            // 
            // tbDENewPos
            // 
            this.tbDENewPos.Enabled = false;
            this.tbDENewPos.Location = new System.Drawing.Point(97, 206);
            this.tbDENewPos.Name = "tbDENewPos";
            this.tbDENewPos.Size = new System.Drawing.Size(76, 20);
            this.tbDENewPos.TabIndex = 33;
            this.tbDENewPos.Text = "90";
            // 
            // tbRANewPos
            // 
            this.tbRANewPos.Enabled = false;
            this.tbRANewPos.Location = new System.Drawing.Point(12, 206);
            this.tbRANewPos.Name = "tbRANewPos";
            this.tbRANewPos.Size = new System.Drawing.Size(79, 20);
            this.tbRANewPos.TabIndex = 32;
            this.tbRANewPos.Text = "0";
            // 
            // tbDEPos
            // 
            this.tbDEPos.Enabled = false;
            this.tbDEPos.Location = new System.Drawing.Point(97, 154);
            this.tbDEPos.Name = "tbDEPos";
            this.tbDEPos.ReadOnly = true;
            this.tbDEPos.Size = new System.Drawing.Size(76, 20);
            this.tbDEPos.TabIndex = 31;
            // 
            // tbRAPos
            // 
            this.tbRAPos.Enabled = false;
            this.tbRAPos.Location = new System.Drawing.Point(12, 154);
            this.tbRAPos.Name = "tbRAPos";
            this.tbRAPos.ReadOnly = true;
            this.tbRAPos.Size = new System.Drawing.Size(79, 20);
            this.tbRAPos.TabIndex = 30;
            // 
            // tbDEPosEQ
            // 
            this.tbDEPosEQ.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbDEPosEQ.Enabled = false;
            this.tbDEPosEQ.Location = new System.Drawing.Point(97, 180);
            this.tbDEPosEQ.Name = "tbDEPosEQ";
            this.tbDEPosEQ.ReadOnly = true;
            this.tbDEPosEQ.Size = new System.Drawing.Size(76, 20);
            this.tbDEPosEQ.TabIndex = 29;
            // 
            // cbDETurning
            // 
            this.cbDETurning.AutoSize = true;
            this.cbDETurning.Enabled = false;
            this.cbDETurning.Location = new System.Drawing.Point(160, 114);
            this.cbDETurning.Name = "cbDETurning";
            this.cbDETurning.Size = new System.Drawing.Size(15, 14);
            this.cbDETurning.TabIndex = 27;
            this.cbDETurning.UseVisualStyleBackColor = true;
            this.cbDETurning.CheckedChanged += new System.EventHandler(this.cbDETurning_CheckedChanged);
            // 
            // cbRATurning
            // 
            this.cbRATurning.AutoSize = true;
            this.cbRATurning.Enabled = false;
            this.cbRATurning.Location = new System.Drawing.Point(160, 85);
            this.cbRATurning.Name = "cbRATurning";
            this.cbRATurning.Size = new System.Drawing.Size(15, 14);
            this.cbRATurning.TabIndex = 26;
            this.cbRATurning.UseVisualStyleBackColor = true;
            this.cbRATurning.CheckedChanged += new System.EventHandler(this.cbRATurning_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "DE :";
            // 
            // btnSetSpeedDETurning
            // 
            this.btnSetSpeedDETurning.Enabled = false;
            this.btnSetSpeedDETurning.Location = new System.Drawing.Point(264, 109);
            this.btnSetSpeedDETurning.Name = "btnSetSpeedDETurning";
            this.btnSetSpeedDETurning.Size = new System.Drawing.Size(35, 23);
            this.btnSetSpeedDETurning.TabIndex = 24;
            this.btnSetSpeedDETurning.Text = "Set";
            this.btnSetSpeedDETurning.UseVisualStyleBackColor = true;
            this.btnSetSpeedDETurning.Click += new System.EventHandler(this.btnSetSpeedDETurning_Click);
            // 
            // tbSpeedDEDaily
            // 
            this.tbSpeedDEDaily.Enabled = false;
            this.tbSpeedDEDaily.Location = new System.Drawing.Point(201, 111);
            this.tbSpeedDEDaily.Name = "tbSpeedDEDaily";
            this.tbSpeedDEDaily.Size = new System.Drawing.Size(61, 20);
            this.tbSpeedDEDaily.TabIndex = 23;
            // 
            // btnSetPosRA
            // 
            this.btnSetPosRA.Enabled = false;
            this.btnSetPosRA.Location = new System.Drawing.Point(24, 232);
            this.btnSetPosRA.Name = "btnSetPosRA";
            this.btnSetPosRA.Size = new System.Drawing.Size(55, 23);
            this.btnSetPosRA.TabIndex = 22;
            this.btnSetPosRA.Text = "Set";
            this.btnSetPosRA.UseVisualStyleBackColor = true;
            this.btnSetPosRA.Click += new System.EventHandler(this.btnSetPos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "RA :";
            // 
            // btnSetSpeedRATurning
            // 
            this.btnSetSpeedRATurning.Enabled = false;
            this.btnSetSpeedRATurning.Location = new System.Drawing.Point(264, 80);
            this.btnSetSpeedRATurning.Name = "btnSetSpeedRATurning";
            this.btnSetSpeedRATurning.Size = new System.Drawing.Size(35, 23);
            this.btnSetSpeedRATurning.TabIndex = 20;
            this.btnSetSpeedRATurning.Text = "Set";
            this.btnSetSpeedRATurning.UseVisualStyleBackColor = true;
            this.btnSetSpeedRATurning.Click += new System.EventHandler(this.btnSetSpeedRATurning_Click);
            // 
            // tbSpeedRADaily
            // 
            this.tbSpeedRADaily.Enabled = false;
            this.tbSpeedRADaily.Location = new System.Drawing.Point(201, 82);
            this.tbSpeedRADaily.Name = "tbSpeedRADaily";
            this.tbSpeedRADaily.Size = new System.Drawing.Size(61, 20);
            this.tbSpeedRADaily.TabIndex = 19;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(244, 180);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(55, 23);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(188, 180);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(55, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbObjectPos
            // 
            this.cbObjectPos.Enabled = false;
            this.cbObjectPos.FormattingEnabled = true;
            this.cbObjectPos.Location = new System.Drawing.Point(188, 154);
            this.cbObjectPos.Name = "cbObjectPos";
            this.cbObjectPos.Size = new System.Drawing.Size(111, 21);
            this.cbObjectPos.TabIndex = 16;
            this.cbObjectPos.SelectedIndexChanged += new System.EventHandler(this.cbObjectPos_SelectedIndexChanged);
            this.cbObjectPos.TextChanged += new System.EventHandler(this.cbObjectPos_TextChanged);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Enabled = false;
            this.btnGoTo.Location = new System.Drawing.Point(188, 206);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(111, 23);
            this.btnGoTo.TabIndex = 12;
            this.btnGoTo.Text = "Go to";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // pgSpeed
            // 
            this.pgSpeed.Enabled = false;
            this.pgSpeed.Location = new System.Drawing.Point(188, 6);
            this.pgSpeed.Name = "pgSpeed";
            this.pgSpeed.Size = new System.Drawing.Size(111, 19);
            this.pgSpeed.Step = 1;
            this.pgSpeed.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgSpeed.TabIndex = 8;
            this.pgSpeed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pgSpeed_MouseDown);
            this.pgSpeed.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pgSpeed_MouseMove);
            this.pgSpeed.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pgSpeed_MouseUp);
            // 
            // btnStepLeft
            // 
            this.btnStepLeft.Enabled = false;
            this.btnStepLeft.Location = new System.Drawing.Point(8, 57);
            this.btnStepLeft.Name = "btnStepLeft";
            this.btnStepLeft.Size = new System.Drawing.Size(21, 23);
            this.btnStepLeft.TabIndex = 6;
            this.btnStepLeft.Text = "<";
            this.btnStepLeft.UseVisualStyleBackColor = true;
            this.btnStepLeft.Click += new System.EventHandler(this.btnStepLeft_Click);
            // 
            // btnRollLeft
            // 
            this.btnRollLeft.Enabled = false;
            this.btnRollLeft.Location = new System.Drawing.Point(30, 57);
            this.btnRollLeft.Name = "btnRollLeft";
            this.btnRollLeft.Size = new System.Drawing.Size(30, 23);
            this.btnRollLeft.TabIndex = 6;
            this.btnRollLeft.Text = "<<";
            this.btnRollLeft.UseVisualStyleBackColor = true;
            this.btnRollLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRollLeft_MouseDown);
            this.btnRollLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRollLeft_MouseUp);
            // 
            // btnStepRight
            // 
            this.btnStepRight.Enabled = false;
            this.btnStepRight.Location = new System.Drawing.Point(136, 57);
            this.btnStepRight.Name = "btnStepRight";
            this.btnStepRight.Size = new System.Drawing.Size(21, 23);
            this.btnStepRight.TabIndex = 5;
            this.btnStepRight.Text = ">";
            this.btnStepRight.UseVisualStyleBackColor = true;
            this.btnStepRight.Click += new System.EventHandler(this.btnStepRight_Click);
            // 
            // btnRollRight
            // 
            this.btnRollRight.Enabled = false;
            this.btnRollRight.Location = new System.Drawing.Point(105, 57);
            this.btnRollRight.Name = "btnRollRight";
            this.btnRollRight.Size = new System.Drawing.Size(30, 23);
            this.btnRollRight.TabIndex = 5;
            this.btnRollRight.Text = ">>";
            this.btnRollRight.UseVisualStyleBackColor = true;
            this.btnRollRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRollRight_MouseDown);
            this.btnRollRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRollRight_MouseUp);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(61, 56);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(43, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Position :";
            // 
            // tbRAPosEQ
            // 
            this.tbRAPosEQ.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbRAPosEQ.Enabled = false;
            this.tbRAPosEQ.Location = new System.Drawing.Point(12, 180);
            this.tbRAPosEQ.Name = "tbRAPosEQ";
            this.tbRAPosEQ.ReadOnly = true;
            this.tbRAPosEQ.Size = new System.Drawing.Size(79, 20);
            this.tbRAPosEQ.TabIndex = 1;
            // 
            // cbRCP
            // 
            this.cbRCP.AutoSize = true;
            this.cbRCP.Enabled = false;
            this.cbRCP.Location = new System.Drawing.Point(234, 238);
            this.cbRCP.Name = "cbRCP";
            this.cbRCP.Size = new System.Drawing.Size(63, 17);
            this.cbRCP.TabIndex = 0;
            this.cbRCP.Text = "Remote";
            this.cbRCP.UseVisualStyleBackColor = true;
            this.cbRCP.CheckedChanged += new System.EventHandler(this.cbRCP_CheckedChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox4);
            this.tabPage6.Controls.Add(this.groupBox3);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(305, 266);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Modes";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbMaxNavSpeed);
            this.groupBox4.Controls.Add(this.btnSetNavSpeed);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tbMinNavSpeed);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cbMicroStepNav);
            this.groupBox4.Location = new System.Drawing.Point(3, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(299, 82);
            this.groupBox4.TabIndex = 59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Navigation mode:";
            // 
            // tbMaxNavSpeed
            // 
            this.tbMaxNavSpeed.Enabled = false;
            this.tbMaxNavSpeed.Location = new System.Drawing.Point(180, 44);
            this.tbMaxNavSpeed.Name = "tbMaxNavSpeed";
            this.tbMaxNavSpeed.Size = new System.Drawing.Size(61, 20);
            this.tbMaxNavSpeed.TabIndex = 52;
            // 
            // btnSetNavSpeed
            // 
            this.btnSetNavSpeed.Enabled = false;
            this.btnSetNavSpeed.Location = new System.Drawing.Point(248, 42);
            this.btnSetNavSpeed.Name = "btnSetNavSpeed";
            this.btnSetNavSpeed.Size = new System.Drawing.Size(48, 23);
            this.btnSetNavSpeed.TabIndex = 51;
            this.btnSetNavSpeed.Text = "Set";
            this.btnSetNavSpeed.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Speed (Min/Max):";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMinNavSpeed
            // 
            this.tbMinNavSpeed.Enabled = false;
            this.tbMinNavSpeed.Location = new System.Drawing.Point(113, 44);
            this.tbMinNavSpeed.Name = "tbMinNavSpeed";
            this.tbMinNavSpeed.Size = new System.Drawing.Size(61, 20);
            this.tbMinNavSpeed.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Microstep :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbMicroStepNav
            // 
            this.cbMicroStepNav.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbMicroStepNav.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMicroStepNav.Enabled = false;
            this.cbMicroStepNav.FormattingEnabled = true;
            this.cbMicroStepNav.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32"});
            this.cbMicroStepNav.Location = new System.Drawing.Point(113, 17);
            this.cbMicroStepNav.Name = "cbMicroStepNav";
            this.cbMicroStepNav.Size = new System.Drawing.Size(61, 21);
            this.cbMicroStepNav.TabIndex = 47;
            this.cbMicroStepNav.SelectedIndexChanged += new System.EventHandler(this.cbMicroStepNav_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbDEMaxDailySpeed);
            this.groupBox3.Controls.Add(this.btnSetDEDailySpeed);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tbDEMinDailySpeed);
            this.groupBox3.Controls.Add(this.tbRAMaxDailySpeed);
            this.groupBox3.Controls.Add(this.btnSetRADailySpeed);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.tbRAMinDailySpeed);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cbMicroStepDaily);
            this.groupBox3.Location = new System.Drawing.Point(3, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 100);
            this.groupBox3.TabIndex = 58;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Daily rotation and compensation mode:";
            // 
            // tbDEMaxDailySpeed
            // 
            this.tbDEMaxDailySpeed.Enabled = false;
            this.tbDEMaxDailySpeed.Location = new System.Drawing.Point(180, 64);
            this.tbDEMaxDailySpeed.Name = "tbDEMaxDailySpeed";
            this.tbDEMaxDailySpeed.Size = new System.Drawing.Size(61, 20);
            this.tbDEMaxDailySpeed.TabIndex = 67;
            // 
            // btnSetDEDailySpeed
            // 
            this.btnSetDEDailySpeed.Enabled = false;
            this.btnSetDEDailySpeed.Location = new System.Drawing.Point(248, 64);
            this.btnSetDEDailySpeed.Name = "btnSetDEDailySpeed";
            this.btnSetDEDailySpeed.Size = new System.Drawing.Size(48, 23);
            this.btnSetDEDailySpeed.TabIndex = 66;
            this.btnSetDEDailySpeed.Text = "Set";
            this.btnSetDEDailySpeed.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 13);
            this.label12.TabIndex = 65;
            this.label12.Text = "DE Speed (Min/Max):";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDEMinDailySpeed
            // 
            this.tbDEMinDailySpeed.Enabled = false;
            this.tbDEMinDailySpeed.Location = new System.Drawing.Point(113, 66);
            this.tbDEMinDailySpeed.Name = "tbDEMinDailySpeed";
            this.tbDEMinDailySpeed.Size = new System.Drawing.Size(61, 20);
            this.tbDEMinDailySpeed.TabIndex = 64;
            // 
            // tbRAMaxDailySpeed
            // 
            this.tbRAMaxDailySpeed.Enabled = false;
            this.tbRAMaxDailySpeed.Location = new System.Drawing.Point(180, 40);
            this.tbRAMaxDailySpeed.Name = "tbRAMaxDailySpeed";
            this.tbRAMaxDailySpeed.Size = new System.Drawing.Size(61, 20);
            this.tbRAMaxDailySpeed.TabIndex = 63;
            // 
            // btnSetRADailySpeed
            // 
            this.btnSetRADailySpeed.Enabled = false;
            this.btnSetRADailySpeed.Location = new System.Drawing.Point(248, 38);
            this.btnSetRADailySpeed.Name = "btnSetRADailySpeed";
            this.btnSetRADailySpeed.Size = new System.Drawing.Size(48, 23);
            this.btnSetRADailySpeed.TabIndex = 62;
            this.btnSetRADailySpeed.Text = "Set";
            this.btnSetRADailySpeed.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "RA Speed (Min/Max):";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbRAMinDailySpeed
            // 
            this.tbRAMinDailySpeed.Enabled = false;
            this.tbRAMinDailySpeed.Location = new System.Drawing.Point(113, 40);
            this.tbRAMinDailySpeed.Name = "tbRAMinDailySpeed";
            this.tbRAMinDailySpeed.Size = new System.Drawing.Size(61, 20);
            this.tbRAMinDailySpeed.TabIndex = 60;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(48, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 59;
            this.label11.Text = "Microstep :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbMicroStepDaily
            // 
            this.cbMicroStepDaily.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbMicroStepDaily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMicroStepDaily.Enabled = false;
            this.cbMicroStepDaily.FormattingEnabled = true;
            this.cbMicroStepDaily.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32"});
            this.cbMicroStepDaily.Location = new System.Drawing.Point(113, 14);
            this.cbMicroStepDaily.Name = "cbMicroStepDaily";
            this.cbMicroStepDaily.Size = new System.Drawing.Size(61, 21);
            this.cbMicroStepDaily.TabIndex = 58;
            this.cbMicroStepDaily.SelectedIndexChanged += new System.EventHandler(this.cbMicroStepDaily_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.tbReleaseTime);
            this.tabPage3.Controls.Add(this.btnSetReleaseTime);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(305, 266);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Motors";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRAPower);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.btnSetRAGear);
            this.groupBox1.Controls.Add(this.tbRAGear);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.tbRAPosMax);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.btnSetRAMaxMinPos);
            this.groupBox1.Controls.Add(this.tbRAPosMin);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 102);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RA Motor";
            // 
            // cbRAPower
            // 
            this.cbRAPower.AutoSize = true;
            this.cbRAPower.Enabled = false;
            this.cbRAPower.Location = new System.Drawing.Point(229, 12);
            this.cbRAPower.Name = "cbRAPower";
            this.cbRAPower.Size = new System.Drawing.Size(56, 17);
            this.cbRAPower.TabIndex = 40;
            this.cbRAPower.Text = "Power";
            this.cbRAPower.UseVisualStyleBackColor = true;
            this.cbRAPower.CheckedChanged += new System.EventHandler(this.cbRAPower_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Positions :";
            // 
            // btnSetRAGear
            // 
            this.btnSetRAGear.Enabled = false;
            this.btnSetRAGear.Location = new System.Drawing.Point(237, 64);
            this.btnSetRAGear.Name = "btnSetRAGear";
            this.btnSetRAGear.Size = new System.Drawing.Size(48, 23);
            this.btnSetRAGear.TabIndex = 38;
            this.btnSetRAGear.Text = "Set";
            this.btnSetRAGear.UseVisualStyleBackColor = true;
            this.btnSetRAGear.Click += new System.EventHandler(this.button9_Click);
            // 
            // tbRAGear
            // 
            this.tbRAGear.Enabled = false;
            this.tbRAGear.Location = new System.Drawing.Point(103, 67);
            this.tbRAGear.Name = "tbRAGear";
            this.tbRAGear.Size = new System.Drawing.Size(128, 20);
            this.tbRAGear.TabIndex = 37;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 69);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(39, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Gear : ";
            // 
            // tbRAPosMax
            // 
            this.tbRAPosMax.Enabled = false;
            this.tbRAPosMax.Location = new System.Drawing.Point(170, 37);
            this.tbRAPosMax.Name = "tbRAPosMax";
            this.tbRAPosMax.Size = new System.Drawing.Size(61, 20);
            this.tbRAPosMax.TabIndex = 35;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(42, 40);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 13);
            this.label20.TabIndex = 34;
            this.label20.Text = "Min/Max: ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSetRAMaxMinPos
            // 
            this.btnSetRAMaxMinPos.Enabled = false;
            this.btnSetRAMaxMinPos.Location = new System.Drawing.Point(237, 35);
            this.btnSetRAMaxMinPos.Name = "btnSetRAMaxMinPos";
            this.btnSetRAMaxMinPos.Size = new System.Drawing.Size(48, 23);
            this.btnSetRAMaxMinPos.TabIndex = 33;
            this.btnSetRAMaxMinPos.Text = "Set";
            this.btnSetRAMaxMinPos.UseVisualStyleBackColor = true;
            // 
            // tbRAPosMin
            // 
            this.tbRAPosMin.Enabled = false;
            this.tbRAPosMin.Location = new System.Drawing.Point(103, 37);
            this.tbRAPosMin.Name = "tbRAPosMin";
            this.tbRAPosMin.Size = new System.Drawing.Size(61, 20);
            this.tbRAPosMin.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Release Time :";
            // 
            // tbReleaseTime
            // 
            this.tbReleaseTime.Enabled = false;
            this.tbReleaseTime.Location = new System.Drawing.Point(176, 233);
            this.tbReleaseTime.Name = "tbReleaseTime";
            this.tbReleaseTime.Size = new System.Drawing.Size(61, 20);
            this.tbReleaseTime.TabIndex = 41;
            // 
            // btnSetReleaseTime
            // 
            this.btnSetReleaseTime.Enabled = false;
            this.btnSetReleaseTime.Location = new System.Drawing.Point(243, 231);
            this.btnSetReleaseTime.Name = "btnSetReleaseTime";
            this.btnSetReleaseTime.Size = new System.Drawing.Size(48, 23);
            this.btnSetReleaseTime.TabIndex = 40;
            this.btnSetReleaseTime.Text = "Set";
            this.btnSetReleaseTime.UseVisualStyleBackColor = true;
            this.btnSetReleaseTime.Click += new System.EventHandler(this.btnSetReleaseTime_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDEPower);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.btnSetDEGear);
            this.groupBox2.Controls.Add(this.tbDEGear);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.tbDEPosMax);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.btnSetDEMaxMinPos);
            this.groupBox2.Controls.Add(this.tbDEPosMin);
            this.groupBox2.Location = new System.Drawing.Point(6, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 102);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DE Motor";
            // 
            // cbDEPower
            // 
            this.cbDEPower.AutoSize = true;
            this.cbDEPower.Enabled = false;
            this.cbDEPower.Location = new System.Drawing.Point(228, 12);
            this.cbDEPower.Name = "cbDEPower";
            this.cbDEPower.Size = new System.Drawing.Size(56, 17);
            this.cbDEPower.TabIndex = 40;
            this.cbDEPower.Text = "Power";
            this.cbDEPower.UseVisualStyleBackColor = true;
            this.cbDEPower.CheckedChanged += new System.EventHandler(this.cbDEPower_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "Positions :";
            // 
            // btnSetDEGear
            // 
            this.btnSetDEGear.Enabled = false;
            this.btnSetDEGear.Location = new System.Drawing.Point(237, 64);
            this.btnSetDEGear.Name = "btnSetDEGear";
            this.btnSetDEGear.Size = new System.Drawing.Size(48, 23);
            this.btnSetDEGear.TabIndex = 38;
            this.btnSetDEGear.Text = "Set";
            this.btnSetDEGear.UseVisualStyleBackColor = true;
            this.btnSetDEGear.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbDEGear
            // 
            this.tbDEGear.Enabled = false;
            this.tbDEGear.Location = new System.Drawing.Point(103, 67);
            this.tbDEGear.Name = "tbDEGear";
            this.tbDEGear.Size = new System.Drawing.Size(128, 20);
            this.tbDEGear.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Gear : ";
            // 
            // tbDEPosMax
            // 
            this.tbDEPosMax.Enabled = false;
            this.tbDEPosMax.Location = new System.Drawing.Point(170, 37);
            this.tbDEPosMax.Name = "tbDEPosMax";
            this.tbDEPosMax.Size = new System.Drawing.Size(61, 20);
            this.tbDEPosMax.TabIndex = 35;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(42, 40);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Min/Max: ";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSetDEMaxMinPos
            // 
            this.btnSetDEMaxMinPos.Enabled = false;
            this.btnSetDEMaxMinPos.Location = new System.Drawing.Point(237, 35);
            this.btnSetDEMaxMinPos.Name = "btnSetDEMaxMinPos";
            this.btnSetDEMaxMinPos.Size = new System.Drawing.Size(48, 23);
            this.btnSetDEMaxMinPos.TabIndex = 33;
            this.btnSetDEMaxMinPos.Text = "Set";
            this.btnSetDEMaxMinPos.UseVisualStyleBackColor = true;
            // 
            // tbDEPosMin
            // 
            this.tbDEPosMin.Enabled = false;
            this.tbDEPosMin.Location = new System.Drawing.Point(103, 37);
            this.tbDEPosMin.Name = "tbDEPosMin";
            this.tbDEPosMin.Size = new System.Drawing.Size(61, 20);
            this.tbDEPosMin.TabIndex = 32;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.rbDegrees);
            this.tabPage5.Controls.Add(this.rbHours);
            this.tabPage5.Controls.Add(this.cbIsTimeRunning);
            this.tabPage5.Controls.Add(this.tbTemp);
            this.tabPage5.Controls.Add(this.label13);
            this.tabPage5.Controls.Add(this.btnTime);
            this.tabPage5.Controls.Add(this.dateTimePicker);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(305, 266);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Parameters";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // rbDegrees
            // 
            this.rbDegrees.AutoSize = true;
            this.rbDegrees.Checked = true;
            this.rbDegrees.Enabled = false;
            this.rbDegrees.Location = new System.Drawing.Point(9, 121);
            this.rbDegrees.Name = "rbDegrees";
            this.rbDegrees.Size = new System.Drawing.Size(84, 17);
            this.rbDegrees.TabIndex = 6;
            this.rbDegrees.TabStop = true;
            this.rbDegrees.Text = "RA: degrees";
            this.rbDegrees.UseVisualStyleBackColor = true;
            this.rbDegrees.CheckedChanged += new System.EventHandler(this.rbDegrees_CheckedChanged);
            // 
            // rbHours
            // 
            this.rbHours.AutoSize = true;
            this.rbHours.Enabled = false;
            this.rbHours.Location = new System.Drawing.Point(9, 98);
            this.rbHours.Name = "rbHours";
            this.rbHours.Size = new System.Drawing.Size(72, 17);
            this.rbHours.TabIndex = 5;
            this.rbHours.Text = "RA: hours";
            this.rbHours.UseVisualStyleBackColor = true;
            this.rbHours.CheckedChanged += new System.EventHandler(this.rbHours_CheckedChanged);
            // 
            // cbIsTimeRunning
            // 
            this.cbIsTimeRunning.AutoSize = true;
            this.cbIsTimeRunning.Checked = true;
            this.cbIsTimeRunning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsTimeRunning.Location = new System.Drawing.Point(9, 63);
            this.cbIsTimeRunning.Name = "cbIsTimeRunning";
            this.cbIsTimeRunning.Size = new System.Drawing.Size(99, 17);
            this.cbIsTimeRunning.TabIndex = 4;
            this.cbIsTimeRunning.Text = "Time is ruinning";
            this.cbIsTimeRunning.UseVisualStyleBackColor = true;
            this.cbIsTimeRunning.CheckedChanged += new System.EventHandler(this.cbIsTimeRunning_CheckedChanged);
            // 
            // tbTemp
            // 
            this.tbTemp.Location = new System.Drawing.Point(99, 32);
            this.tbTemp.Name = "tbTemp";
            this.tbTemp.ReadOnly = true;
            this.tbTemp.Size = new System.Drawing.Size(108, 20);
            this.tbTemp.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Temperature :";
            // 
            // btnTime
            // 
            this.btnTime.Enabled = false;
            this.btnTime.Location = new System.Drawing.Point(157, 6);
            this.btnTime.Name = "btnTime";
            this.btnTime.Size = new System.Drawing.Size(50, 23);
            this.btnTime.TabIndex = 1;
            this.btnTime.Text = "Set";
            this.btnTime.UseVisualStyleBackColor = true;
            this.btnTime.Click += new System.EventHandler(this.btnTime_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "yyyy:MM:dd HH:MM:ss";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(6, 6);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(145, 20);
            this.dateTimePicker.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnClear);
            this.tabPage2.Controls.Add(this.cbDebug);
            this.tabPage2.Controls.Add(this.btnSend);
            this.tabPage2.Controls.Add(this.btnPing);
            this.tabPage2.Controls.Add(this.tbCmd);
            this.tabPage2.Controls.Add(this.LogBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(305, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(251, 211);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(48, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbDebug
            // 
            this.cbDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDebug.AutoSize = true;
            this.cbDebug.Enabled = false;
            this.cbDebug.Location = new System.Drawing.Point(6, 215);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(91, 17);
            this.cbDebug.TabIndex = 4;
            this.cbDebug.Text = "Debug  Mode";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.CheckedChanged += new System.EventHandler(this.cbDebug_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(60, 238);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(50, 22);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnPing
            // 
            this.btnPing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPing.Enabled = false;
            this.btnPing.Location = new System.Drawing.Point(116, 238);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(48, 23);
            this.btnPing.TabIndex = 2;
            this.btnPing.Text = "Ping";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // tbCmd
            // 
            this.tbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbCmd.Enabled = false;
            this.tbCmd.Location = new System.Drawing.Point(6, 238);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(51, 20);
            this.tbCmd.TabIndex = 1;
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.Location = new System.Drawing.Point(6, 6);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(293, 199);
            this.LogBox.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbAbout);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(305, 266);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tbAbout
            // 
            this.tbAbout.AcceptsReturn = true;
            this.tbAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAbout.Location = new System.Drawing.Point(6, 3);
            this.tbAbout.Multiline = true;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.Size = new System.Drawing.Size(292, 257);
            this.tbAbout.TabIndex = 0;
            this.tbAbout.Text = "\r\n\r\nMounter Control Panel\r\nver 1.0.net\r\n\r\nCreated by Alexey V. Popov\r\n9141866@gma" +
    "il.com\r\nSt.-Petersburg\r\nRussia\r\n\r\n2015";
            this.tbAbout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Location = new System.Drawing.Point(242, 359);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 9;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // MounterControlPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 384);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.tabPageModes);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MounterControlPanelForm";
            this.Text = "FMounter Control Panel v 1.0";
            this.tabPageModes.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tabPageModes;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSetPosRA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetSpeedRATurning;
        private System.Windows.Forms.TextBox tbSpeedRADaily;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbObjectPos;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.ProgressBar pgSpeed;
        private System.Windows.Forms.Button btnStepLeft;
        private System.Windows.Forms.Button btnRollLeft;
        private System.Windows.Forms.Button btnStepRight;
        private System.Windows.Forms.Button btnRollRight;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRAPosEQ;
        private System.Windows.Forms.CheckBox cbRCP;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.TextBox tbCmd;
        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox tbAbout;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnStepUp;
        private System.Windows.Forms.Button btnStepDown;
        private System.Windows.Forms.Button btnRollDown;
        private System.Windows.Forms.Button btnRollUp;
        private System.Windows.Forms.Button btnSetCurrentNavSpeed;
        private System.Windows.Forms.TextBox tbNavSpeed;
        private System.Windows.Forms.TextBox tbDENewPos;
        private System.Windows.Forms.TextBox tbRANewPos;
        private System.Windows.Forms.TextBox tbDEPos;
        private System.Windows.Forms.TextBox tbRAPos;
        private System.Windows.Forms.TextBox tbDEPosEQ;
        private System.Windows.Forms.CheckBox cbDETurning;
        private System.Windows.Forms.CheckBox cbRATurning;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetSpeedDETurning;
        private System.Windows.Forms.TextBox tbSpeedDEDaily;
        private System.Windows.Forms.Button btnSetPosDE;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox tbTemp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.RadioButton rbDegrees;
        private System.Windows.Forms.RadioButton rbHours;
        private System.Windows.Forms.CheckBox cbIsTimeRunning;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbMaxNavSpeed;
        private System.Windows.Forms.Button btnSetNavSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbMinNavSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMicroStepNav;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbDEMaxDailySpeed;
        private System.Windows.Forms.Button btnSetDEDailySpeed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbDEMinDailySpeed;
        private System.Windows.Forms.TextBox tbRAMaxDailySpeed;
        private System.Windows.Forms.Button btnSetRADailySpeed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRAMinDailySpeed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbMicroStepDaily;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSetRAGear;
        private System.Windows.Forms.TextBox tbRAGear;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbRAPosMax;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnSetRAMaxMinPos;
        private System.Windows.Forms.TextBox tbRAPosMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbReleaseTime;
        private System.Windows.Forms.Button btnSetReleaseTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnSetDEGear;
        private System.Windows.Forms.TextBox tbDEGear;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbDEPosMax;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSetDEMaxMinPos;
        private System.Windows.Forms.TextBox tbDEPosMin;
        private System.Windows.Forms.CheckBox cbRAPower;
        private System.Windows.Forms.CheckBox cbDEPower;
    }
}

