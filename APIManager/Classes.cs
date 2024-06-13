using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary
{

    public static class APIManager
    {
        public class Chat
        {
            public long idChat { get; set; }
            public List<Message> messages { get; set; } = new List<Message> { };
            
            public Chat(long idChat) { 
                this.idChat = idChat;            
            }
        }

        public class Message{
            public long idChat { get; set; }
            public long idUser { get; set; }
            public string nameUser { get; set; }
            public string message { get; set; }
            public string date { get; set; }
            public Message(long idChat, long idUser, string nameUser , string message, string date)
            {
                this.idChat = idChat;
                this.idUser = idUser;
                this.nameUser = nameUser;
                this.message = message;
                this.date = date;
            }
        }
        public class Room
        {
            public long idChat { get; set; }
            public string nameRoom { get; set; }
            public Room(long idChat, string nameRoom)
            {
                this.idChat = idChat;
                this.nameRoom = nameRoom;
            }
        }
        public class AutificationDataClient{
            public bool autificationDataClient { get; set; } = true;
            public bool isNew { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public AutificationDataClient(bool isNew, string userName, string password)
            { 
                this.isNew = isNew;
                this.userName = userName;
                this.password = password;
            }
        }
        public class FullDataClient{
            public bool fullDataClient { get; set; } = true;
            public long idUser { get; set; }
            public string userName { get; set; }
            public List<Room> rooms { get; set; }
            public FullDataClient(long idUser, string userName, List<Room> rooms)
            {
                this.idUser = idUser;
                this.userName = userName;
                this.rooms = rooms;
            }
        }
        public class SimplAnswer
        {
            public string header { get; set; }
            public string value { get; set; }

            public SimplAnswer(string header, string value) { 
                this.header = header;
                this.value = value;
            }
        }




        public static string CreateSimpleAnswer(string header, string value){
             return Encoding.UTF8.GetString(Encoding.Default.GetBytes(JsonSerializer.Serialize(new SimplAnswer(header, value ))));
        }
        public static string CreateMessage(Message message)
        {
             return Encoding.UTF8.GetString(Encoding.Default.GetBytes(JsonSerializer.Serialize(message)));
        }
        public static string CreateMessage(long idChat, long idUser, string nameUser, string message, string date)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(JsonSerializer.Serialize(new Message(idChat, idUser, nameUser, message, date))));
        }
        public static string CreateFullDataClient(long idUser, string userName, List<Room> rooms)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(JsonSerializer.Serialize(new FullDataClient(idUser, userName, rooms))));
        }
        public static Message GetMessage(string data)
        {
            return JsonSerializer.Deserialize<Message>(data);
        }
        public static SimplAnswer GetSimplAnswer(string data)
        {
            return JsonSerializer.Deserialize<SimplAnswer>(data);
        }
        public static AutificationDataClient GetLoginData(string data){
            return JsonSerializer.Deserialize<AutificationDataClient>(data);
        }
        public static FullDataClient GetFullData(string data){
            return JsonSerializer.Deserialize<FullDataClient>(data);
        }
    }
}
