using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int port = 8301;
            Server server = new Server(port);
            server.Start();
            Console.WriteLine("PORT:" + port);

            Console.WriteLine("Press any key to exit");
           Console.Read();
           

        }
    }
}
