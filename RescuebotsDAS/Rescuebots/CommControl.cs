using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;

namespace Rescuebots
{
    class CommControl
    {
        private SerialPort serialPort;
        private List<string> comport;

        public CommControl()
        {
            comport = new List<string>();

        }

        public void scann()
        {
            String[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);

            foreach (String port in ports)
            {
                if (comport.Contains(port)){ }
                else
                {
                    comport.Add(port);
                }
            }
        }


        public string connect(string port)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            else
            {
                String Port = port;

                    serialPort.PortName = Port;
                    serialPort.Open();
                    if (serialPort.IsOpen)
                    {
                        //ClearAllMessageData();
                        serialPort.DiscardInBuffer();
                        serialPort.DiscardOutBuffer();
                        return "connected";
                    }
            }
            return "disconnected";
        }

        public string GetComPortList()
        {
            foreach(string a in comport)
            {
                return a;
            }
            return "sorry er is niks gevonden";
        }
    }
}