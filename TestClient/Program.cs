using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using CommonLibrary;

enum StatusConnect
{
    connected = 3,
    witeConnected = 2,
    retry = 1,
    disconnected = 0
}
namespace TestClient
{
    internal class Program
    {
        static NetworkStream stream;
        static string userName = "";
        static long userId = 0;
        static StatusConnect statusConnect = StatusConnect.retry;
        static void Main(string[] args)
        {
            while (true)
            {
                /*Console.Write("Введите ip-адрес:");
                var ip = Console.ReadLine();
                Console.Write("Введите порт(8301 - по умолчанию):");
                var port = int.Parse(Console.ReadLine());
                TcpClient client = new TcpClient(ip, port);*/
                TcpClient client = new TcpClient("localhost", 8305);
                stream = client.GetStream();

                while (statusConnect == StatusConnect.retry)
                {
                    Console.Write("Введите имя пользователя:");
                    userName = Console.ReadLine();
                    Console.Write("Введите пороль пользователя:");
                    string password = Console.ReadLine();

                    Console.Write("1.Регистрация\n2.Вход\nВыберите действие:");
                    if (Console.ReadLine() == "1")
                        API_REGISTRATION(userName, password);
                    else
                        API_LOGIN(userName, password);

                    statusConnect = StatusConnect.witeConnected;

                    Thread thread = new Thread(new ThreadStart(ReadStream));
                    thread.Start();

                    while (statusConnect != StatusConnect.disconnected && statusConnect != StatusConnect.retry)
                        while (statusConnect == StatusConnect.connected)
                            SendString(APIManager.CreateMessage(0,userId,userName,Console.ReadLine(),DateTime.Now));
                    
                }


                client.Close();
            }
        }

        public static void API_LOGIN(string username, string password)
        {
            SendString(JsonSerializer.Serialize(new APIManager.AutificationDataClient(false,username, password)));
        }
        public static void API_REGISTRATION(string username, string password)
        {
            SendString(JsonSerializer.Serialize(new APIManager.AutificationDataClient(true,username, password)));

        }

        public static void SendString(string msg)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(msg);
            if (stream != null)
                stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
        static void ReadStream()
        {
            byte[] data = new byte[1024];
            int bytes;
            while ((bytes = stream.Read(data, 0, data.Length)) != 0)
            {
                string responseData = Encoding.ASCII.GetString(data, 0, bytes).Replace("\0","");
                //Console.WriteLine(responseData);
                MainThreadMethod(responseData);
            }
        }

        static void MainThreadMethod(string data)
        {
            try
            {
                var loginData = APIManager.GetSimplAnswer(data);
                userId = long.Parse(loginData.value);
                Console.WriteLine("Ваш " + loginData.value);
                statusConnect = StatusConnect.connected;

            }
            catch (Exception)
            {
            }
            try
            {
                var message = APIManager.GetMessage(data);
                if (message.message != null) 
                Console.WriteLine(message.nameUser.ToString()+ ":"+ message.message);

            }
            catch (Exception e)
            {

            }
        }
    }
}
