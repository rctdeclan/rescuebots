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
        private CommControl COM;
        private const int connectionSpeed = 38400;
        private const String messageBeginMarker = "#";
        private const String messageEndMarker = "%";
        private SerialPort serialPort;
        private MessageBuilder messageBuilder;


        public MainForm()
        {
            InitializeComponent();
            COM = new CommControl();
            COM.scann();
            serialPortSelectionBoxUpdate();
        }

        private void serialPortSelectionBoxUpdate()
        {
            serialPortSelectionBox.Items.Clear();
            serialPortSelectionBox.Items.Add(COM.GetComPortList());
        }

        private void refreshSerialPortsButton_Click(object sender, EventArgs e)
        {
            COM.scann();
            serialPortSelectionBox.Items.Clear();
            serialPortSelectionBox.Items.Add(COM.GetComPortList());
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                COM.connect(serialPortSelectionBox.SelectedText);
                receivedMessagesListBox.Items.Add("connected: " + serialPortSelectionBox.SelectedText);
            }
            catch (Exception) { receivedMessagesListBox.Items.Add("sorry niet gelukt"); }

        }
        private void DisplayReceivedMessage(String message)
        {
            receivedMessagesListBox.Items.Insert(0, message);
        }

        private void DisplaySendMessage(String message)
        {
            sendMessagesListBox.Items.Insert(0, message);
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
                    //ProcessMessages();
                }
                catch (Exception exception) // Not very nice to catch Exception...but for now it's good enough.
                {
                    Debug.WriteLine("Could not read from serial port: " + exception.Message);
                }
            }
        }

        //private void ProcessMessages()
        //{
        //    String message = messageBuilder.FindAndRemoveNextMessage();
        //    while (message != null)
        //    {
        //        DisplayReceivedMessage(message);
        //        MessageReceived(message);
        //        message = messageBuilder.FindAndRemoveNextMessage();
        //    }
        //}

        private void MessageReceived(String message)
        {
            if (message.Contains("#C_VALUE:"))
            {
                //string Inbox = message;
                //int StartValue = Inbox.IndexOf(":") + 1;
                //int EndValue = Inbox.IndexOf("%");
                //string Value = Inbox.Substring(StartValue, EndValue - StartValue);
                //int Value2 = Convert.ToInt32(Value);
                //sliderC.Value = Value2;
            }

            // TODO: Put code here to handle the received message.
            //       The received message is contained in the message parameter.
            //
            // Watch it! received messages can be with or without parameters.
            //   Like E.g. "#SHOW_BUTTON_1_CLICKED%" (no parameters)
            //        or   "#SET_SLIDER_B_VALUE:7%"  (has an number parameter at the end,
            //                                        that notes the new value for slider B)
            // Hints: 
            //  - You can use the String methods to parse the message and (if present) its parameters.
            //    See: http://msdn.microsoft.com/en-us/library/system.string.aspx
            //     for help on available String methods like
            //     - String.StartsWith(...)
            //     - String.Substring(...)
            //     - String.IndexOf(...)
            //     - etc
            //  - Convert.ToInt32(...) can be used to convert strings to an integer.
            //  - Sliders have a 'Value' property which can be used to get/set the position of the slider.


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
    }
}
