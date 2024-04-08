using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataManager.OpenConfigServer();
            Server server = new Server(INFO.PORT);
            server.Start();
            Console.WriteLine("PORT:" + INFO.PORT);
            Console.WriteLine("Press any key to exit");

            Console.Read();
           

        }
    }
}
