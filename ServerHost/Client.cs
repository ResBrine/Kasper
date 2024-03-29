﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{
    internal class Client
    {
        public TcpClient tcpClient;
        public string ip;
        public int port;
        public string userName;

        public Client(TcpClient client, string Name)
        {
            tcpClient = client;
            ip = client.Client.RemoteEndPoint.ToString().Split(':')[0];
            port = int.Parse(client.Client.RemoteEndPoint.ToString().Split(':')[1]);
            userName = Name;
        }

        public void Rename(string Name)
        {
            userName = Name;
        }
    }
}
