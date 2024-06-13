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
        public List<int> rooms = new List<int>();


        public string ip;
        public int port;

        public ClientJson GetClientJson()
        {
            return new ClientJson(id, userName, password, rooms);
        }

        public Client(int id, string userName, string password, List<int> rooms)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.rooms = rooms;
        }
        public Client(ClientJson clientJson)
        {
            id = clientJson.id;
            userName = clientJson.userName;
            password = clientJson.password;
            rooms = clientJson.rooms;
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

    class ClientJson
    {
        public int id {  get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<int> rooms { get; set; } = new List<int>();

        public ClientJson(int id, string userName, string password, List<int> rooms)
        {
            this.id=id;
            this.userName=userName;
            this.password=password;
            this.rooms = rooms;
        }
    }
}
