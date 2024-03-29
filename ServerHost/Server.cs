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
        Socket socket;
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
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);   // связываем с локальной точкой ipPoint
        }

        public void SetMsg(string msg)
        {
            NetworkStream networkStream = null;
            foreach (var item in clients)
            {
                if(item.tcpClient.Connected == true)
                networkStream = new NetworkStream(item.tcpClient);
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
            socket.Listen(1000);
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
        //    socket.Stop();
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
        Socket ClientConnectedNow()
        {
            return socket.Accept();
        }
        Client GetFullDataAboutClient(Socket client)
        {
            byte[] data = new byte[256];
            int bytes;
            Client newClient = null;
            while (true)
                if ((bytes = new NetworkStream(client).Read(data, 0, data.Length)) != 0)
                {
                    newClient = new Client(client, Encoding.ASCII.GetString(data, 0, bytes));
                    break;
                }
            return newClient;
        }

        void ListenClient()
        {
            try
            {
                Client client = clients[CountClients - 1];
                NetworkStream networkStream = new NetworkStream(client.tcpClient);

                byte[] data = new byte[1024];
                int bytes;
                
                while ((bytes = networkStream.Read(data, 0, data.Length)) != 0)
                {
                    string responseData = Encoding.ASCII.GetString(data, 0, bytes);
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


