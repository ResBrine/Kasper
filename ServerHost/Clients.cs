using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{
    internal class Clients
    {
        public List<Client> clients = new List<Client>();
        public int Count
        {
            get { return clients.Count; }
        }
        public Clients() { }

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
            return this.clients;
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
            {
                client.socket.Close();
            }
        }
        public Client CreateClient(LoginData loginData)
        {
            Client client = new Client(Count);
            client.setUserName(loginData.userName);
            client.setPassword(loginData.password);
            clients.Add(client);
            return client;
        }
    }
}
