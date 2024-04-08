using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;

namespace TestClient
{
    internal class Program
    {
        static NetworkStream stream;
        static string userName = "";
        static void Main(string[] args)
        {
            /*Console.Write("Введите ip-адрес:");
            var ip = Console.ReadLine();
            Console.Write("Введите порт(8301 - по умолчанию):");
            var port = int.Parse(Console.ReadLine());
            TcpClient client = new TcpClient(ip, port);*/
            TcpClient client = new TcpClient("localhost", 8305);
            stream = client.GetStream();
            Console.Write("Введите имя пользователя:");
            userName = Console.ReadLine();
            Console.Write("Введите пороль пользователя:");
            string password = Console.ReadLine();
            API_REGISTRATION(userName, password);

            Thread thread = new Thread(new ThreadStart(ReadStream));
            thread.Start();
            

            while (true)
            {
                SendString(Console.ReadLine());
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
                // Преобразуем данные в ASCII string
                string responseData = Encoding.ASCII.GetString(data, 0, bytes).Replace("\0","");
                // Вызываем метод из основного потока
                MainThreadMethod(responseData);
            }
        }

        static void MainThreadMethod(string data)
        {
            if (data.Split(':')[0] != userName)
                Console.WriteLine("\n" + data);
        }
    }
}
