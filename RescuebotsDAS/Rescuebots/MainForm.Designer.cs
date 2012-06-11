namespace Rescuebots
{
    partial class MainForm
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
            this.receivedRawDataGroupBox = new System.Windows.Forms.GroupBox();
            this.receivedRawDataTextBox = new System.Windows.Forms.TextBox();
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.serialPortSelectionBox = new System.Windows.Forms.ComboBox();
            this.refreshSerialPortsButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.receivedMessagesGroupBox = new System.Windows.Forms.GroupBox();
            this.receivedMessagesListBox = new System.Windows.Forms.ListBox();
            this.sendMessagesGroupBox = new System.Windows.Forms.GroupBox();
            this.sendMessagesListBox = new System.Windows.Forms.ListBox();
            this.readMessageTimer = new System.Windows.Forms.Timer(this.components);
            this.FillMapBtn = new System.Windows.Forms.Button();
            this.XCOORDINATEnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.YCOORDINATEnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.field = new Rescuebots.Field();
            this.SendCoordinatesBtn = new System.Windows.Forms.Button();
            this.receivedRawDataGroupBox.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.receivedMessagesGroupBox.SuspendLayout();
            this.sendMessagesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XCOORDINATEnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCOORDINATEnumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // receivedRawDataGroupBox
            // 
            this.receivedRawDataGroupBox.Controls.Add(this.receivedRawDataTextBox);
            this.receivedRawDataGroupBox.Location = new System.Drawing.Point(716, 408);
            this.receivedRawDataGroupBox.Name = "receivedRawDataGroupBox";
            this.receivedRawDataGroupBox.Size = new System.Drawing.Size(296, 128);
            this.receivedRawDataGroupBox.TabIndex = 8;
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
            // connectionGroupBox
            // 
            this.connectionGroupBox.Controls.Add(this.serialPortSelectionBox);
            this.connectionGroupBox.Controls.Add(this.refreshSerialPortsButton);
            this.connectionGroupBox.Controls.Add(this.connectButton);
            this.connectionGroupBox.Location = new System.Drawing.Point(717, 12);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Size = new System.Drawing.Size(296, 55);
            this.connectionGroupBox.TabIndex = 9;
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
            // 
            // receivedMessagesGroupBox
            // 
            this.receivedMessagesGroupBox.Controls.Add(this.receivedMessagesListBox);
            this.receivedMessagesGroupBox.Location = new System.Drawing.Point(716, 241);
            this.receivedMessagesGroupBox.Name = "receivedMessagesGroupBox";
            this.receivedMessagesGroupBox.Size = new System.Drawing.Size(296, 162);
            this.receivedMessagesGroupBox.TabIndex = 7;
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
            // sendMessagesGroupBox
            // 
            this.sendMessagesGroupBox.Controls.Add(this.sendMessagesListBox);
            this.sendMessagesGroupBox.Location = new System.Drawing.Point(716, 73);
            this.sendMessagesGroupBox.Name = "sendMessagesGroupBox";
            this.sendMessagesGroupBox.Size = new System.Drawing.Size(297, 162);
            this.sendMessagesGroupBox.TabIndex = 6;
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
            // readMessageTimer
            // 
            this.readMessageTimer.Interval = 15;
            // 
            // FillMapBtn
            // 
            this.FillMapBtn.Location = new System.Drawing.Point(884, 542);
            this.FillMapBtn.Name = "FillMapBtn";
            this.FillMapBtn.Size = new System.Drawing.Size(122, 23);
            this.FillMapBtn.TabIndex = 111;
            this.FillMapBtn.Text = "Fill Map";
            this.FillMapBtn.UseVisualStyleBackColor = true;
            this.FillMapBtn.Click += new System.EventHandler(this.FillMapBtn_Click);
            // 
            // XCOORDINATEnumericUpDown
            // 
            this.XCOORDINATEnumericUpDown.Location = new System.Drawing.Point(765, 583);
            this.XCOORDINATEnumericUpDown.Name = "XCOORDINATEnumericUpDown";
            this.XCOORDINATEnumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.XCOORDINATEnumericUpDown.TabIndex = 113;
            this.XCOORDINATEnumericUpDown.ValueChanged += new System.EventHandler(this.XCOORDINATEnumericUpDown_ValueChanged);
            // 
            // YCOORDINATEnumericUpDown
            // 
            this.YCOORDINATEnumericUpDown.Location = new System.Drawing.Point(765, 635);
            this.YCOORDINATEnumericUpDown.Name = "YCOORDINATEnumericUpDown";
            this.YCOORDINATEnumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.YCOORDINATEnumericUpDown.TabIndex = 114;
            this.YCOORDINATEnumericUpDown.ValueChanged += new System.EventHandler(this.YCOORDINATEnumericUpDown_ValueChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.WriteBufferSize = 5000;
            // 
            // field
            // 
            this.field.Location = new System.Drawing.Point(0, 0);
            this.field.Name = "field";
            this.field.Size = new System.Drawing.Size(700, 700);
            this.field.TabIndex = 112;
            // 
            // SendCoordinatesBtn
            // 
            this.SendCoordinatesBtn.Location = new System.Drawing.Point(884, 605);
            this.SendCoordinatesBtn.Name = "SendCoordinatesBtn";
            this.SendCoordinatesBtn.Size = new System.Drawing.Size(122, 23);
            this.SendCoordinatesBtn.TabIndex = 115;
            this.SendCoordinatesBtn.Text = "Send Coördinates";
            this.SendCoordinatesBtn.UseVisualStyleBackColor = true;
            this.SendCoordinatesBtn.Click += new System.EventHandler(this.SendCoordinatesBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 702);
            this.Controls.Add(this.SendCoordinatesBtn);
            this.Controls.Add(this.YCOORDINATEnumericUpDown);
            this.Controls.Add(this.XCOORDINATEnumericUpDown);
            this.Controls.Add(this.field);
            this.Controls.Add(this.FillMapBtn);
            this.Controls.Add(this.receivedRawDataGroupBox);
            this.Controls.Add(this.connectionGroupBox);
            this.Controls.Add(this.receivedMessagesGroupBox);
            this.Controls.Add(this.sendMessagesGroupBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Rescue Bots";
            this.receivedRawDataGroupBox.ResumeLayout(false);
            this.receivedRawDataGroupBox.PerformLayout();
            this.connectionGroupBox.ResumeLayout(false);
            this.receivedMessagesGroupBox.ResumeLayout(false);
            this.sendMessagesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.XCOORDINATEnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCOORDINATEnumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox receivedRawDataGroupBox;
        private System.Windows.Forms.TextBox receivedRawDataTextBox;
        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.ComboBox serialPortSelectionBox;
        private System.Windows.Forms.Button refreshSerialPortsButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.GroupBox receivedMessagesGroupBox;
        private System.Windows.Forms.ListBox receivedMessagesListBox;
        private System.Windows.Forms.GroupBox sendMessagesGroupBox;
        private System.Windows.Forms.ListBox sendMessagesListBox;
        private System.Windows.Forms.Timer readMessageTimer;
        private System.Windows.Forms.Button FillMapBtn;
        private Field field;
        private System.Windows.Forms.NumericUpDown XCOORDINATEnumericUpDown;
        private System.Windows.Forms.NumericUpDown YCOORDINATEnumericUpDown;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button SendCoordinatesBtn;
    }
}

