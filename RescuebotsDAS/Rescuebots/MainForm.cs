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

        private const int connectionSpeed = 38400;
        private SerialPort serialPort;

        List<cell> cells = new List<cell>();
        List<cell> invCells = new List<cell>();

        public MainForm()
        {
            InitializeComponent();
            MakeConnectionWithRP6Robot();

        }

        private void MakeConnectionWithRP6Robot()
        {
            FillSerialPortSelectionBoxWithAvailablePorts();

            serialPort = new SerialPort();
            serialPort.BaudRate = connectionSpeed;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;

            orientBox.SelectedIndex = 0;
            
        }

        private void refreshSerialPortsButton_Click(object sender, EventArgs e)
        {
            FillSerialPortSelectionBoxWithAvailablePorts();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            MakeConnectionWithRP6Robot();
            if (serialPort.IsOpen)
            {
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
                        serialPort.DiscardInBuffer();
                        serialPort.DiscardOutBuffer();
                    }
                    sendBtn.Enabled = true;
                    receiveBtn.Enabled = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Could not connect to the given serial port: " + exception.Message);
                }
            }

        }


        private void receiveBtn_Click(object sender, EventArgs e)
        {
            field.ResetField();

            serialPort.DiscardInBuffer();
            byte[] buffer = new byte[1];
            buffer[0] = 0x24;
            serialPort.Write(buffer, 0, 1);//Send ready signal

            while (serialPort.BytesToRead==0) { }
            int i = 0;
            System.Threading.Thread.Sleep(200);

            int totalRouteCells = Convert.ToInt32(serialPort.ReadLine());

            while (i<totalRouteCells) //2 chars and one newline * 8 = 24 chars.
            {
                try
                {
                    System.Threading.Thread.Sleep(100);
                    string no = serialPort.ReadLine();
                    string ea = serialPort.ReadLine();
                    string so = serialPort.ReadLine();
                    string we = serialPort.ReadLine();
                    string xx = serialPort.ReadLine();
                    string yy = serialPort.ReadLine();
                    string aa = serialPort.ReadLine();
                    string dd = serialPort.ReadLine();
                        
                    cell c = new cell();
                    c.north = (no == "NY");
                    c.east = (ea == "EY");
                    c.south = (so == "SY");
                    c.west = (we == "WY");
                    try
                    {
                        c.x = Convert.ToInt16(xx);
                        c.y = Convert.ToInt16(yy);
                    }
                    catch (FormatException) { MessageBox.Show("Invalid coordinates."); return; }
                    try
                    {
                        c.a = (Rescuebots.Action)Convert.ToInt16(aa);
                    }
                    catch (FormatException) { MessageBox.Show("Unknown action"); return; }
                    try
                    {
                        c.dir = (Orientation)Convert.ToInt16(dd);
                    }
                    catch (FormatException) { MessageBox.Show("Unknown direction"); return; }
                    cells.Add(c);
                    i++;
                }
                catch (TimeoutException) { MessageBox.Show("timeout"); }
            }

            
            if (serialPort.BytesToRead>0)
            {
                string f = serialPort.ReadLine();
                if (f=="F")
                {
                    serialPort.DiscardInBuffer();
                    field.softEndRoute = false;
                }
            }
            else
            {
                int j = 0;
                byte[] buffer1 = new byte[1];
                buffer1[0] = 0x24;
                serialPort.Write(buffer1, 0, 1);//Send ready signal

                while (serialPort.BytesToRead == 0) { }

                int totalInvRouteCells = Convert.ToInt32(serialPort.ReadLine());
                while (i<totalInvRouteCells)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        string no = serialPort.ReadLine();
                        string ea = serialPort.ReadLine();
                        string so = serialPort.ReadLine();
                        string we = serialPort.ReadLine();
                        string xx = serialPort.ReadLine();
                        string yy = serialPort.ReadLine();
                        string aa = serialPort.ReadLine();
                        string dd = serialPort.ReadLine();

                        cell c = new cell();
                        c.north = (no == "NY");
                        c.east = (ea == "EY");
                        c.south = (so == "SY");
                        c.west = (we == "WY");
                        try
                        {
                            c.x = Convert.ToInt16(xx);
                            c.y = Convert.ToInt16(yy);
                        }
                        catch (FormatException) { MessageBox.Show("Invalid coordinates."); return; }
                        try
                        {
                            c.a = (Rescuebots.Action)Convert.ToInt16(aa);
                        }
                        catch (FormatException) { MessageBox.Show("Unknown action"); return; }
                        try
                        {
                            c.dir = (Orientation)Convert.ToInt16(dd);
                        }
                        catch (FormatException) { MessageBox.Show("Unknown direction"); return; } 
                        invCells.Add(c);
                        j++;
                    }
                    catch (TimeoutException) { MessageBox.Show("timeout"); }
                }


                //if (serialPort.BytesToRead > 0)
                //{
                //    string f = serialPort.ReadLine();
                //    if (f == "F")
                //    {
                //        serialPort.DiscardInBuffer();
                //        field.softEndInvRoute = false;
                //    }
                //}
            }

            //send values to field.
            field.FillBorders(cells);
            field.FillRoute(cells);
            field.FillInvRoute(invCells);


        }
        

        private void sendBtn_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[3];
            buffer[0] = (byte) Convert.ToByte(xNud.Value);
            buffer[1] = (byte) Convert.ToByte(yNud.Value);
            buffer[2] = (byte) Convert.ToByte(orientBox.SelectedIndex);
            serialPort.Write(buffer,0,3);
            
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

    public enum Orientation
    {
        facingNorth,
        facingEast,
        facingSouth,
        facingWest
    }

    public struct cell
    {
        public Boolean north;
        public Boolean east;
        public Boolean south;
        public Boolean west;
        public int x;
        public int y;
        public Rescuebots.Action a;
        public Orientation dir;
    }

    public enum Action
    {
        tLeft,
        tRight,
        mForward,
        t180s
    }

}
