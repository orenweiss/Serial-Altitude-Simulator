namespace Serial_Altitude_Simulator
{
    partial class SerialAltSimForm
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
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.transmitTimer = new System.Windows.Forms.Timer(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.openButton = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comPortsComboBox = new System.Windows.Forms.ComboBox();
            this.protocolComboBox = new System.Windows.Forms.ComboBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.altitudeSelector = new Serial_Altitude_Simulator.FormattedNumericUpDown();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.altitudeSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort_ErrorReceived);
            // 
            // transmitTimer
            // 
            this.transmitTimer.Interval = 1000;
            this.transmitTimer.Tick += new System.EventHandler(this.transmitTimer_Tick);
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openButton.Location = new System.Drawing.Point(362, 216);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 3;
            this.openButton.TabStop = false;
            this.openButton.Text = "Open Port";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.comPortsComboBox);
            this.groupBox.Controls.Add(this.protocolComboBox);
            this.groupBox.Controls.Add(this.altitudeSelector);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(425, 185);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Protocol:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "COM Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Altitude:";
            // 
            // comPortsComboBox
            // 
            this.comPortsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comPortsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortsComboBox.FormattingEnabled = true;
            this.comPortsComboBox.Location = new System.Drawing.Point(107, 115);
            this.comPortsComboBox.Name = "comPortsComboBox";
            this.comPortsComboBox.Size = new System.Drawing.Size(287, 21);
            this.comPortsComboBox.TabIndex = 2;
            this.comPortsComboBox.TabStop = false;
            this.comPortsComboBox.DropDown += new System.EventHandler(this.comPortsComboBox_DropDown);
            this.comPortsComboBox.SelectionChangeCommitted += new System.EventHandler(this.comPortsComboBox_SelectionChangeCommitted);
            // 
            // protocolComboBox
            // 
            this.protocolComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.protocolComboBox.DisplayMember = "altitude";
            this.protocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolComboBox.FormattingEnabled = true;
            this.protocolComboBox.Items.AddRange(new object[] {
            "Trimble/Garmin Nav Devices",
            "UPS Aviation/Garmin AT/IIMorrow Nav Devices",
            "Northstar Nav Devices",
            "UPS AT 618 Loran Devices",
            "Magellan Nav Devices",
            "Shadin One-Foot Resolution"});
            this.protocolComboBox.Location = new System.Drawing.Point(107, 142);
            this.protocolComboBox.Name = "protocolComboBox";
            this.protocolComboBox.Size = new System.Drawing.Size(287, 21);
            this.protocolComboBox.TabIndex = 1;
            this.protocolComboBox.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Enabled = false;
            this.closeButton.Location = new System.Drawing.Point(281, 216);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "Close Port";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // altitudeSelector
            // 
            this.altitudeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.altitudeSelector.Floor = 25;
            this.altitudeSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.altitudeSelector.FormatString = "00000 ft";
            this.altitudeSelector.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.altitudeSelector.Location = new System.Drawing.Point(41, 43);
            this.altitudeSelector.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.altitudeSelector.Maximum = new decimal(new int[] {
            99500,
            0,
            0,
            0});
            this.altitudeSelector.Minimum = new decimal(new int[] {
            99500,
            0,
            0,
            -2147483648});
            this.altitudeSelector.Name = "altitudeSelector";
            this.altitudeSelector.Size = new System.Drawing.Size(353, 49);
            this.altitudeSelector.TabIndex = 0;
            this.altitudeSelector.TabStop = false;
            this.altitudeSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.altitudeSelector.ValueChanged += new System.EventHandler(this.altitudeSelector_ValueChanged);
            this.altitudeSelector.MouseUp += new System.Windows.Forms.MouseEventHandler(this.altitudeSelector_MouseUp);
            // 
            // SerialAltSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 251);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.openButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Name = "SerialAltSimForm";
            this.Text = "Serial Altitude Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialAltSimForm_FormClosing);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.altitudeSelector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Timer transmitTimer;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.GroupBox groupBox;
        private FormattedNumericUpDown altitudeSelector;
        private System.Windows.Forms.ComboBox comPortsComboBox;
        private System.Windows.Forms.ComboBox protocolComboBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

