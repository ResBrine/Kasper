using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerHost
{
    class LoginDataClient
    {
        public string userName {get;set;}
        public string password {get;set;}
    }
    class FullDataClient
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string frends { get; set; }
        public string rooms { get; set; }
    }

    class APIManager
    {
        public static string createSimpleAnswer(string header, string value)
        {
            return $"{header}\n" +
                "{\n" +
                $"answer={value}\n" +
                "}\n";
        } 
        public static LoginDataClient getLoginData(string data)
        {
            return JsonSerializer.Deserialize<LoginDataClient>(data);
        }
        public static FullDataClient getFullData(string data)
        {
            return JsonSerializer.Deserialize<FullDataClient>(data);
        }
    }
}


