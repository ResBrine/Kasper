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
        public List<int> friends = new List<int>();


        public string ip;
        public int port;

        public ClientJson GetClientJson()
        {
            return new ClientJson(id, userName, password, friends);
        }

        public Client(int id)
        {
            this.id = id;
        }
        public Client(ClientJson clientJson)
        {
            id = clientJson.id;
            userName = clientJson.userName;
            password = clientJson.password;
            friends = clientJson.friends;
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
        public void addFrend(int idFrend)
        {
            friends.Add(idFrend);
        }
        public void removeFrend(int idFrend)
        {
            friends.Remove(idFrend);
        }
    }

    class ClientJson
    {
        public int id {  get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<int> friends { get; set; }

        public ClientJson(int id, string userName, string password, List<int> friends)
        {
            this.id=id;
            this.userName=userName;
            this.password=password;
            this.friends = friends;
        }
    }
}
