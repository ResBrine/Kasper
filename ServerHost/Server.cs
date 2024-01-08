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

namespace ServerHost
{
    internal class Server
    {
        TcpListener tcpListener;
        List<TcpClient> tcpClients = new List<TcpClient>();
        Thread threadListConnect;
        Thread threadClientListen;
        private int CountClients = 0;
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
            foreach (TcpClient client in tcpClients)
            {
                networkStream = client.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes(msg);
                if (networkStream != null)
                    networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
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
            threadClientListen.Suspend();
            threadListConnect.Suspend();
            foreach (var item in tcpClients)
                item.Close();
            tcpListener.Stop();
        }
         void ListenConnect()
        {
            while (true)
            {
                tcpClients.Add(tcpListener.AcceptTcpClient());
                threadClientListen = new Thread(new ThreadStart(ListenClient));
                threadClientListen.Start();
                CountClients++;
                Console.WriteLine("New Client");
            }
        }

        void ListenClient()
        {
            int idClient = CountClients;
            NetworkStream networkStream = tcpClients[idClient-1].GetStream();

            byte[] data = new byte[1024];
            int bytes;

            while ((bytes = networkStream.Read(data, 0, data.Length)) != 0)
            {
                // Преобразуем данные в ASCII string
                string responseData = System.Text.Encoding.Unicode.GetString(data, 0, bytes);
                Console.WriteLine(responseData);
                // Вызываем метод из основного потока
                CallBackAll(responseData);
            }

        }
        void CallBackAll(string message)
        {
            SetMsg(message);
        }

    }

}


