using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommonLibrary;
using System.IO;
using Server.AppData.DataBase;
using System.Runtime.Remoting.Contexts;

namespace Server.AppData.Server
{
    public class Server
    {
        private Socket socket;
        Clients managerClients = new Clients();

        public Server(int port)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
            managerClients.AddRange(ManagerData.GetUsers());
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);

        }
        public void Start()
        {
            socket.Listen(1000);
            new Thread(new ThreadStart(ListenConnect)).Start();
            Console.WriteLine("Сервер запущен");
        }
        public void Stop()
        {
            socket.Close();
            Console.WriteLine("Сервер остановлен");
        }

        private void ListenConnect()
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
            Client client = null;
            while (client==null)
                client = GetFullDataAboutClient(_socket as Socket);
            new Thread(ListenClient).Start(client);

        }
        Client GetFullDataAboutClient(Socket client)
        {
            string adress = client.RemoteEndPoint.ToString();
            byte[] data = new byte[256];
            int bytes;

            while (true)
                if ((bytes = new NetworkStream(client).Read(data, 0, data.Length)) != 0)
                {
                    string request = Encoding.UTF8.GetString(data, 0, bytes);
                    var answer = APIManager.GetLoginData(request);
                    Console.WriteLine(answer);
                        foreach (var itemClient in managerClients.GetClients())
                            if (itemClient.userName == answer.userName && itemClient.password == answer.password)
                            {
                                itemClient.setSocket(client);
                                Write(itemClient.socket, APIManager.CreateFullDataClient(itemClient.id, itemClient.userName, itemClient.rooms));
                                Console.WriteLine($"{adress}>Залогинился как:{itemClient.userName}, ID={itemClient.id}");
                                return itemClient;
                            }
                    break;
                }
            Console.WriteLine($"{adress}>Неудачная авторизация");
            return null;
        }

        Socket ClientConnectedNow()
        {
            return socket.Accept();
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
                    string responseData = Encoding.UTF8.GetString(data, 0, bytes).Replace("\0", "");
                    var message = APIManager.GetMessage(responseData);
                    if (message != null)
                    {
                        Connect.IncrementMessageCount((int)message.idUser);
                        CallBackAll(message);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            }


        }

        void CallBackAll(APIManager.Message message)
        {
            Console.WriteLine(APIManager.CreateMessage(message));
            foreach (var client in managerClients.GetActiveClients())
                foreach (var item in client.rooms)
                    if (item.idChat == message.idChat)
                    {
                        NetworkStream networkStream = new NetworkStream(client.socket);
                        byte[] bytes = Encoding.UTF8.GetBytes(APIManager.CreateMessage(message));
                        networkStream.Write(bytes, 0, bytes.Length);
                        networkStream.Flush();
                    }
        }

        void Write(Socket socket, string str)
        {
            NetworkStream networkStream = new NetworkStream(socket);
            byte[] bytes = Encoding.UTF8.GetBytes(str + "\r\n");
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
        }
    }
}
