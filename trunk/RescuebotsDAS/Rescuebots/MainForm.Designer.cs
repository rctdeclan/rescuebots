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
            this.field = new Rescuebots.Field();
            this.receivedRawDataGroupBox.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.receivedMessagesGroupBox.SuspendLayout();
            this.sendMessagesGroupBox.SuspendLayout();
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
            this.FillMapBtn.Location = new System.Drawing.Point(931, 542);
            this.FillMapBtn.Name = "FillMapBtn";
            this.FillMapBtn.Size = new System.Drawing.Size(75, 23);
            this.FillMapBtn.TabIndex = 111;
            this.FillMapBtn.Text = "Fill Map";
            this.FillMapBtn.UseVisualStyleBackColor = true;
            this.FillMapBtn.Click += new System.EventHandler(this.FillMapBtn_Click);
            // 
            // field
            // 
            this.field.Location = new System.Drawing.Point(0, 0);
            this.field.Name = "field";
            this.field.Size = new System.Drawing.Size(700, 700);
            this.field.TabIndex = 112;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 702);
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
    }
}

