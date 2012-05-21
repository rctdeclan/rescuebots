﻿using System;
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
    public partial class Form1 : Form
    {
        /// <summary>
        /// The speed of the serial connection (bytes per second).
        /// TODO: make sure that the speed in the Arduino program is set to the same value!
        /// Note: You can set this to a higher value like 57600 or 115200, 
        /// but data loss is possible then.
        /// </summary>
        /// 

        private int Xcoordinate2DArray = 0;
        private int YCoordinate2DARRAY = 0;


        Bitmap ALL_OPEN                 = new Bitmap(Rescuebots.Properties.Resources.ALL_OPEN);
        Bitmap EAST_CLOSED              = new Bitmap(Rescuebots.Properties.Resources.EAST_CLOSED);
        Bitmap EAST_SOUTH_CLOSED        = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_CLOSED);
        Bitmap EAST_SOUTH_WEST_CLOSED   = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_WEST_CLOSED);
        Bitmap EAST_WEST_CLOSED         = new Bitmap(Rescuebots.Properties.Resources.EAST_WEST_CLOSED);
        Bitmap NORTH_CLOSED             = new Bitmap(Rescuebots.Properties.Resources.NORTH_CLOSED);
        Bitmap NORTH_EAST_CLOSED        = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_CLOSED);
        Bitmap NORTH_EAST_SOUTH_CLOSED  = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_SOUTH_CLOSED);
        Bitmap NORTH_SOUTH_CLOSED       = new Bitmap(Rescuebots.Properties.Resources.NORTH_SOUTH_CLOSED);
        Bitmap SOUTH_CLOSED             = new Bitmap(Rescuebots.Properties.Resources.SOUTH_CLOSED);
        Bitmap SOUTH_WEST_CLOSED        = new Bitmap(Rescuebots.Properties.Resources.SOUTH_WEST_CLOSED);
        Bitmap SOUTH_WEST_NORTH_CLOSED  = new Bitmap(Rescuebots.Properties.Resources.SOUTH_WEST_NORTH_CLOSED);
        Bitmap WEST_CLOSED              = new Bitmap(Rescuebots.Properties.Resources.WEST_CLOSED);
        Bitmap WEST_NORTH_CLOSED        = new Bitmap(Rescuebots.Properties.Resources.WEST_NORTH_CLOSED);
        Bitmap WEST_NORTH_EAST_CLOSED   = new Bitmap(Rescuebots.Properties.Resources.WEST_NORTH_EAST_CLOSED);

        int[,] multiDimensionalArray1 = new int[10, 10] 
        { 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        };
        
        void FillForm1WithMapValuesFrom2dArray()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int caseSwitch = multiDimensionalArray1 [i,j];
                    switch (caseSwitch)
                    {
                        case 0:
                            pictureBox1.Image = ALL_OPEN;
                            break;
                        case 1:
                            pictureBox1.Image = EAST_CLOSED;
                            break;
                        case 2:
                            pictureBox1.Image = EAST_SOUTH_CLOSED;
                            break;
                        case 3:
                            pictureBox1.Image = EAST_SOUTH_WEST_CLOSED;
                            break;
                        case 4:
                            pictureBox1.Image = EAST_WEST_CLOSED;
                            break;
                        case 5:
                            pictureBox1.Image = NORTH_CLOSED;
                            break;
                        case 6:
                            pictureBox1.Image = NORTH_EAST_CLOSED;
                            break;
                        case 7:
                            pictureBox1.Image = NORTH_EAST_SOUTH_CLOSED;
                            break;
                        case 8:
                            pictureBox1.Image = NORTH_SOUTH_CLOSED;
                            break;
                        case 9:
                            pictureBox1.Image = SOUTH_CLOSED;
                            break;
                        case 10:
                            pictureBox1.Image = SOUTH_WEST_CLOSED;
                            break;
                        case 11:
                            pictureBox1.Image = SOUTH_WEST_NORTH_CLOSED;
                            break;
                        case 12:
                            pictureBox1.Image = WEST_CLOSED;
                            break;
                        case 13:
                            pictureBox1.Image = WEST_NORTH_CLOSED;
                            break;
                        case 14:
                            pictureBox1.Image = WEST_NORTH_EAST_CLOSED;
                            break;
                        default:
                            pictureBox1.Image = ALL_OPEN;
                            
                            break;
                    }
                }
            }
        }

        
        
        private const int connectionSpeed = 38400;

        /// <summary>
        /// Marker to mark the start of a message.
        /// TODO: You can adapt this value to your own needs.
        /// </summary>
        private const String messageBeginMarker = "#";

        /// <summary>
        /// Marker to mark the end of a message.
        /// TODO: You can adapt this value to your own needs.
        /// </summary>
        private const String messageEndMarker = "%";

        /// <summary>
        /// Serial port used for the connection.
        /// </summary>
        private SerialPort serialPort;

        /// <summary>
        /// Buffer that is used to store data (text) that is received from the Arduino.
        /// Messages that a recevied can be retrieved from the messageBuilder.
        /// </summary>
        private MessageBuilder messageBuilder;
        public Form1()
        {
            InitializeComponent();
        }

        private void refreshSerialPortsButton_Click(object sender, EventArgs e)
        {
            FillSerialPortSelectionBoxWithAvailablePorts();
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
                        //ClearAllMessageData();
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

            //UpdateUserInterface();
        }
        private void serialPortSelectionBox_Leave(object sender, EventArgs e)
        {
            serialPortSelectionBox.Text = serialPortSelectionBox.Text.ToUpper();
        }


        private void DisplayReceivedMessage(String message)
        {
            receivedMessagesListBox.Items.Insert(0, message);
        }

        /// <summary>
        /// Display a message in the 'send messages' UI box.
        /// </summary>
        /// <param name="message">The message to displayed</param>
        private void DisplaySendMessage(String message)
        {
            sendMessagesListBox.Items.Insert(0, message);
        }

        /// <summary>
        /// Display received raw data in the 'Received raw data' UI box.
        /// </summary>
        /// <param name="data">The data to displayed</param>
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

        /// <summary>
        /// Find and process all buffered messages .
        /// </summary>
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
        /// <summary>
        /// This method is called when a message is received.
        /// </summary>
        /// <param name="message">The received message</param>
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

        //====================================================================================
        // Please read the code below this comment block! 
        // There are methods in it you can use, like sendMessage(..). 
        // 
        // Also read the code in the 'MessageBuilder' class.
        // (see MessageBuilder.cs file in the project)
        // 
        // There is no need to change code below this comment block.
        // Please do not add code below this comment block.
        //====================================================================================

        /// <summary>
        /// Sends the given message to the serial port.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>true if the message was send, false otherwise.</returns>
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

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Load("C:\Users\Rowin\Desktop\C# programming\Rescuebots\Rescuebots\Resources\EAST_CLOSED.png");
            pictureBox1.Image = ALL_OPEN;
           
        }

        private void FillMapBtn_Click(object sender, EventArgs e)
        {
            FillForm1WithMapValuesFrom2dArray();
        }

        

       

       

       

      

        

        

       


      
    }
}
