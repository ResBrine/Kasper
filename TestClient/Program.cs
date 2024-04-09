using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
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
                            SendString(Console.ReadLine());
                    
                }


                client.Close();
            }
        }

        public static void API_LOGIN(string username, string password)
        {
            SendString(
                 "login\n" +
                         "{\n" +
                         "userName=" + username + "\n" +
                         "password=" + password + "\n" +
                         "}"
             );
        }
        public static void API_REGISTRATION(string username, string password)
        {
            SendString(
                 "registration\n" +
                         "{\n" +
                         "userName=" + username + "\n" +
                         "password=" + password + "\n" +
                         "}"
             );
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
            byte[] data = new byte[256];
            int bytes;
            while ((bytes = stream.Read(data, 0, data.Length)) != 0)
            {
                string responseData = Encoding.ASCII.GetString(data, 0, bytes).Replace("\0","");
                MainThreadMethod(responseData);
            }
        }

        static void MainThreadMethod(string data)
        {
            if (data.Split('\n')[0] == "registration" || data.Split('\n')[0] == "login")
                if (data.Split('\n')[2].Substring(8).StartsWith("error"))
                {
                    Console.WriteLine(data.Split('\n')[2].Substring(8));
                    statusConnect = StatusConnect.retry;
                }
                else
                {
                    Console.WriteLine("Ваш id" + data.Split('\n')[2].Substring(8+3));
                    statusConnect = StatusConnect.connected;

                }
            else  
                if (data.Split(':')[0] != userName)
                    Console.WriteLine("\n" + data);
        }
    }
}
