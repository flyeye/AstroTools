namespace WindowsFormsApplication1
{
    partial class FocuserControlPanelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FocuserControlPanelForm));
            this.btnQuit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSetPos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.tbSpeed = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbNewPos = new System.Windows.Forms.ComboBox();
            this.pgPosition = new System.Windows.Forms.ProgressBar();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRangeCheck = new System.Windows.Forms.CheckBox();
            this.pgSpeed = new System.Windows.Forms.ProgressBar();
            this.cbPower = new System.Windows.Forms.CheckBox();
            this.btnStepLeft = new System.Windows.Forms.Button();
            this.btnRollLeft = new System.Windows.Forms.Button();
            this.btnStepRight = new System.Windows.Forms.Button();
            this.btnRollRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPosition = new System.Windows.Forms.TextBox();
            this.cbRCP = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnPing = new System.Windows.Forms.Button();
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMicroStep = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbReleaseTime = new System.Windows.Forms.TextBox();
            this.btnSetReleaseTime = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.tbMinPos = new System.Windows.Forms.TextBox();
            this.tbMaxPos = new System.Windows.Forms.TextBox();
            this.btnSetMinPos = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSetMax = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Location = new System.Drawing.Point(191, 401);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 0;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(4, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(262, 325);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSetPos);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnSetSpeed);
            this.tabPage1.Controls.Add(this.tbSpeed);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.cbNewPos);
            this.tabPage1.Controls.Add(this.pgPosition);
            this.tabPage1.Controls.Add(this.btnGoTo);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.cbRangeCheck);
            this.tabPage1.Controls.Add(this.pgSpeed);
            this.tabPage1.Controls.Add(this.cbPower);
            this.tabPage1.Controls.Add(this.btnStepLeft);
            this.tabPage1.Controls.Add(this.btnRollLeft);
            this.tabPage1.Controls.Add(this.btnStepRight);
            this.tabPage1.Controls.Add(this.btnRollRight);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbPosition);
            this.tabPage1.Controls.Add(this.cbRCP);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(254, 299);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Focuser";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSetPos
            // 
            this.btnSetPos.Enabled = false;
            this.btnSetPos.Location = new System.Drawing.Point(190, 104);
            this.btnSetPos.Name = "btnSetPos";
            this.btnSetPos.Size = new System.Drawing.Size(55, 23);
            this.btnSetPos.TabIndex = 22;
            this.btnSetPos.Text = "Set";
            this.btnSetPos.UseVisualStyleBackColor = true;
            this.btnSetPos.Click += new System.EventHandler(this.btnPosSet_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Speed :";
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.Enabled = false;
            this.btnSetSpeed.Location = new System.Drawing.Point(134, 227);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(35, 23);
            this.btnSetSpeed.TabIndex = 20;
            this.btnSetSpeed.Text = "Set";
            this.btnSetSpeed.UseVisualStyleBackColor = true;
            // 
            // tbSpeed
            // 
            this.tbSpeed.Enabled = false;
            this.tbSpeed.Location = new System.Drawing.Point(61, 229);
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(67, 20);
            this.tbSpeed.TabIndex = 19;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(190, 133);
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
            this.btnSave.Location = new System.Drawing.Point(134, 133);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(55, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbNewPos
            // 
            this.cbNewPos.Enabled = false;
            this.cbNewPos.FormattingEnabled = true;
            this.cbNewPos.Location = new System.Drawing.Point(61, 106);
            this.cbNewPos.Name = "cbNewPos";
            this.cbNewPos.Size = new System.Drawing.Size(67, 21);
            this.cbNewPos.TabIndex = 16;
            this.cbNewPos.Text = "0";
            this.cbNewPos.TextChanged += new System.EventHandler(this.cbNewPos_TextChanged);
            // 
            // pgPosition
            // 
            this.pgPosition.Enabled = false;
            this.pgPosition.Location = new System.Drawing.Point(8, 38);
            this.pgPosition.Name = "pgPosition";
            this.pgPosition.Size = new System.Drawing.Size(237, 23);
            this.pgPosition.Step = 1;
            this.pgPosition.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgPosition.TabIndex = 13;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Enabled = false;
            this.btnGoTo.Location = new System.Drawing.Point(134, 104);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(55, 23);
            this.btnGoTo.TabIndex = 12;
            this.btnGoTo.Text = "Go to";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "New: ";
            // 
            // cbRangeCheck
            // 
            this.cbRangeCheck.AutoSize = true;
            this.cbRangeCheck.Enabled = false;
            this.cbRangeCheck.Location = new System.Drawing.Point(15, 140);
            this.cbRangeCheck.Name = "cbRangeCheck";
            this.cbRangeCheck.Size = new System.Drawing.Size(91, 17);
            this.cbRangeCheck.TabIndex = 9;
            this.cbRangeCheck.Text = "Range check";
            this.cbRangeCheck.UseVisualStyleBackColor = true;
            this.cbRangeCheck.CheckedChanged += new System.EventHandler(this.cbRangeCheck_CheckedChanged);
            // 
            // pgSpeed
            // 
            this.pgSpeed.Enabled = false;
            this.pgSpeed.Location = new System.Drawing.Point(8, 200);
            this.pgSpeed.Name = "pgSpeed";
            this.pgSpeed.Size = new System.Drawing.Size(237, 23);
            this.pgSpeed.Step = 1;
            this.pgSpeed.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgSpeed.TabIndex = 8;
            this.pgSpeed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pgSpeed_MouseDown);
            this.pgSpeed.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pgSpeed_MouseMove);
            this.pgSpeed.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pgSpeed_MouseUp);
            // 
            // cbPower
            // 
            this.cbPower.AutoSize = true;
            this.cbPower.Enabled = false;
            this.cbPower.Location = new System.Drawing.Point(87, 278);
            this.cbPower.Name = "cbPower";
            this.cbPower.Size = new System.Drawing.Size(56, 17);
            this.cbPower.TabIndex = 7;
            this.cbPower.Text = "Power";
            this.cbPower.UseVisualStyleBackColor = true;
            this.cbPower.CheckedChanged += new System.EventHandler(this.cbPower_CheckedChanged);
            // 
            // btnStepLeft
            // 
            this.btnStepLeft.Enabled = false;
            this.btnStepLeft.Location = new System.Drawing.Point(8, 67);
            this.btnStepLeft.Name = "btnStepLeft";
            this.btnStepLeft.Size = new System.Drawing.Size(38, 23);
            this.btnStepLeft.TabIndex = 6;
            this.btnStepLeft.Text = "<";
            this.btnStepLeft.UseVisualStyleBackColor = true;
            this.btnStepLeft.Click += new System.EventHandler(this.btnStepRight_Click);
            // 
            // btnRollLeft
            // 
            this.btnRollLeft.Enabled = false;
            this.btnRollLeft.Location = new System.Drawing.Point(51, 67);
            this.btnRollLeft.Name = "btnRollLeft";
            this.btnRollLeft.Size = new System.Drawing.Size(38, 23);
            this.btnRollLeft.TabIndex = 6;
            this.btnRollLeft.Text = "<<";
            this.btnRollLeft.UseVisualStyleBackColor = true;
            this.btnRollLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRollRight_MouseDown);
            this.btnRollLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRollRight_MouseUp);
            // 
            // btnStepRight
            // 
            this.btnStepRight.Enabled = false;
            this.btnStepRight.Location = new System.Drawing.Point(209, 67);
            this.btnStepRight.Name = "btnStepRight";
            this.btnStepRight.Size = new System.Drawing.Size(38, 23);
            this.btnStepRight.TabIndex = 5;
            this.btnStepRight.Text = ">";
            this.btnStepRight.UseVisualStyleBackColor = true;
            this.btnStepRight.Click += new System.EventHandler(this.btnStepLeft_Click);
            // 
            // btnRollRight
            // 
            this.btnRollRight.Enabled = false;
            this.btnRollRight.Location = new System.Drawing.Point(165, 67);
            this.btnRollRight.Name = "btnRollRight";
            this.btnRollRight.Size = new System.Drawing.Size(38, 23);
            this.btnRollRight.TabIndex = 5;
            this.btnRollRight.Text = ">>";
            this.btnRollRight.UseVisualStyleBackColor = true;
            this.btnRollRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRollLeft_MouseDown);
            this.btnRollRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRollLeft_MouseUp);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(95, 67);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(64, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Position :";
            // 
            // tbPosition
            // 
            this.tbPosition.Enabled = false;
            this.tbPosition.Location = new System.Drawing.Point(61, 12);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.ReadOnly = true;
            this.tbPosition.Size = new System.Drawing.Size(67, 20);
            this.tbPosition.TabIndex = 1;
            // 
            // cbRCP
            // 
            this.cbRCP.AutoSize = true;
            this.cbRCP.Enabled = false;
            this.cbRCP.Location = new System.Drawing.Point(12, 278);
            this.cbRCP.Name = "cbRCP";
            this.cbRCP.Size = new System.Drawing.Size(63, 17);
            this.cbRCP.TabIndex = 0;
            this.cbRCP.Text = "Remote";
            this.cbRCP.UseVisualStyleBackColor = true;
            this.cbRCP.CheckedChanged += new System.EventHandler(this.cbRCP_CheckedChanged);
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
            this.tabPage2.Size = new System.Drawing.Size(254, 299);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(200, 244);
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
            this.cbDebug.Location = new System.Drawing.Point(6, 248);
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
            this.btnSend.Location = new System.Drawing.Point(60, 271);
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
            this.btnPing.Location = new System.Drawing.Point(116, 271);
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
            this.tbCmd.Location = new System.Drawing.Point(6, 271);
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
            this.LogBox.Size = new System.Drawing.Size(242, 232);
            this.LogBox.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSetMax);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.btnSetMinPos);
            this.tabPage3.Controls.Add(this.tbMaxPos);
            this.tabPage3.Controls.Add(this.tbMinPos);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.cbMicroStep);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.tbReleaseTime);
            this.tabPage3.Controls.Add(this.btnSetReleaseTime);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(254, 299);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Parameters";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Microstep mode :";
            // 
            // cbMicroStep
            // 
            this.cbMicroStep.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbMicroStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMicroStep.Enabled = false;
            this.cbMicroStep.FormattingEnabled = true;
            this.cbMicroStep.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16"});
            this.cbMicroStep.Location = new System.Drawing.Point(105, 76);
            this.cbMicroStep.Name = "cbMicroStep";
            this.cbMicroStep.Size = new System.Drawing.Size(61, 21);
            this.cbMicroStep.TabIndex = 18;
            this.cbMicroStep.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Release Time :";
            // 
            // tbReleaseTime
            // 
            this.tbReleaseTime.Enabled = false;
            this.tbReleaseTime.Location = new System.Drawing.Point(105, 49);
            this.tbReleaseTime.Name = "tbReleaseTime";
            this.tbReleaseTime.Size = new System.Drawing.Size(61, 20);
            this.tbReleaseTime.TabIndex = 16;
            // 
            // btnSetReleaseTime
            // 
            this.btnSetReleaseTime.Enabled = false;
            this.btnSetReleaseTime.Location = new System.Drawing.Point(172, 47);
            this.btnSetReleaseTime.Name = "btnSetReleaseTime";
            this.btnSetReleaseTime.Size = new System.Drawing.Size(48, 23);
            this.btnSetReleaseTime.TabIndex = 15;
            this.btnSetReleaseTime.Text = "Set";
            this.btnSetReleaseTime.UseVisualStyleBackColor = true;
            this.btnSetReleaseTime.Click += new System.EventHandler(this.btnSetReleaseTime_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(254, 299);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(241, 315);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "\r\n\r\nFocuser Control Panel\r\nver 1.4.net\r\n\r\nCreated by Alexey V. Popov\r\n9141866@gma" +
    "il.com\r\nSt.-Petersburg\r\nRussia\r\n\r\n2015";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(191, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(191, 41);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Close";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
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
            this.cbPort.Location = new System.Drawing.Point(95, 14);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(90, 21);
            this.cbPort.TabIndex = 4;
            // 
            // tbMinPos
            // 
            this.tbMinPos.Location = new System.Drawing.Point(105, 103);
            this.tbMinPos.Name = "tbMinPos";
            this.tbMinPos.Size = new System.Drawing.Size(61, 20);
            this.tbMinPos.TabIndex = 20;
            // 
            // tbMaxPos
            // 
            this.tbMaxPos.Location = new System.Drawing.Point(105, 129);
            this.tbMaxPos.Name = "tbMaxPos";
            this.tbMaxPos.Size = new System.Drawing.Size(61, 20);
            this.tbMaxPos.TabIndex = 21;
            // 
            // btnSetMinPos
            // 
            this.btnSetMinPos.Enabled = false;
            this.btnSetMinPos.Location = new System.Drawing.Point(172, 101);
            this.btnSetMinPos.Name = "btnSetMinPos";
            this.btnSetMinPos.Size = new System.Drawing.Size(48, 23);
            this.btnSetMinPos.TabIndex = 22;
            this.btnSetMinPos.Text = "Set";
            this.btnSetMinPos.UseVisualStyleBackColor = true;
            this.btnSetMinPos.Click += new System.EventHandler(this.btnSetMinPos_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Min position : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Max position : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSetMax
            // 
            this.btnSetMax.Enabled = false;
            this.btnSetMax.Location = new System.Drawing.Point(172, 127);
            this.btnSetMax.Name = "btnSetMax";
            this.btnSetMax.Size = new System.Drawing.Size(48, 23);
            this.btnSetMax.TabIndex = 25;
            this.btnSetMax.Text = "Set";
            this.btnSetMax.UseVisualStyleBackColor = true;
            this.btnSetMax.Click += new System.EventHandler(this.btnSetMax_Click_1);
            // 
            // FocuserControlPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 426);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FocuserControlPanelForm";
            this.Text = "FFocuserV1 Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FocuserControlPanelForm_FormClosing);
            this.Shown += new System.EventHandler(this.FocuserControlPanelForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.TextBox tbCmd;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox cbRCP;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.TextBox tbPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRollRight;
        private System.Windows.Forms.Button btnRollLeft;
        private System.Windows.Forms.CheckBox cbPower;
        private System.Windows.Forms.ProgressBar pgSpeed;
        private System.Windows.Forms.Button btnStepLeft;
        private System.Windows.Forms.Button btnStepRight;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbReleaseTime;
        private System.Windows.Forms.Button btnSetReleaseTime;
        private System.Windows.Forms.CheckBox cbRangeCheck;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMicroStep;
        private System.Windows.Forms.ProgressBar pgPosition;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbNewPos;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetSpeed;
        private System.Windows.Forms.TextBox tbSpeed;
        private System.Windows.Forms.Button btnSetPos;
        private System.Windows.Forms.Button btnSetMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSetMinPos;
        private System.Windows.Forms.TextBox tbMaxPos;
        private System.Windows.Forms.TextBox tbMinPos;
    }
}

