namespace ASCOM.FMounterV1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbSlewing = new System.Windows.Forms.CheckBox();
            this.cbTracking = new System.Windows.Forms.CheckBox();
            this.tbRAPos = new System.Windows.Forms.TextBox();
            this.tbDEPos = new System.Windows.Forms.TextBox();
            this.labelDriverId = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tbNavSpeed = new System.Windows.Forms.TextBox();
            this.cbTrackSpeed = new System.Windows.Forms.ComboBox();
            this.tbDESpeed = new System.Windows.Forms.TextBox();
            this.tbRASpeed = new System.Windows.Forms.TextBox();
            this.tbRAOffset = new System.Windows.Forms.TextBox();
            this.tbDEOffset = new System.Windows.Forms.TextBox();
            this.btnSetOffset = new System.Windows.Forms.Button();
            this.tbNewRA = new System.Windows.Forms.TextBox();
            this.tbNewDE = new System.Windows.Forms.TextBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.dateTimeUNC = new System.Windows.Forms.DateTimePicker();
            this.tbSideral = new System.Windows.Forms.TextBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.tbAzimuth = new System.Windows.Forms.TextBox();
            this.tbElevation = new System.Windows.Forms.TextBox();
            this.btnStepRight = new System.Windows.Forms.Button();
            this.btnStepLeft = new System.Windows.Forms.Button();
            this.btnStepDown = new System.Windows.Forms.Button();
            this.btnStepUp = new System.Windows.Forms.Button();
            this.tbGuideTimeout = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(309, 10);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(72, 23);
            this.buttonChoose.TabIndex = 0;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(309, 39);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbSlewing
            // 
            this.cbSlewing.AutoSize = true;
            this.cbSlewing.Location = new System.Drawing.Point(13, 78);
            this.cbSlewing.Name = "cbSlewing";
            this.cbSlewing.Size = new System.Drawing.Size(74, 17);
            this.cbSlewing.TabIndex = 3;
            this.cbSlewing.Text = "Is Slewing";
            this.cbSlewing.UseVisualStyleBackColor = true;
            // 
            // cbTracking
            // 
            this.cbTracking.AutoSize = true;
            this.cbTracking.Location = new System.Drawing.Point(94, 78);
            this.cbTracking.Name = "cbTracking";
            this.cbTracking.Size = new System.Drawing.Size(79, 17);
            this.cbTracking.TabIndex = 4;
            this.cbTracking.Text = "Is Tracking";
            this.cbTracking.UseVisualStyleBackColor = true;
            this.cbTracking.CheckedChanged += new System.EventHandler(this.cbTracking_CheckedChanged);
            // 
            // tbRAPos
            // 
            this.tbRAPos.Location = new System.Drawing.Point(329, 78);
            this.tbRAPos.Name = "tbRAPos";
            this.tbRAPos.Size = new System.Drawing.Size(68, 20);
            this.tbRAPos.TabIndex = 5;
            // 
            // tbDEPos
            // 
            this.tbDEPos.Location = new System.Drawing.Point(329, 104);
            this.tbDEPos.Name = "tbDEPos";
            this.tbDEPos.Size = new System.Drawing.Size(68, 20);
            this.tbDEPos.TabIndex = 6;
            // 
            // labelDriverId
            // 
            this.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.FMounterV1.Properties.Settings.Default, "DriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelDriverId.Location = new System.Drawing.Point(12, 40);
            this.labelDriverId.Name = "labelDriverId";
            this.labelDriverId.Size = new System.Drawing.Size(291, 21);
            this.labelDriverId.TabIndex = 2;
            this.labelDriverId.Text = global::ASCOM.FMounterV1.Properties.Settings.Default.DriverId;
            this.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(60, 116);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(36, 23);
            this.btnUp.TabIndex = 7;
            this.btnUp.Text = "^";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(61, 174);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(35, 23);
            this.btnDown.TabIndex = 8;
            this.btnDown.Text = "v";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseUp);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(94, 145);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(36, 23);
            this.btnRight.TabIndex = 9;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseUp);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(28, 145);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(32, 23);
            this.btnLeft.TabIndex = 10;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(328, 226);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(68, 23);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tbNavSpeed
            // 
            this.tbNavSpeed.Location = new System.Drawing.Point(8, 227);
            this.tbNavSpeed.Name = "tbNavSpeed";
            this.tbNavSpeed.Size = new System.Drawing.Size(52, 20);
            this.tbNavSpeed.TabIndex = 12;
            this.tbNavSpeed.Text = "500";
            // 
            // cbTrackSpeed
            // 
            this.cbTrackSpeed.FormattingEnabled = true;
            this.cbTrackSpeed.Items.AddRange(new object[] {
            "Sideral",
            "Lunar",
            "Solar"});
            this.cbTrackSpeed.Location = new System.Drawing.Point(179, 76);
            this.cbTrackSpeed.Name = "cbTrackSpeed";
            this.cbTrackSpeed.Size = new System.Drawing.Size(94, 21);
            this.cbTrackSpeed.TabIndex = 14;
            this.cbTrackSpeed.SelectedIndexChanged += new System.EventHandler(this.cbTrackSpeed_SelectedIndexChanged);
            // 
            // tbDESpeed
            // 
            this.tbDESpeed.Location = new System.Drawing.Point(239, 103);
            this.tbDESpeed.Name = "tbDESpeed";
            this.tbDESpeed.Size = new System.Drawing.Size(64, 20);
            this.tbDESpeed.TabIndex = 15;
            // 
            // tbRASpeed
            // 
            this.tbRASpeed.Location = new System.Drawing.Point(170, 103);
            this.tbRASpeed.Name = "tbRASpeed";
            this.tbRASpeed.Size = new System.Drawing.Size(64, 20);
            this.tbRASpeed.TabIndex = 16;
            // 
            // tbRAOffset
            // 
            this.tbRAOffset.Location = new System.Drawing.Point(170, 135);
            this.tbRAOffset.Name = "tbRAOffset";
            this.tbRAOffset.Size = new System.Drawing.Size(64, 20);
            this.tbRAOffset.TabIndex = 17;
            this.tbRAOffset.Text = "0";
            // 
            // tbDEOffset
            // 
            this.tbDEOffset.Location = new System.Drawing.Point(239, 135);
            this.tbDEOffset.Name = "tbDEOffset";
            this.tbDEOffset.Size = new System.Drawing.Size(64, 20);
            this.tbDEOffset.TabIndex = 18;
            this.tbDEOffset.Text = "0";
            // 
            // btnSetOffset
            // 
            this.btnSetOffset.Location = new System.Drawing.Point(198, 161);
            this.btnSetOffset.Name = "btnSetOffset";
            this.btnSetOffset.Size = new System.Drawing.Size(75, 23);
            this.btnSetOffset.TabIndex = 19;
            this.btnSetOffset.Text = "Set Offset";
            this.btnSetOffset.UseVisualStyleBackColor = true;
            this.btnSetOffset.Click += new System.EventHandler(this.btnSetOffset_Click);
            // 
            // tbNewRA
            // 
            this.tbNewRA.Location = new System.Drawing.Point(108, 227);
            this.tbNewRA.Name = "tbNewRA";
            this.tbNewRA.Size = new System.Drawing.Size(64, 20);
            this.tbNewRA.TabIndex = 20;
            // 
            // tbNewDE
            // 
            this.tbNewDE.Location = new System.Drawing.Point(177, 227);
            this.tbNewDE.Name = "tbNewDE";
            this.tbNewDE.Size = new System.Drawing.Size(64, 20);
            this.tbNewDE.TabIndex = 21;
            // 
            // btnGoto
            // 
            this.btnGoto.Location = new System.Drawing.Point(254, 225);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(68, 23);
            this.btnGoto.TabIndex = 22;
            this.btnGoto.Text = "Goto";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(254, 254);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(68, 23);
            this.btnSet.TabIndex = 23;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // dateTimeUNC
            // 
            this.dateTimeUNC.CustomFormat = "yyyy:MM:dd HH:MM:ss";
            this.dateTimeUNC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeUNC.Location = new System.Drawing.Point(128, 297);
            this.dateTimeUNC.Name = "dateTimeUNC";
            this.dateTimeUNC.Size = new System.Drawing.Size(136, 20);
            this.dateTimeUNC.TabIndex = 25;
            // 
            // tbSideral
            // 
            this.tbSideral.Location = new System.Drawing.Point(12, 297);
            this.tbSideral.Name = "tbSideral";
            this.tbSideral.Size = new System.Drawing.Size(100, 20);
            this.tbSideral.TabIndex = 26;
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(328, 255);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(68, 23);
            this.btnHome.TabIndex = 27;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // tbAzimuth
            // 
            this.tbAzimuth.Location = new System.Drawing.Point(422, 78);
            this.tbAzimuth.Name = "tbAzimuth";
            this.tbAzimuth.Size = new System.Drawing.Size(68, 20);
            this.tbAzimuth.TabIndex = 28;
            // 
            // tbElevation
            // 
            this.tbElevation.Location = new System.Drawing.Point(422, 103);
            this.tbElevation.Name = "tbElevation";
            this.tbElevation.Size = new System.Drawing.Size(68, 20);
            this.tbElevation.TabIndex = 29;
            // 
            // btnStepRight
            // 
            this.btnStepRight.Location = new System.Drawing.Point(136, 145);
            this.btnStepRight.Name = "btnStepRight";
            this.btnStepRight.Size = new System.Drawing.Size(19, 23);
            this.btnStepRight.TabIndex = 30;
            this.btnStepRight.UseVisualStyleBackColor = true;
            this.btnStepRight.Click += new System.EventHandler(this.btnStepRight_Click);
            // 
            // btnStepLeft
            // 
            this.btnStepLeft.Location = new System.Drawing.Point(3, 145);
            this.btnStepLeft.Name = "btnStepLeft";
            this.btnStepLeft.Size = new System.Drawing.Size(19, 23);
            this.btnStepLeft.TabIndex = 31;
            this.btnStepLeft.UseVisualStyleBackColor = true;
            this.btnStepLeft.Click += new System.EventHandler(this.btnStepLeft_Click);
            // 
            // btnStepDown
            // 
            this.btnStepDown.Location = new System.Drawing.Point(61, 200);
            this.btnStepDown.Name = "btnStepDown";
            this.btnStepDown.Size = new System.Drawing.Size(35, 20);
            this.btnStepDown.TabIndex = 32;
            this.btnStepDown.UseVisualStyleBackColor = true;
            this.btnStepDown.Click += new System.EventHandler(this.btnStepDown_Click);
            // 
            // btnStepUp
            // 
            this.btnStepUp.Location = new System.Drawing.Point(61, 90);
            this.btnStepUp.Name = "btnStepUp";
            this.btnStepUp.Size = new System.Drawing.Size(35, 20);
            this.btnStepUp.TabIndex = 33;
            this.btnStepUp.UseVisualStyleBackColor = true;
            this.btnStepUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnStepUp_MouseClick);
            // 
            // tbGuideTimeout
            // 
            this.tbGuideTimeout.Location = new System.Drawing.Point(8, 253);
            this.tbGuideTimeout.Name = "tbGuideTimeout";
            this.tbGuideTimeout.Size = new System.Drawing.Size(49, 20);
            this.tbGuideTimeout.TabIndex = 34;
            this.tbGuideTimeout.Text = "1000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 329);
            this.Controls.Add(this.tbGuideTimeout);
            this.Controls.Add(this.btnStepUp);
            this.Controls.Add(this.btnStepDown);
            this.Controls.Add(this.btnStepLeft);
            this.Controls.Add(this.btnStepRight);
            this.Controls.Add(this.tbElevation);
            this.Controls.Add(this.tbAzimuth);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.tbSideral);
            this.Controls.Add(this.dateTimeUNC);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnGoto);
            this.Controls.Add(this.tbNewDE);
            this.Controls.Add(this.tbNewRA);
            this.Controls.Add(this.btnSetOffset);
            this.Controls.Add(this.tbDEOffset);
            this.Controls.Add(this.tbRAOffset);
            this.Controls.Add(this.tbRASpeed);
            this.Controls.Add(this.tbDESpeed);
            this.Controls.Add(this.cbTrackSpeed);
            this.Controls.Add(this.tbNavSpeed);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.tbDEPos);
            this.Controls.Add(this.tbRAPos);
            this.Controls.Add(this.cbTracking);
            this.Controls.Add(this.cbSlewing);
            this.Controls.Add(this.labelDriverId);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonChoose);
            this.Name = "Form1";
            this.Text = "TEMPLATEDEVICETYPE Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelDriverId;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbSlewing;
        private System.Windows.Forms.CheckBox cbTracking;
        private System.Windows.Forms.TextBox tbRAPos;
        private System.Windows.Forms.TextBox tbDEPos;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox tbNavSpeed;
        private System.Windows.Forms.ComboBox cbTrackSpeed;
        private System.Windows.Forms.TextBox tbDESpeed;
        private System.Windows.Forms.TextBox tbRASpeed;
        private System.Windows.Forms.TextBox tbRAOffset;
        private System.Windows.Forms.TextBox tbDEOffset;
        private System.Windows.Forms.Button btnSetOffset;
        private System.Windows.Forms.TextBox tbNewRA;
        private System.Windows.Forms.TextBox tbNewDE;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.DateTimePicker dateTimeUNC;
        private System.Windows.Forms.TextBox tbSideral;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.TextBox tbAzimuth;
        private System.Windows.Forms.TextBox tbElevation;
        private System.Windows.Forms.Button btnStepRight;
        private System.Windows.Forms.Button btnStepLeft;
        private System.Windows.Forms.Button btnStepDown;
        private System.Windows.Forms.Button btnStepUp;
        private System.Windows.Forms.TextBox tbGuideTimeout;
    }
}

