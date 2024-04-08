using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{
    internal class Client
    {
        public int id;
        public Socket socket;
        public string userName;
        public string password;


        public string ip;
        public int port;
        public Client(int id)
        {
            this.id = id;
        }
        
        public void setUserName(string userName)
        {
            this.userName = userName;
        }
        public void setSocket(Socket socket)
        {
            this.socket = socket;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }
    }
}
