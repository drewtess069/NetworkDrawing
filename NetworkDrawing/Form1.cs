using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace NetworkDrawing
{
    public partial class Form1 : Form
    {
        byte[] dataX = new byte[1024];
        byte[] dataY = new byte[1024];
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket client;


        Point mousePoint;

        int x;
        int y;
        int brushSize = 10;
        Pen redPen = new Pen(Color.Red, 1);
        SolidBrush drawBrush = new SolidBrush(Color.Red);

        List<int> xList = new List<int>();
        List<int> yList = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);
            x = e.X;
            y = e.Y;

            ServerFunc(x, y);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(redPen, mousePoint.X, mousePoint.Y, 1, 1);
            //e.Graphics.FillRectangle(drawBrush, x, y, brushSize, brushSize);

            //if (xList.Count > 0)
            //{
            //    for (int i = 0; i < xList.Count; i++)
            //    {
            //        e.Graphics.DrawRectangle(redPen, x, y, 1, 1);
            //    }
            //}
        }

        private void ServerFunc(int x, int y)
        {
            if(client == null)
            {
                testLabel.Text = "null";
            }
                else
            {
                testLabel.Text = "Client Found";
            }
            Refresh();
            Thread.Sleep(1000);

            Graphics g = this.CreateGraphics();

            int recX;
            int recY;

            dataX = Encoding.UTF8.GetBytes(x.ToString());
            dataY = Encoding.UTF8.GetBytes(y.ToString());

            client.Send(dataX, dataX.Length, SocketFlags.None);
            client.Send(dataY, dataY.Length, SocketFlags.None);

            dataX = new byte[1024];
            dataY = new byte[1024];

            recX = client.Receive(dataX);
            recY = client.Receive(dataY);

            xList.Add(recX);
            yList.Add(recY);
            //g.DrawRectangle(redPen, recX, recY, 1, 1);
        }

        private void ClientFunc()
        {
            Graphics g = this.CreateGraphics();

            //byte[] recX = new byte[1024];
            //byte[] recY = new byte[1024];

            var recX = server.Receive(dataX);

           // int index = 

            //var recY = server.Receive(dataY);

            string stringPoint = $"{Encoding.UTF8.GetString(dataX, 0, recX)}";

            int index = stringPoint.IndexOf(",");
            string x = stringPoint.Substring(0, index);
            string y  = stringPoint.Substring(index + 1);
             mousePoint = new Point(Convert.ToInt16(x), Convert.ToInt16(y));
            //foreach(Byte b in dataX)
            //{
            //    char c = (char)b;
            //    x += c;
            //}

            testLabel.Text = $"{Encoding.UTF8.GetString(dataX, 0, recX)}";
            Refresh();
            Thread.Sleep(1000);
            //xList.Add(recX);
            //yList.Add(recY);

            for (int i = 0; i < xList.Count; i++)
            {
                testLabel.Text += $"x = {xList[i]}, y = {yList[i]}";
            }

            // g.DrawRectangle(redPen, recX, recY, 1, 1);

            //stringData = Encoding.UTF8.GetString(data, 0, recv);

            dataX = Encoding.UTF8.GetBytes(x.ToString());
            dataY = Encoding.UTF8.GetBytes(y.ToString());

            server.Send(Encoding.UTF8.GetBytes(dataX.ToString()));
            server.Send(Encoding.UTF8.GetBytes(dataY.ToString()));
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(serverCheck.Checked)
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("10.63.42.206"), 9050);

                Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                newsock.Bind(ipep);

                newsock.Listen(10);

                Refresh();

                client = newsock.Accept();

                if (client == null)
                {
                    testLabel.Text = "null";
                }
                else
                {
                    testLabel.Text = "Client Found";
                }
                Refresh();
                Thread.Sleep(1000); 


                //IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
            }
            else if(clientCheck.Checked)
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("10.63.42.206"), 9050);
                try
                {
                    server.Connect(ipep);

                }
                catch (SocketException)
                {
                    connectButton.Text = "Error";
                    Refresh();
                    Thread.Sleep(1000);
                    connectButton.Text = "Try Again";
                }
            }
        }

        private void receiveButton_Click(object sender, EventArgs e)
        {
            ClientFunc();
        }
    }
}

