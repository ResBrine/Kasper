using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace ServerHost
{
    internal class Server
    {
        TcpListener tcpListener;
        List<Client> clients = new List<Client>();
        Thread threadListConnect;
        Thread threadClientListen;
        private int CountClients
        {
            get { return clients.Count; }
        }
        /// <summary>
        /// Создание и настройка сервера
        /// </summary>
        /// <param name="port">Открытый порт для прослушивания клиентов</param>
        public Server(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
        }

        public void SetMsg(string msg)
        {
            NetworkStream networkStream = null;
            foreach (var item in clients)
            {
                if(item.tcpClient.Connected == true)
                networkStream = item.tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes(msg);
                if (networkStream != null)
                {
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                }
            }
        }
        public void Start()
        {
            tcpListener.Start();
            threadListConnect = new Thread(new ThreadStart(ListenConnect));
            threadListConnect.Start();
            Console.WriteLine("Start server");
        }
        public void Stop()
        {
            threadClientListen.Abort();
            threadListConnect.Abort();
            foreach (var item in clients)
                item.tcpClient.Close();
            tcpListener.Stop();
        }
         void ListenConnect()
        {
            while (true)
            {
                clients.Add(GetFullDataAboutClient(ClientConnectedNow()));
                threadClientListen = new Thread(new ThreadStart(ListenClient));
                threadClientListen.Start();
                Console.WriteLine("Connect client: " + clients[CountClients-1].ip+":"+ clients[CountClients - 1].port + " " + clients[CountClients - 1].userName);
            }
        }
        TcpClient ClientConnectedNow()
        {
            return tcpListener.AcceptTcpClient();
        }
        Client GetFullDataAboutClient(TcpClient tcpClient)
        {
            byte[] data = new byte[256];
            int bytes;
            Client newClient = null;
            while (true)
                if ((bytes = tcpClient.GetStream().Read(data, 0, data.Length)) != 0)
                {
                    newClient = new Client(tcpClient, Encoding.Unicode.GetString(data, 0, bytes));
                    break;
                }
            return newClient;
        }

        void ListenClient()
        {
            try
            {
                Client client = clients[CountClients - 1];
                NetworkStream networkStream = client.tcpClient.GetStream();

                byte[] data = new byte[1024];
                int bytes;
                
                while ((bytes = networkStream.Read(data, 0, data.Length)) != 0)
                {
                    string responseData = Encoding.Unicode.GetString(data, 0, bytes);
                    Console.WriteLine(client.userName +":" +responseData);
                    CallBackAll(client.userName + ":" + responseData);
                }
            }
            catch (IOException ex)
            { 
                Console.WriteLine(ex.Message.ToString());
            }


        }
        void CallBackAll(string message)
        {
            SetMsg(message);
        }

    }

}


