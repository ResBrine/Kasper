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
            Server server = new Server(8301);
            server.Start();

           Console.WriteLine("Press any key to exit");
           Console.Read();
           

        }
    }
}
