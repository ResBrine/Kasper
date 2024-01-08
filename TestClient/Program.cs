using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    internal class Program
    {
        static NetworkStream stream;

        static void Main(string[] args)
        {
            //176,59,76,254
            //new IPEndPoint(new IPAddress(new byte[] { 127,0,0,2 })
            TcpClient client = new TcpClient("localhost", 8301);
            stream = client.GetStream();

            Thread thread = new Thread(new ThreadStart(ReadStream));
            thread.Start();
            while(true)
            {
                SetMsg(Console.ReadLine());
            }
        }
        public static void SetMsg(string msg)
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
                string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                // Вызываем метод из основного потока
                MainThreadMethod(responseData);
            }
        }

        static void MainThreadMethod(string data)
        {
            // Здесь ваш код
            Console.WriteLine("Получены данные: " + data);
        }
    }
}
