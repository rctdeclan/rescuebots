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

            byte[] buffer = new byte[1];
            buffer[0] = 0x24;
            serialPort.Write(buffer, 0, 1);//Send ready signal

            while (serialPort.BytesToRead==0) { }
            int i = 0;
            while (serialPort.BytesToRead >= 7)
            {
                cell c = new cell();
                c.north = (serialPort.ReadByte() == 0x1);
                c.east = (serialPort.ReadByte() == 0x1);
                c.south = (serialPort.ReadByte() == 0x1);
                c.west = (serialPort.ReadByte() == 0x1);
                c.x = serialPort.ReadByte();
                c.y = serialPort.ReadByte();
                c.a = (Rescuebots.Action) serialPort.ReadByte();
                cells.Add(c);
                i++;
            }

            //send values to field.
            field.FillBorders(cells);
            field.FillRoute();

            if (serialPort.BytesToRead == 1)
            {
                serialPort.DiscardInBuffer();
                field.softEndRoute = false;
                
            }
            else
            {
                byte[] buffer1 = new byte[1];
                buffer1[0] = 0x24;
                serialPort.Write(buffer1, 0, 1);//Send ready signal

                while (serialPort.BytesToRead == 0) { }

                int j = 0;
                while (serialPort.BytesToRead >= 7)
                {
                    cell c = new cell();
                    c.north = (serialPort.ReadByte() == 0x1);
                    c.east = (serialPort.ReadByte() == 0x1);
                    c.south = (serialPort.ReadByte() == 0x1);
                    c.west = (serialPort.ReadByte() == 0x1);
                    c.x = serialPort.ReadByte();
                    c.y = serialPort.ReadByte();
                    c.a = (Rescuebots.Action)serialPort.ReadByte();
                    invCells.Add(c);
                    j++;
                }

                //send values to field
                field.FillInvRoute();

                if (serialPort.BytesToRead == 1)
                {
                    serialPort.DiscardInBuffer();
                    field.softEndInvRoute = false;
                }
            }


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
    }

    public enum Action
    {
        tLeft,
        tRight,
        mForward,
        t180s
    }

}
