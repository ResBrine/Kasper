using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static CommonLibrary.APIManager;

namespace Server.AppData.Server
{
    internal class Clients
    {
        public List<Client> clients { get; set; } = new List<Client>();
        public int Count
        {
            get { return clients.Count; }
        }
        public Clients() { }
        public Clients(List<Client> clients) {
            Clear();
            this.clients.AddRange(clients);
        }

        public void AddRange(List<Client> clients)
        {
            this.clients.AddRange(clients);
        }
        public void Clear()
        {
            this.clients.Clear();
        }
        public List<Client> GetClients()
        {
            return clients;
        }
        public List<Client> GetActiveClients()
        {
            List<Client> clients = new List<Client>();
            foreach (var client in this.clients)
                if (client.socket != null)
                    if (client.socket.Connected)
                        clients.Add(client);
            return clients;
        }

        public void StopAll()
        {
            foreach (var client in GetActiveClients())
                client.socket.Close();
        }
        public Client CreateClient(APIManager.AutificationDataClient loginData)
        {
            Client client = new Client(Count, loginData.userName, loginData.password, new List<Room>());
            clients.Add(client);
            //DataManager.SaveListClients(this);
            return client;
        }
    }
    internal class Client
    {
        public int id;
        public Socket socket;
        public string userName;
        public string password;
        public List<Room> rooms = new List<Room>();


        public string ip;
        public int port;

        public ClientJson GetClientJson()
        {
            return new ClientJson(id, userName, password, rooms);
        }

        public Client(int id, string userName, string password, List<Room> rooms)
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
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<Room> rooms { get; set; } = new List<Room>();

        public ClientJson(int id, string userName, string password, List<Room> rooms)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.rooms = rooms;
        }
    }
}
