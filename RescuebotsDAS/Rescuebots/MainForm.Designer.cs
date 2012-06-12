﻿namespace Rescuebots
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
            this.SendCoordinatesBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.receivedRawDataGroupBox.Location = new System.Drawing.Point(955, 502);
            this.receivedRawDataGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.receivedRawDataGroupBox.Name = "receivedRawDataGroupBox";
            this.receivedRawDataGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.receivedRawDataGroupBox.Size = new System.Drawing.Size(395, 158);
            this.receivedRawDataGroupBox.TabIndex = 8;
            this.receivedRawDataGroupBox.TabStop = false;
            this.receivedRawDataGroupBox.Text = "Received raw data";
            // 
            // receivedRawDataTextBox
            // 
            this.receivedRawDataTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.receivedRawDataTextBox.Location = new System.Drawing.Point(9, 25);
            this.receivedRawDataTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.receivedRawDataTextBox.Multiline = true;
            this.receivedRawDataTextBox.Name = "receivedRawDataTextBox";
            this.receivedRawDataTextBox.ReadOnly = true;
            this.receivedRawDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.receivedRawDataTextBox.Size = new System.Drawing.Size(376, 125);
            this.receivedRawDataTextBox.TabIndex = 0;
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.Controls.Add(this.serialPortSelectionBox);
            this.connectionGroupBox.Controls.Add(this.refreshSerialPortsButton);
            this.connectionGroupBox.Controls.Add(this.connectButton);
            this.connectionGroupBox.Location = new System.Drawing.Point(956, 15);
            this.connectionGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.connectionGroupBox.Size = new System.Drawing.Size(395, 68);
            this.connectionGroupBox.TabIndex = 9;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Connection";
            // 
            // serialPortSelectionBox
            // 
            this.serialPortSelectionBox.FormattingEnabled = true;
            this.serialPortSelectionBox.Location = new System.Drawing.Point(149, 26);
            this.serialPortSelectionBox.Margin = new System.Windows.Forms.Padding(4);
            this.serialPortSelectionBox.Name = "serialPortSelectionBox";
            this.serialPortSelectionBox.Size = new System.Drawing.Size(101, 24);
            this.serialPortSelectionBox.TabIndex = 1;
            // 
            // refreshSerialPortsButton
            // 
            this.refreshSerialPortsButton.Location = new System.Drawing.Point(36, 23);
            this.refreshSerialPortsButton.Margin = new System.Windows.Forms.Padding(4);
            this.refreshSerialPortsButton.Name = "refreshSerialPortsButton";
            this.refreshSerialPortsButton.Size = new System.Drawing.Size(105, 28);
            this.refreshSerialPortsButton.TabIndex = 0;
            this.refreshSerialPortsButton.Text = "Rescan ports";
            this.refreshSerialPortsButton.UseVisualStyleBackColor = true;
            this.refreshSerialPortsButton.Click += new System.EventHandler(this.refreshSerialPortsButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(260, 23);
            this.connectButton.Margin = new System.Windows.Forms.Padding(4);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 28);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // receivedMessagesGroupBox
            // 
            this.receivedMessagesGroupBox.Controls.Add(this.receivedMessagesListBox);
            this.receivedMessagesGroupBox.Location = new System.Drawing.Point(955, 297);
            this.receivedMessagesGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.receivedMessagesGroupBox.Name = "receivedMessagesGroupBox";
            this.receivedMessagesGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.receivedMessagesGroupBox.Size = new System.Drawing.Size(395, 199);
            this.receivedMessagesGroupBox.TabIndex = 7;
            this.receivedMessagesGroupBox.TabStop = false;
            this.receivedMessagesGroupBox.Text = "Received Messages";
            // 
            // receivedMessagesListBox
            // 
            this.receivedMessagesListBox.FormattingEnabled = true;
            this.receivedMessagesListBox.ItemHeight = 16;
            this.receivedMessagesListBox.Location = new System.Drawing.Point(9, 25);
            this.receivedMessagesListBox.Margin = new System.Windows.Forms.Padding(4);
            this.receivedMessagesListBox.Name = "receivedMessagesListBox";
            this.receivedMessagesListBox.Size = new System.Drawing.Size(377, 164);
            this.receivedMessagesListBox.TabIndex = 0;
            // 
            // sendMessagesGroupBox
            // 
            this.sendMessagesGroupBox.Controls.Add(this.sendMessagesListBox);
            this.sendMessagesGroupBox.Location = new System.Drawing.Point(955, 90);
            this.sendMessagesGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.sendMessagesGroupBox.Name = "sendMessagesGroupBox";
            this.sendMessagesGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.sendMessagesGroupBox.Size = new System.Drawing.Size(396, 199);
            this.sendMessagesGroupBox.TabIndex = 6;
            this.sendMessagesGroupBox.TabStop = false;
            this.sendMessagesGroupBox.Text = "Send messages";
            // 
            // sendMessagesListBox
            // 
            this.sendMessagesListBox.FormattingEnabled = true;
            this.sendMessagesListBox.ItemHeight = 16;
            this.sendMessagesListBox.Location = new System.Drawing.Point(9, 25);
            this.sendMessagesListBox.Margin = new System.Windows.Forms.Padding(4);
            this.sendMessagesListBox.Name = "sendMessagesListBox";
            this.sendMessagesListBox.Size = new System.Drawing.Size(377, 164);
            this.sendMessagesListBox.TabIndex = 0;
            // 
            // readMessageTimer
            // 
            this.readMessageTimer.Interval = 15;
            this.readMessageTimer.Tick += new System.EventHandler(this.messageReceiveTimer_Tick);
            // 
            // FillMapBtn
            // 
            this.FillMapBtn.Location = new System.Drawing.Point(1179, 667);
            this.FillMapBtn.Margin = new System.Windows.Forms.Padding(4);
            this.FillMapBtn.Name = "FillMapBtn";
            this.FillMapBtn.Size = new System.Drawing.Size(163, 28);
            this.FillMapBtn.TabIndex = 111;
            this.FillMapBtn.Text = "Fill Map";
            this.FillMapBtn.UseVisualStyleBackColor = true;
            this.FillMapBtn.Click += new System.EventHandler(this.FillMapBtn_Click);
            // 
            // SendCoordinatesBtn
            // 
            this.SendCoordinatesBtn.Location = new System.Drawing.Point(1179, 745);
            this.SendCoordinatesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SendCoordinatesBtn.Name = "SendCoordinatesBtn";
            this.SendCoordinatesBtn.Size = new System.Drawing.Size(163, 28);
            this.SendCoordinatesBtn.TabIndex = 115;
            this.SendCoordinatesBtn.Text = "Send Coördinates";
            this.SendCoordinatesBtn.UseVisualStyleBackColor = true;
            this.SendCoordinatesBtn.Click += new System.EventHandler(this.SendCoordinatesBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1043, 748);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 116;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // field
            // 
            this.field.Location = new System.Drawing.Point(0, 0);
            this.field.Margin = new System.Windows.Forms.Padding(4);
            this.field.Name = "field";
            this.field.Size = new System.Drawing.Size(933, 862);
            this.field.TabIndex = 112;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 864);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SendCoordinatesBtn);
            this.Controls.Add(this.field);
            this.Controls.Add(this.FillMapBtn);
            this.Controls.Add(this.receivedRawDataGroupBox);
            this.Controls.Add(this.connectionGroupBox);
            this.Controls.Add(this.receivedMessagesGroupBox);
            this.Controls.Add(this.sendMessagesGroupBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Rescue Bots";
            this.receivedRawDataGroupBox.ResumeLayout(false);
            this.receivedRawDataGroupBox.PerformLayout();
            this.connectionGroupBox.ResumeLayout(false);
            this.receivedMessagesGroupBox.ResumeLayout(false);
            this.sendMessagesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button SendCoordinatesBtn;
        private System.Windows.Forms.TextBox textBox1;
    }
}

