﻿using System;
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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetworkDrawing
{
    public partial class Form1 : Form
    {
        byte[] dataX = new byte[1024];
        byte[] dataY = new byte[1024];
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        int x;
        int y;
        int brushSize = 10;
        Pen redPen = new Pen(Color.Red, 20);
        SolidBrush drawBrush = new SolidBrush(Color.Red);

        List<int> xList = new List<int>();
        List<int> yList = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            if (serverCheck.Checked)
            {
                ServerFunc(x, y);

            }
            else if (clientCheck.Checked)
            {
                ClientFunc(x, y);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.FillRectangle(drawBrush, x, y, brushSize, brushSize);

            if (xList.Count > 0)
            {
                for (int i = 0; i < xList.Count; i++)
                {
                    e.Graphics.DrawRectangle(redPen, x, y, Width, Height);
                }
            }
        }

        private void ServerFunc(int x, int y)
        {
            Graphics g = this.CreateGraphics();

            int recX;
            int recY;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);

            newsock.Listen(10);

            Refresh();

            Socket client = newsock.Accept();

            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;

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

        private void ClientFunc(int x, int y)
        {
            Graphics g = this.CreateGraphics();

            int recX = server.Receive(dataX);
            int recY = server.Receive(dataY);

            xList.Add(recX);
            yList.Add(recY);

            Refresh();

           // g.DrawRectangle(redPen, recX, recY, 1, 1);

            //stringData = Encoding.UTF8.GetString(data, 0, recv);

            dataX = Encoding.UTF8.GetBytes(x.ToString());
            dataY = Encoding.UTF8.GetBytes(y.ToString());

            server.Send(Encoding.UTF8.GetBytes(dataX.ToString()));
            server.Send(Encoding.UTF8.GetBytes(dataY.ToString()));


            //data = new byte[1024];

            recX = server.Receive(dataX);
            recY = server .Receive(dataY);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("10.63.42.206"), 9050);
                try
                {
                    server.Connect(ipep);

                }
                catch (SocketException)
                {
                connectButton.Text = "Error";
                Thread.Sleep(1000);
                connectButton.Text = "Connect";
                }
            }
        }
    }

