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
    public partial class Form1 : Form
    {
        private CommControl COM;
        private const int connectionSpeed = 38400;
        private const String messageBeginMarker = "#";
        private const String messageEndMarker = "%";
        private SerialPort serialPort;
        private MessageBuilder messageBuilder;
        private int Xcoordinate2DArray = 0;
        private int YCoordinate2DARRAY = 0;


        Bitmap ALL_OPEN                     = new Bitmap(Rescuebots.Properties.Resources.ALL_OPEN1);
        Bitmap EAST_CLOSED                  = new Bitmap(Rescuebots.Properties.Resources.EAST_CLOSED1);
        Bitmap EAST_SOUTH_CLOSED            = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_CLOSED1);
        Bitmap EAST_SOUTH_WEST_CLOSED       = new Bitmap(Rescuebots.Properties.Resources.EAST_SOUTH_WEST_CLOSED1);
        Bitmap EAST_WEST_CLOSED             = new Bitmap(Rescuebots.Properties.Resources.EAST_WEST_CLOSED1);
        Bitmap NORTH_CLOSED                 = new Bitmap(Rescuebots.Properties.Resources.NORTH_CLOSED1);
        Bitmap NORTH_EAST_CLOSED            = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_CLOSED1);
        Bitmap NORTH_EAST_SOUTH_CLOSED      = new Bitmap(Rescuebots.Properties.Resources.NORTH_EAST_SOUTH_CLOSED1);
        Bitmap NORTH_SOUTH_CLOSED           = new Bitmap(Rescuebots.Properties.Resources.NORTH_SOUTH_CLOSED1);
        Bitmap SOUTH_CLOSED                 = new Bitmap(Rescuebots.Properties.Resources.SOUTH_CLOSED1);
        Bitmap SOUTH_WEST_CLOSED            = new Bitmap(Rescuebots.Properties.Resources.SOUTH_WEST_CLOSED1);
        Bitmap SOUTH_WEST_NORTH_CLOSED      = new Bitmap(Rescuebots.Properties.Resources.SOUTH_WEST_NORTH_CLOSED1);
        Bitmap WEST_CLOSED                  = new Bitmap(Rescuebots.Properties.Resources.WEST_CLOSED1);
        Bitmap WEST_NORTH_CLOSED            = new Bitmap(Rescuebots.Properties.Resources.WEST_NORTH_CLOSED1);
        Bitmap WEST_NORTH_EAST_CLOSED       = new Bitmap(Rescuebots.Properties.Resources.WEST_NORTH_EAST_CLOSED1);

        Bitmap NO_STARTPOSITION_OR_VICTIM   = new Bitmap(Rescuebots.Properties.Resources.NO_STARTPOSITION_OR_VICTIM);
        Bitmap STARTPOSITION                = new Bitmap(Rescuebots.Properties.Resources.STARTPOSITION);
        Bitmap VICTIM                       = new Bitmap(Rescuebots.Properties.Resources.VICTIM);

        Bitmap EAST_TO_SOUTH_IN             = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);
        Bitmap EAST_TO_WEST_IN              = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);
        Bitmap NORTH_TO_EAST_IN             = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);
        Bitmap NORTH_TO_SOUTH_IN            = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);
        Bitmap NOT_ENTERED_ZONE_IN          = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);
        Bitmap SOUTH_TO_WEST_IN             = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);
        Bitmap WEST_TO_NORTH_IN             = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_IN);


        Bitmap EAST_TO_SOUTH_OUT            = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap EAST_TO_WEST_OUT             = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap NORTH_TO_EAST_OUT            = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap NORTH_TO_SOUTH_OUT           = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap NOT_ENTERED_ZONE_OUT         = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap SOUTH_TO_WEST_OUT            = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);
        Bitmap WEST_TO_NORTH_OUT            = new Bitmap(Rescuebots.Properties.Resources.EAST_TO_SOUTH_OUT);



        PictureBox[,] pictureBoxArray1 = new PictureBox[10, 10];
        PictureBox[,] pictureBoxArray_WAY_IN = new PictureBox[10, 10];

        int[,] multiDimensionalArray1 = new int[10, 10] 
        { 
        { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 8, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, 
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
        };

        int[,] Mda2 = new int[10, 10] 
        { 
       { 0, 0, 3, 4, 5, 0, 0, 0, 0, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
       { 1, 2, 3, 4, 5, 6, 5, 4, 3, 0 },
        };

        void FillForm1WithMapValuesFrom2dArray_MAIN()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int caseSwitch = multiDimensionalArray1[i, j];
                    PictureBox pb1 = pictureBoxArray1[i, j];
                    switch (caseSwitch)
                    {
                        case 0:
                            pb1.Image = ALL_OPEN;
                            break;
                        case 1:
                            pb1.Image = EAST_CLOSED;
                            break;
                        case 2:
                            pb1.Image = EAST_SOUTH_CLOSED;
                            break;
                        case 3:
                            pb1.Image = EAST_SOUTH_WEST_CLOSED;
                            break;
                        case 4:
                            pb1.Image = EAST_WEST_CLOSED;
                            break;
                        case 5:
                            pb1.Image = NORTH_CLOSED;
                            break;
                        case 6:
                            pb1.Image = NORTH_EAST_CLOSED;
                            break;
                        case 7:
                            pb1.Image = NORTH_EAST_SOUTH_CLOSED;
                            break;
                        case 8:
                            pb1.Image = NORTH_SOUTH_CLOSED;
                            break;
                        case 9:
                            pb1.Image = SOUTH_CLOSED;
                            break;
                        case 10:
                            pb1.Image = SOUTH_WEST_CLOSED;
                            break;
                        case 11:
                            pb1.Image = SOUTH_WEST_NORTH_CLOSED;
                            break;
                        case 12:
                            pb1.Image = WEST_CLOSED;
                            break;
                        case 13:
                            pb1.Image = WEST_NORTH_CLOSED;
                            break;
                        case 14:
                            pb1.Image = WEST_NORTH_EAST_CLOSED;
                            break;
                        default:
                            pb1.Image = ALL_OPEN;

                            break;
                    }
                }
            }
        }

        
        void FillForm1WithMapValuesFrom2dArray_WAY_IN()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int caseSwitch2 = Mda2[i, j];
                    PictureBox pb2 = pictureBoxArray_WAY_IN[i, j];
                    switch (caseSwitch2)
                    {
                        case 0:
                            pb2.Image = NOT_ENTERED_ZONE_IN;
                            break;
                        case 1:
                            pb2.Image = EAST_TO_SOUTH_IN;
                            break;
                        case 2:
                            pb2.Image = EAST_TO_WEST_IN;
                            break;
                        case 3:
                            pb2.Image = NORTH_TO_EAST_IN;
                            break;
                        case 4:
                            pb2.Image = NORTH_TO_SOUTH_IN;
                            break;
                        case 5:
                            pb2.Image = SOUTH_TO_WEST_IN;
                            break;
                        case 6:
                            pb2.Image = WEST_TO_NORTH_IN;
                            break;
                        default:
                            pb2.Image = NOT_ENTERED_ZONE_IN;

                            break;
                    }
                }
            }
        }

        
        

        public Form1()
        {
            InitializeComponent();
            COM = new CommControl();
            COM.scann();
            serialPortSelectionBoxUpdate();


            pictureBoxArray1[0, 0] = pictureBox1;
            pictureBoxArray1[0, 1] = pictureBox2;
            pictureBoxArray1[0, 2] = pictureBox3;
            pictureBoxArray1[0, 3] = pictureBox4;
            pictureBoxArray1[0, 4] = pictureBox5;
            pictureBoxArray1[0, 5] = pictureBox6;
            pictureBoxArray1[0, 6] = pictureBox7;
            pictureBoxArray1[0, 7] = pictureBox8;
            pictureBoxArray1[0, 8] = pictureBox9;
            pictureBoxArray1[0, 9] = pictureBox10;

            pictureBoxArray1[1, 0] = pictureBox11;
            pictureBoxArray1[1, 1] = pictureBox12;
            pictureBoxArray1[1, 2] = pictureBox13;
            pictureBoxArray1[1, 3] = pictureBox14;
            pictureBoxArray1[1, 4] = pictureBox15;
            pictureBoxArray1[1, 5] = pictureBox16;
            pictureBoxArray1[1, 6] = pictureBox17;
            pictureBoxArray1[1, 7] = pictureBox18;
            pictureBoxArray1[1, 8] = pictureBox19;
            pictureBoxArray1[1, 9] = pictureBox20;

            pictureBoxArray1[2, 0] = pictureBox21;
            pictureBoxArray1[2, 1] = pictureBox22;
            pictureBoxArray1[2, 2] = pictureBox23;
            pictureBoxArray1[2, 3] = pictureBox24;
            pictureBoxArray1[2, 4] = pictureBox25;
            pictureBoxArray1[2, 5] = pictureBox26;
            pictureBoxArray1[2, 6] = pictureBox27;
            pictureBoxArray1[2, 7] = pictureBox28;
            pictureBoxArray1[2, 8] = pictureBox29;
            pictureBoxArray1[2, 9] = pictureBox30;

            pictureBoxArray1[3, 0] = pictureBox31;
            pictureBoxArray1[3, 1] = pictureBox32;
            pictureBoxArray1[3, 2] = pictureBox33;
            pictureBoxArray1[3, 3] = pictureBox34;
            pictureBoxArray1[3, 4] = pictureBox35;
            pictureBoxArray1[3, 5] = pictureBox36;
            pictureBoxArray1[3, 6] = pictureBox37;
            pictureBoxArray1[3, 7] = pictureBox38;
            pictureBoxArray1[3, 8] = pictureBox39;
            pictureBoxArray1[3, 9] = pictureBox40;

            pictureBoxArray1[4, 0] = pictureBox41;
            pictureBoxArray1[4, 1] = pictureBox42;
            pictureBoxArray1[4, 2] = pictureBox43;
            pictureBoxArray1[4, 3] = pictureBox44;
            pictureBoxArray1[4, 4] = pictureBox45;
            pictureBoxArray1[4, 5] = pictureBox46;
            pictureBoxArray1[4, 6] = pictureBox47;
            pictureBoxArray1[4, 7] = pictureBox48;
            pictureBoxArray1[4, 8] = pictureBox49;
            pictureBoxArray1[4, 9] = pictureBox50;

            pictureBoxArray1[5, 0] = pictureBox51;
            pictureBoxArray1[5, 1] = pictureBox52;
            pictureBoxArray1[5, 2] = pictureBox53;
            pictureBoxArray1[5, 3] = pictureBox54;
            pictureBoxArray1[5, 4] = pictureBox55;
            pictureBoxArray1[5, 5] = pictureBox56;
            pictureBoxArray1[5, 6] = pictureBox57;
            pictureBoxArray1[5, 7] = pictureBox58;
            pictureBoxArray1[5, 8] = pictureBox59;
            pictureBoxArray1[5, 9] = pictureBox60;

            pictureBoxArray1[6, 0] = pictureBox61;
            pictureBoxArray1[6, 1] = pictureBox62;
            pictureBoxArray1[6, 2] = pictureBox63;
            pictureBoxArray1[6, 3] = pictureBox64;
            pictureBoxArray1[6, 4] = pictureBox65;
            pictureBoxArray1[6, 5] = pictureBox66;
            pictureBoxArray1[6, 6] = pictureBox67;
            pictureBoxArray1[6, 7] = pictureBox68;
            pictureBoxArray1[6, 8] = pictureBox69;
            pictureBoxArray1[6, 9] = pictureBox70;

            pictureBoxArray1[7, 0] = pictureBox71;
            pictureBoxArray1[7, 1] = pictureBox72;
            pictureBoxArray1[7, 2] = pictureBox73;
            pictureBoxArray1[7, 3] = pictureBox74;
            pictureBoxArray1[7, 4] = pictureBox75;
            pictureBoxArray1[7, 5] = pictureBox76;
            pictureBoxArray1[7, 6] = pictureBox77;
            pictureBoxArray1[7, 7] = pictureBox78;
            pictureBoxArray1[7, 8] = pictureBox79;
            pictureBoxArray1[7, 9] = pictureBox80;

            pictureBoxArray1[8, 0] = pictureBox81;
            pictureBoxArray1[8, 1] = pictureBox82;
            pictureBoxArray1[8, 2] = pictureBox83;
            pictureBoxArray1[8, 3] = pictureBox84;
            pictureBoxArray1[8, 4] = pictureBox85;
            pictureBoxArray1[8, 5] = pictureBox86;
            pictureBoxArray1[8, 6] = pictureBox87;
            pictureBoxArray1[8, 7] = pictureBox88;
            pictureBoxArray1[8, 8] = pictureBox89;
            pictureBoxArray1[8, 9] = pictureBox90;

            pictureBoxArray1[9, 0] = pictureBox91;
            pictureBoxArray1[9, 1] = pictureBox92;
            pictureBoxArray1[9, 2] = pictureBox93;
            pictureBoxArray1[9, 3] = pictureBox94;
            pictureBoxArray1[9, 4] = pictureBox95;
            pictureBoxArray1[9, 5] = pictureBox96;
            pictureBoxArray1[9, 6] = pictureBox97;
            pictureBoxArray1[9, 7] = pictureBox98;
            pictureBoxArray1[9, 8] = pictureBox99;
            pictureBoxArray1[9, 9] = pictureBox100;

            //laag 2

            pictureBoxArray_WAY_IN[0, 0] = pictureBox101;
            pictureBoxArray_WAY_IN[0, 1] = pictureBox102;
            pictureBoxArray_WAY_IN[0, 2] = pictureBox103;
            pictureBoxArray_WAY_IN[0, 3] = pictureBox104;
            pictureBoxArray_WAY_IN[0, 4] = pictureBox105;
            pictureBoxArray_WAY_IN[0, 5] = pictureBox106;
            pictureBoxArray_WAY_IN[0, 6] = pictureBox107;
            pictureBoxArray_WAY_IN[0, 7] = pictureBox108;
            pictureBoxArray_WAY_IN[0, 8] = pictureBox109;
            pictureBoxArray_WAY_IN[0, 9] = pictureBox110;

            pictureBoxArray_WAY_IN[1, 0] = pictureBox111;
            pictureBoxArray_WAY_IN[1, 1] = pictureBox112;
            pictureBoxArray_WAY_IN[1, 2] = pictureBox113;
            pictureBoxArray_WAY_IN[1, 3] = pictureBox114;
            pictureBoxArray_WAY_IN[1, 4] = pictureBox115;
            pictureBoxArray_WAY_IN[1, 5] = pictureBox116;
            pictureBoxArray_WAY_IN[1, 6] = pictureBox117;
            pictureBoxArray_WAY_IN[1, 7] = pictureBox118;
            pictureBoxArray_WAY_IN[1, 8] = pictureBox119;
            pictureBoxArray_WAY_IN[1, 9] = pictureBox120;

            pictureBoxArray_WAY_IN[2, 0] = pictureBox121;
            pictureBoxArray_WAY_IN[2, 1] = pictureBox122;
            pictureBoxArray_WAY_IN[2, 2] = pictureBox123;
            pictureBoxArray_WAY_IN[2, 3] = pictureBox124;
            pictureBoxArray_WAY_IN[2, 4] = pictureBox125;
            pictureBoxArray_WAY_IN[2, 5] = pictureBox126;
            pictureBoxArray_WAY_IN[2, 6] = pictureBox127;
            pictureBoxArray_WAY_IN[2, 7] = pictureBox128;
            pictureBoxArray_WAY_IN[2, 8] = pictureBox129;
            pictureBoxArray_WAY_IN[2, 9] = pictureBox130;

            pictureBoxArray_WAY_IN[3, 0] = pictureBox131;
            pictureBoxArray_WAY_IN[3, 1] = pictureBox132;
            pictureBoxArray_WAY_IN[3, 2] = pictureBox133;
            pictureBoxArray_WAY_IN[3, 3] = pictureBox134;
            pictureBoxArray_WAY_IN[3, 4] = pictureBox135;
            pictureBoxArray_WAY_IN[3, 5] = pictureBox136;
            pictureBoxArray_WAY_IN[3, 6] = pictureBox137;
            pictureBoxArray_WAY_IN[3, 7] = pictureBox138;
            pictureBoxArray_WAY_IN[3, 8] = pictureBox139;
            pictureBoxArray_WAY_IN[3, 9] = pictureBox140;

            pictureBoxArray_WAY_IN[4, 0] = pictureBox141;
            pictureBoxArray_WAY_IN[4, 1] = pictureBox142;
            pictureBoxArray_WAY_IN[4, 2] = pictureBox143;
            pictureBoxArray_WAY_IN[4, 3] = pictureBox144;
            pictureBoxArray_WAY_IN[4, 4] = pictureBox145;
            pictureBoxArray_WAY_IN[4, 5] = pictureBox146;
            pictureBoxArray_WAY_IN[4, 6] = pictureBox147;
            pictureBoxArray_WAY_IN[4, 7] = pictureBox148;
            pictureBoxArray_WAY_IN[4, 8] = pictureBox149;
            pictureBoxArray_WAY_IN[4, 9] = pictureBox150;

            pictureBoxArray_WAY_IN[5, 0] = pictureBox151;
            pictureBoxArray_WAY_IN[5, 1] = pictureBox152;
            pictureBoxArray_WAY_IN[5, 2] = pictureBox153;
            pictureBoxArray_WAY_IN[5, 3] = pictureBox154;
            pictureBoxArray_WAY_IN[5, 4] = pictureBox155;
            pictureBoxArray_WAY_IN[5, 5] = pictureBox156;
            pictureBoxArray_WAY_IN[5, 6] = pictureBox157;
            pictureBoxArray_WAY_IN[5, 7] = pictureBox158;
            pictureBoxArray_WAY_IN[5, 8] = pictureBox159;
            pictureBoxArray_WAY_IN[5, 9] = pictureBox160;

            pictureBoxArray_WAY_IN[6, 0] = pictureBox161;
            pictureBoxArray_WAY_IN[6, 1] = pictureBox162;
            pictureBoxArray_WAY_IN[6, 2] = pictureBox163;
            pictureBoxArray_WAY_IN[6, 3] = pictureBox164;
            pictureBoxArray_WAY_IN[6, 4] = pictureBox165;
            pictureBoxArray_WAY_IN[6, 5] = pictureBox166;
            pictureBoxArray_WAY_IN[6, 6] = pictureBox167;
            pictureBoxArray_WAY_IN[6, 7] = pictureBox168;
            pictureBoxArray_WAY_IN[6, 8] = pictureBox169;
            pictureBoxArray_WAY_IN[6, 9] = pictureBox170;

            pictureBoxArray_WAY_IN[7, 0] = pictureBox171;
            pictureBoxArray_WAY_IN[7, 1] = pictureBox172;
            pictureBoxArray_WAY_IN[7, 2] = pictureBox173;
            pictureBoxArray_WAY_IN[7, 3] = pictureBox174;
            pictureBoxArray_WAY_IN[7, 4] = pictureBox175;
            pictureBoxArray_WAY_IN[7, 5] = pictureBox176;
            pictureBoxArray_WAY_IN[7, 6] = pictureBox177;
            pictureBoxArray_WAY_IN[7, 7] = pictureBox178;
            pictureBoxArray_WAY_IN[7, 8] = pictureBox179;
            pictureBoxArray_WAY_IN[7, 9] = pictureBox180;

            pictureBoxArray_WAY_IN[8, 0] = pictureBox181;
            pictureBoxArray_WAY_IN[8, 1] = pictureBox182;
            pictureBoxArray_WAY_IN[8, 2] = pictureBox183;
            pictureBoxArray_WAY_IN[8, 3] = pictureBox184;
            pictureBoxArray_WAY_IN[8, 4] = pictureBox185;
            pictureBoxArray_WAY_IN[8, 5] = pictureBox186;
            pictureBoxArray_WAY_IN[8, 6] = pictureBox187;
            pictureBoxArray_WAY_IN[8, 7] = pictureBox188;
            pictureBoxArray_WAY_IN[8, 8] = pictureBox189;
            pictureBoxArray_WAY_IN[8, 9] = pictureBox190;

            pictureBoxArray_WAY_IN[9, 0] = pictureBox191;
            pictureBoxArray_WAY_IN[9, 1] = pictureBox192;
            pictureBoxArray_WAY_IN[9, 2] = pictureBox193;
            pictureBoxArray_WAY_IN[9, 3] = pictureBox194;
            pictureBoxArray_WAY_IN[9, 4] = pictureBox195;
            pictureBoxArray_WAY_IN[9, 5] = pictureBox196;
            pictureBoxArray_WAY_IN[9, 6] = pictureBox197;
            pictureBoxArray_WAY_IN[9, 7] = pictureBox198;
            pictureBoxArray_WAY_IN[9, 8] = pictureBox199;
            pictureBoxArray_WAY_IN[9, 9] = pictureBox200;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Load("C:\Users\Rowin\Desktop\C# programming\Rescuebots\Rescuebots\Resources\EAST_CLOSED.png");
            pictureBox1.Image = ALL_OPEN;
           
        }

        private void FillMapBtn_Click(object sender, EventArgs e)
        {
            FillForm1WithMapValuesFrom2dArray_MAIN();
            FillForm1WithMapValuesFrom2dArray_WAY_IN();
        }

       

       

       

      

        

        

       


      
    }
}
