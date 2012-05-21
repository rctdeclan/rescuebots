namespace ArdDangerCom
{
    partial class ArDangerComMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArDangerComMainForm));
            this.dangerShieldPictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.sliderB = new System.Windows.Forms.TrackBar();
            this.sliderA = new System.Windows.Forms.TrackBar();
            this.sendMessagesGroupBox = new System.Windows.Forms.GroupBox();
            this.sendMessagesListBox = new System.Windows.Forms.ListBox();
            this.receivedMessagesGroupBox = new System.Windows.Forms.GroupBox();
            this.receivedMessagesListBox = new System.Windows.Forms.ListBox();
            this.readMessageTimer = new System.Windows.Forms.Timer(this.components);
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.serialPortSelectionBox = new System.Windows.Forms.ComboBox();
            this.refreshSerialPortsButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.sliderC = new System.Windows.Forms.TrackBar();
            this.sliderALabel = new System.Windows.Forms.Label();
            this.sliderBLabel = new System.Windows.Forms.Label();
            this.sliderCLabel = new System.Windows.Forms.Label();
            this.receivedRawDataGroupBox = new System.Windows.Forms.GroupBox();
            this.receivedRawDataTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dangerShieldPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderA)).BeginInit();
            this.sendMessagesGroupBox.SuspendLayout();
            this.receivedMessagesGroupBox.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderC)).BeginInit();
            this.receivedRawDataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dangerShieldPictureBox
            // 
            this.dangerShieldPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("dangerShieldPictureBox.Image")));
            this.dangerShieldPictureBox.Location = new System.Drawing.Point(12, 12);
            this.dangerShieldPictureBox.Name = "dangerShieldPictureBox";
            this.dangerShieldPictureBox.Size = new System.Drawing.Size(406, 524);
            this.dangerShieldPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.dangerShieldPictureBox.TabIndex = 0;
            this.dangerShieldPictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(281, 466);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(23, 20);
            this.button3.TabIndex = 1;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(208, 466);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 20);
            this.button2.TabIndex = 1;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sliderB
            // 
            this.sliderB.AutoSize = false;
            this.sliderB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sliderB.Enabled = false;
            this.sliderB.LargeChange = 1;
            this.sliderB.Location = new System.Drawing.Point(209, 58);
            this.sliderB.Maximum = 100;
            this.sliderB.Name = "sliderB";
            this.sliderB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderB.Size = new System.Drawing.Size(24, 350);
            this.sliderB.TabIndex = 2;
            this.sliderB.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderB.Value = 100;
            // 
            // sliderA
            // 
            this.sliderA.AutoSize = false;
            this.sliderA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sliderA.LargeChange = 1;
            this.sliderA.Location = new System.Drawing.Point(132, 58);
            this.sliderA.Maximum = 100;
            this.sliderA.Name = "sliderA";
            this.sliderA.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderA.Size = new System.Drawing.Size(24, 350);
            this.sliderA.TabIndex = 2;
            this.sliderA.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderA.Value = 100;
            this.sliderA.ValueChanged += new System.EventHandler(this.sliderA_ValueChanged);
            // 
            // sendMessagesGroupBox
            // 
            this.sendMessagesGroupBox.Controls.Add(this.sendMessagesListBox);
            this.sendMessagesGroupBox.Location = new System.Drawing.Point(424, 73);
            this.sendMessagesGroupBox.Name = "sendMessagesGroupBox";
            this.sendMessagesGroupBox.Size = new System.Drawing.Size(297, 162);
            this.sendMessagesGroupBox.TabIndex = 3;
            this.sendMessagesGroupBox.TabStop = false;
            this.sendMessagesGroupBox.Text = "Send messages";
            // 
            // sendMessagesListBox
            // 
            this.sendMessagesListBox.FormattingEnabled = true;
            this.sendMessagesListBox.Location = new System.Drawing.Point(7, 20);
            this.sendMessagesListBox.Name = "sendMessagesListBox";
            this.sendMessagesListBox.Size = new System.Drawing.Size(284, 134);
            this.sendMessagesListBox.TabIndex = 0;
            // 
            // receivedMessagesGroupBox
            // 
            this.receivedMessagesGroupBox.Controls.Add(this.receivedMessagesListBox);
            this.receivedMessagesGroupBox.Location = new System.Drawing.Point(424, 241);
            this.receivedMessagesGroupBox.Name = "receivedMessagesGroupBox";
            this.receivedMessagesGroupBox.Size = new System.Drawing.Size(296, 162);
            this.receivedMessagesGroupBox.TabIndex = 4;
            this.receivedMessagesGroupBox.TabStop = false;
            this.receivedMessagesGroupBox.Text = "Received Messages";
            // 
            // receivedMessagesListBox
            // 
            this.receivedMessagesListBox.FormattingEnabled = true;
            this.receivedMessagesListBox.Location = new System.Drawing.Point(7, 20);
            this.receivedMessagesListBox.Name = "receivedMessagesListBox";
            this.receivedMessagesListBox.Size = new System.Drawing.Size(284, 134);
            this.receivedMessagesListBox.TabIndex = 0;
            // 
            // readMessageTimer
            // 
            this.readMessageTimer.Interval = 15;
            this.readMessageTimer.Tick += new System.EventHandler(this.messageReceiveTimer_Tick);
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.Controls.Add(this.serialPortSelectionBox);
            this.connectionGroupBox.Controls.Add(this.refreshSerialPortsButton);
            this.connectionGroupBox.Controls.Add(this.connectButton);
            this.connectionGroupBox.Location = new System.Drawing.Point(425, 12);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Size = new System.Drawing.Size(296, 55);
            this.connectionGroupBox.TabIndex = 5;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Connection";
            // 
            // serialPortSelectionBox
            // 
            this.serialPortSelectionBox.FormattingEnabled = true;
            this.serialPortSelectionBox.Location = new System.Drawing.Point(112, 21);
            this.serialPortSelectionBox.Name = "serialPortSelectionBox";
            this.serialPortSelectionBox.Size = new System.Drawing.Size(77, 21);
            this.serialPortSelectionBox.TabIndex = 1;
            this.serialPortSelectionBox.Leave += new System.EventHandler(this.serialPortSelectionBox_Leave);
            // 
            // refreshSerialPortsButton
            // 
            this.refreshSerialPortsButton.Location = new System.Drawing.Point(27, 19);
            this.refreshSerialPortsButton.Name = "refreshSerialPortsButton";
            this.refreshSerialPortsButton.Size = new System.Drawing.Size(79, 23);
            this.refreshSerialPortsButton.TabIndex = 0;
            this.refreshSerialPortsButton.Text = "Rescan ports";
            this.refreshSerialPortsButton.UseVisualStyleBackColor = true;
            this.refreshSerialPortsButton.Click += new System.EventHandler(this.refreshSerialPortsButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(195, 19);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // sliderC
            // 
            this.sliderC.AutoSize = false;
            this.sliderC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sliderC.Enabled = false;
            this.sliderC.LargeChange = 1;
            this.sliderC.Location = new System.Drawing.Point(284, 58);
            this.sliderC.Maximum = 100;
            this.sliderC.Name = "sliderC";
            this.sliderC.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderC.Size = new System.Drawing.Size(24, 350);
            this.sliderC.TabIndex = 2;
            this.sliderC.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderC.Value = 100;
            this.sliderC.Scroll += new System.EventHandler(this.sliderC_Scroll_1);
            // 
            // sliderALabel
            // 
            this.sliderALabel.AutoSize = true;
            this.sliderALabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sliderALabel.Location = new System.Drawing.Point(132, 415);
            this.sliderALabel.Name = "sliderALabel";
            this.sliderALabel.Size = new System.Drawing.Size(21, 20);
            this.sliderALabel.TabIndex = 6;
            this.sliderALabel.Text = "A";
            // 
            // sliderBLabel
            // 
            this.sliderBLabel.AutoSize = true;
            this.sliderBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sliderBLabel.Location = new System.Drawing.Point(210, 415);
            this.sliderBLabel.Name = "sliderBLabel";
            this.sliderBLabel.Size = new System.Drawing.Size(21, 20);
            this.sliderBLabel.TabIndex = 6;
            this.sliderBLabel.Text = "B";
            // 
            // sliderCLabel
            // 
            this.sliderCLabel.AutoSize = true;
            this.sliderCLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sliderCLabel.Location = new System.Drawing.Point(283, 415);
            this.sliderCLabel.Name = "sliderCLabel";
            this.sliderCLabel.Size = new System.Drawing.Size(21, 20);
            this.sliderCLabel.TabIndex = 6;
            this.sliderCLabel.Text = "C";
            // 
            // receivedRawDataGroupBox
            // 
            this.receivedRawDataGroupBox.Controls.Add(this.receivedRawDataTextBox);
            this.receivedRawDataGroupBox.Location = new System.Drawing.Point(424, 409);
            this.receivedRawDataGroupBox.Name = "receivedRawDataGroupBox";
            this.receivedRawDataGroupBox.Size = new System.Drawing.Size(296, 128);
            this.receivedRawDataGroupBox.TabIndex = 5;
            this.receivedRawDataGroupBox.TabStop = false;
            this.receivedRawDataGroupBox.Text = "Received raw data";
            // 
            // receivedRawDataTextBox
            // 
            this.receivedRawDataTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.receivedRawDataTextBox.Location = new System.Drawing.Point(7, 20);
            this.receivedRawDataTextBox.Multiline = true;
            this.receivedRawDataTextBox.Name = "receivedRawDataTextBox";
            this.receivedRawDataTextBox.ReadOnly = true;
            this.receivedRawDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.receivedRawDataTextBox.Size = new System.Drawing.Size(283, 102);
            this.receivedRawDataTextBox.TabIndex = 0;
            // 
            // ArDangerComMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 549);
            this.Controls.Add(this.receivedRawDataGroupBox);
            this.Controls.Add(this.sliderCLabel);
            this.Controls.Add(this.sliderBLabel);
            this.Controls.Add(this.sliderALabel);
            this.Controls.Add(this.connectionGroupBox);
            this.Controls.Add(this.receivedMessagesGroupBox);
            this.Controls.Add(this.sendMessagesGroupBox);
            this.Controls.Add(this.sliderA);
            this.Controls.Add(this.sliderC);
            this.Controls.Add(this.sliderB);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dangerShieldPictureBox);
            this.Name = "ArDangerComMainForm";
            this.Text = "ArDangerCom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ArDangerComMainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dangerShieldPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderA)).EndInit();
            this.sendMessagesGroupBox.ResumeLayout(false);
            this.receivedMessagesGroupBox.ResumeLayout(false);
            this.connectionGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sliderC)).EndInit();
            this.receivedRawDataGroupBox.ResumeLayout(false);
            this.receivedRawDataGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox dangerShieldPictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TrackBar sliderB;
        private System.Windows.Forms.TrackBar sliderA;
        private System.Windows.Forms.GroupBox sendMessagesGroupBox;
        private System.Windows.Forms.GroupBox receivedMessagesGroupBox;
        private System.Windows.Forms.ListBox sendMessagesListBox;
        private System.Windows.Forms.ListBox receivedMessagesListBox;
        private System.Windows.Forms.Timer readMessageTimer;
        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button refreshSerialPortsButton;
        private System.Windows.Forms.ComboBox serialPortSelectionBox;
        private System.Windows.Forms.TrackBar sliderC;
        private System.Windows.Forms.Label sliderALabel;
        private System.Windows.Forms.Label sliderBLabel;
        private System.Windows.Forms.Label sliderCLabel;
        private System.Windows.Forms.GroupBox receivedRawDataGroupBox;
        private System.Windows.Forms.TextBox receivedRawDataTextBox;
    }
}

