using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

namespace Rescuebots
{
    public partial class MainForm : Form
    {

        private const int connectionSpeed = 9600;
        private const String messageBeginMarker = "#";
        private const String messageEndMarker = "%";
        private SerialPort serialPort;
        private MessageBuilder messageBuilder;


        public MainForm()
        {
            InitializeComponent();
            FillSerialPortSelectionBoxWithAvailablePorts();

            serialPort = new SerialPort();
            serialPort.BaudRate = connectionSpeed;
            // Choose: 9600, 19200 or 38400. Getting errors? Choose lower speed.
            // Also be sure that you set the speed on the arduino to the same value!

            messageBuilder = new MessageBuilder(messageBeginMarker, messageEndMarker);
        }



        private void refreshSerialPortsButton_Click(object sender, EventArgs e)
        {
            FillSerialPortSelectionBoxWithAvailablePorts();
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                readMessageTimer.Enabled = false;
                serialPort.Close();
            }
            else
            {
                String port = serialPortSelectionBox.Text;
                try
                {
                    serialPort.PortName = port;
                    serialPort.Open();
                    if (serialPort.IsOpen)
                    {
                        ClearAllMessageData();
                        serialPort.DiscardInBuffer();
                        serialPort.DiscardOutBuffer();
                    }
                    readMessageTimer.Enabled = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Could not connect to the given serial port: " + exception.Message);
                }
            }

            UpdateUserInterface();
        }
        private void DisplayReceivedMessage(String message)
        {
            receivedMessagesListBox.Items.Insert(0, message);
        }
        private void DisplaySendMessage(String message)
        {
            sendMessagesListBox.Items.Insert(0, message);
        }
        private void serialPortSelectionBox_Leave(object sender, EventArgs e)
        {
            serialPortSelectionBox.Text = serialPortSelectionBox.Text.ToUpper();
        }
        private void DisplayReceivedRawData(String data)
        {
            receivedRawDataTextBox.AppendText(data);
        }
        private void messageReceiveTimer_Tick(object sender, EventArgs e)
        {
            if (serialPort.IsOpen
                && serialPort.BytesToRead > 0)
            {
                try
                {
                    String dataFromSocket = serialPort.ReadExisting();
                    DisplayReceivedRawData(dataFromSocket);
                    messageBuilder.Append(dataFromSocket);
                    ProcessMessages();
                }
                catch (Exception exception) // Not very nice to catch Exception...but for now it's good enough.
                {
                    Debug.WriteLine("Could not read from serial port: " + exception.Message);
                }
            }
        }
        private void ClearAllMessageData()
        {
            sendMessagesListBox.Items.Clear();
            receivedMessagesListBox.Items.Clear();
            receivedRawDataTextBox.Clear();
            messageBuilder.Clear();
        }
        private void ProcessMessages()
        {
            String message = messageBuilder.FindAndRemoveNextMessage();
            while (message != null)
            {
                DisplayReceivedMessage(message);
                MessageReceived(message);
                message = messageBuilder.FindAndRemoveNextMessage();
            }
        }
        private void UpdateUserInterface()
        {
            bool isConnected = serialPort.IsOpen;
            if (isConnected)
            {
                connectButton.Text = "Disconnect";
            }
            else
            {
                connectButton.Text = "Connect";
            }
            refreshSerialPortsButton.Enabled = !isConnected;
            serialPortSelectionBox.Enabled = !isConnected;
            receivedMessagesGroupBox.Enabled = isConnected;
            sendMessagesGroupBox.Enabled = isConnected;
            receivedRawDataGroupBox.Enabled = isConnected;
        }
        private void MessageReceived(String message)
        {
            if (message.Contains("#%"))
            {
                string Inbox = message;
                int StartValue = Inbox.IndexOf(":") + 1;
                int EndValue = Inbox.IndexOf("%");
                string Value = Inbox.Substring(StartValue, EndValue - StartValue);
                int Value2 = Convert.ToInt32(Value);

            }
        }
        private bool SendMessage(String message)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(message);
                    DisplaySendMessage(message);
                    return true;
                }
                catch (Exception exception) // Not very nice to catch Exception...but for now it's good enough.
                {
                    Debug.WriteLine("Could not write to serial port: " + exception.Message);
                }
            }
            return false;
        }
        private void FillMapBtn_Click(object sender, EventArgs e)
        {
            field.FillWithValues();
        }
        private void YCOORDINATEnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
         
            
            


        }
        private void XCOORDINATEnumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }
        private void SendCoordinatesBtn_Click(object sender, EventArgs e)
        {
            SendMessage("#" + textBox1.Text + "%");
        }
        private void FillSerialPortSelectionBoxWithAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);

            serialPortSelectionBox.Items.Clear();
            foreach (String port in ports)
            {
                serialPortSelectionBox.Items.Add(port);
            }
        }
    }
}
