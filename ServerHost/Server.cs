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
using System.Security.Policy;

namespace ServerHost
{
    internal class Server
    {
        Socket socket;
        Clients ManagerClients = new Clients();
        Thread threadListConnect;
        Thread threadClientListen;
        /// <summary>
        /// Создание и настройка сервера
        /// </summary> 
        /// <param name="port">Открытый порт для прослушивания клиентов</param>
        public Server(int port)
        {
            ManagerClients = DataManager.LoadListClients();
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);   // связываем с локальной точкой ipPoint
        }

        public void Start()
        {
            socket.Listen(1000);
            threadListConnect = new Thread(new ThreadStart(ListenConnect));
            threadListConnect.Start();
            Console.WriteLine("Сервер запущен!");
        }
        public void Stop()
        {
            threadClientListen.Abort();
            threadListConnect.Abort();
            ManagerClients.StopAll();
            socket.Close();
            Console.WriteLine("Сервер сстановлен!");
        }
        void ListenConnect()
        {
            Thread WiteAuthorizationThread;
            while (true)
            {
                var socket = ClientConnectedNow();
                string adress = socket.RemoteEndPoint.ToString();
                Console.WriteLine($"Новое подключение с адресса:{adress}");

                WiteAuthorizationThread = new Thread(WiteAuthorization);
                WiteAuthorizationThread.Start(socket);
            }
        }
        void WiteAuthorization(object _socket)
        {
            while (true)
            {
            restartGetFullData:
                var client = GetFullDataAboutClient(_socket as Socket);
                if (client != null)
                {
                    threadClientListen = new Thread(ListenClient);
                    threadClientListen.Start(client);
                    break;
                }
                else
                    goto restartGetFullData;
            }
        }
        void ListenClient(object _client)
        {
            try
            {
                Client client = _client as Client;
                NetworkStream networkStream = new NetworkStream(client.socket);

                byte[] data = new byte[1024];
                int bytes;

                while ((bytes = networkStream.Read(data, 0, data.Length)) != 0)
                {
                    string responseData = Encoding.ASCII.GetString(data, 0, bytes).Replace("\0", "");
                    Console.WriteLine(client.userName + ":" + responseData);
                    CallBackAll(client.userName + ":" + responseData);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message.ToString());
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

            string adress = client.RemoteEndPoint.ToString();
            Console.WriteLine($"Попытка авторизации с адресса:{adress}");

            while (true)
                if ((bytes = new NetworkStream(client).Read(data, 0, data.Length)) != 0)
                {
                    string request = Encoding.ASCII.GetString(data, 0, bytes);

                    if (request.Split('\n')[0] == "login")
                    {
                        var loginData = APIManager.getLoginData(request);
                        foreach (var itemClient in ManagerClients.GetClients())
                            if (itemClient.userName == loginData.userName && itemClient.password == loginData.password)
                            {
                                itemClient.setSocket(client);
                                Write(itemClient.socket, APIManager.createRequest("login", $"id:{itemClient.id}"));
                                Console.WriteLine($"{adress}>Залогинился как:{itemClient.userName}, ID={itemClient.id}");
                                return itemClient;
                            }

                        Write(client, APIManager.createRequest("login", "error"));
                        Console.WriteLine($"{adress}>Неудачная авторизация");

                    }
                    if (request.Split('\n')[0] == "registration")
                    {
                        var loginData = APIManager.getLoginData(request);
                        foreach (var itemClient in ManagerClients.GetClients())
                            if (itemClient.userName == loginData.userName)
                            {
                                Write(client, APIManager.createRequest("registration", "error:408"));
                                Console.WriteLine($"{adress}>Неудачная авторизация");
                                return null;
                            }
                        var newClient = ManagerClients.CreateClient(loginData);
                        Write(client, APIManager.createRequest("registration", $"id:{newClient.id}"));
                        newClient.setSocket(client);
                        return newClient;

                    }

                    break;
                }

            Console.WriteLine($"{adress}>Неудачная авторизация");
            return null;
        }
        void Write(Socket socket, string str)
        {
            NetworkStream networkStream = new NetworkStream(socket);
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
        }


        void CallBackAll(string message)
        {
            foreach (var client in ManagerClients.GetActiveClients())
            {
                NetworkStream networkStream = new NetworkStream(client.socket);
                byte[] bytes = Encoding.Unicode.GetBytes(message);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
            }
        }

    }

}


