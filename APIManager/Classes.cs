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
        public class Message{
            public bool typeMessage { get; set; } = true;
            public long idChat { get; set; }
            public long idUser { get; set; }
            public string nameUser { get; set; }
            public string message { get; set; }
            public DateTime date { get; set; }
            public Message(long idChat, long idUser, string nameUser , string message, DateTime date)
            {
                this.idChat = idChat;
                this.idUser = idUser;
                this.nameUser = nameUser;
                this.message = message;
                this.date = date;
            }
        }
        public class AutificationDataClient{
            public bool typeAutificationDataClient { get; set; } = true;
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
            public bool typeFullDataClient { get; set; } = true;

            public string userName { get; set; }
            public string password { get; set; }
            public string frends { get; set; }
            public string rooms { get; set; }
        }
        public class SimplAnswer
        {
            public bool typeSimplAnswer { get; set; } = true;

            public string header { get; set; }
            public string value { get; set; }

            public SimplAnswer(string header, string value) { 
                this.header = header;
                this.value = value;
            }
        }

        public static string createSimpleAnswer(string header, string value){
            return JsonSerializer.Serialize(new SimplAnswer(header, value ));
        }
        public static string CreateMessage(Message message)
        {
            return JsonSerializer.Serialize(message);
        }
        public static string CreateMessage(long idChat, long idUser, string nameUser,string message, DateTime date)
        {
            return JsonSerializer.Serialize(new Message(idChat, idUser, nameUser, message, date));
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
