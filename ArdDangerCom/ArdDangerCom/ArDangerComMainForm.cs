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

namespace ArdDangerCom
{
    public partial class ArDangerComMainForm : Form
    {
        /// <summary>
        /// The speed of the serial connection (bytes per second).
        /// TODO: make sure that the speed in the Arduino program is set to the same value!
        /// Note: You can set this to a higher value like 57600 or 115200, 
        /// but data loss is possible then.
        /// </summary>
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

        public ArDangerComMainForm()
        {
            InitializeComponent();
            FillSerialPortSelectionBoxWithAvailablePorts();

            serialPort = new SerialPort();
            serialPort.BaudRate = connectionSpeed;
            // Choose: 9600, 19200 or 38400. Getting errors? Choose lower speed.
            // Also be sure that you set the speed on the arduino to the same value!

            messageBuilder = new MessageBuilder(messageBeginMarker, messageEndMarker);
            UpdateUserInterface();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage("#ZET_ONDERSTE_LED_AAN%");
            // TODO: Put code here that sends a message to the Arduino. 
            //       The message should tell the Arduino to turn 'on'
            //       the bottom LED of the danger shield.
            //       You can use the given SendMessage(...) function if you like.


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessage("#ZET_ONDERSTE_LED_UIT%");
            // TODO: Put code here that sends a message to the Arduino. 
            //       The message should tell the Arduino to turn 'off'
            //       the bottom LED of the danger shield.
            //       You can use the given SendMessage(...) function if you like.


        }

        private void sliderA_ValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToString( sliderA.Value));
            SendMessage("#SET_TOP_LED_INTENSITY:" + sliderA.Value + "%");
            // TODO: Put code here that sends a message to the Arduino.
            //       The message should tell the Arduino to set the intensity
            //       of the topmost LED of the Danger shield to a specified value (from the slider). 
            //       The value in the message is a number from 1 until 100. (unit: percentage!)


        }

        /// <summary>
        /// This method is called when a message is received.
        /// </summary>
        /// <param name="message">The received message</param>
        private void MessageReceived(String message)
        {
            if (message.Contains("#C_VALUE:"))
            {
                string Inbox = message;
                int StartValue = Inbox.IndexOf(":") + 1;
                int EndValue = Inbox.IndexOf("%");
                string Value = Inbox.Substring(StartValue, EndValue - StartValue);
                int Value2 = Convert.ToInt32(Value);
                sliderC.Value = Value2;
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

        /// <summary>
        /// Reads data from the serial port and get received messages from that data.
        /// Called on each tick of the messageReceiveTimer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Display a message in the 'received messages' UI box.
        /// </summary>
        /// <param name="message">The message to displayed</param>
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

        /// <summary>
        /// Close the serial connection when the form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArDangerComMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            readMessageTimer.Enabled = false;
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        /// <summary>
        /// Detect the serial ports when the refresh button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshSerialPortsButton_Click(object sender, EventArgs e)
        {
            FillSerialPortSelectionBoxWithAvailablePorts();
        }

        /// <summary>
        /// Set the selected serial port after leaving the selection box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPortSelectionBox_Leave(object sender, EventArgs e)
        {
            serialPortSelectionBox.Text = serialPortSelectionBox.Text.ToUpper();
        }

        /// <summary>
        /// Connect / disconnect the serial port when the connect button is pressed.
        /// And update the UI widgets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Clear message data from textboxes and from the messageUnderConstruction buffer.
        /// </summary>
        private void ClearAllMessageData()
        {
            sendMessagesListBox.Items.Clear();
            receivedMessagesListBox.Items.Clear();
            receivedRawDataTextBox.Clear();
            messageBuilder.Clear();
        }

        /// <summary>
        /// Enable / Disable UI widgets depending on the connected state of the serial port.
        /// </summary>
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

            button1.Enabled = isConnected;
            button2.Enabled = isConnected;
            button3.Enabled = isConnected;
            sliderA.Enabled = isConnected;
            sliderB.Enabled = isConnected;
            sliderC.Enabled = isConnected;
        }

        /// <summary>
        /// Put all detected serial ports in the serial port selection box.
        /// </summary>
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

        private void sliderA_Scroll(object sender, EventArgs e)
        {
           // SendMessage("#SET_TOP_LED_INTENSITY:" + sliderA.Value + "%");
        }

        private void sliderC_Scroll(object sender, EventArgs e)
        {

        }

        private void sliderC_Scroll_1(object sender, EventArgs e)
        {

        }
    }
}
