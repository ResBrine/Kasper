using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{
    class LoginData
    {
        public string userName;
        public string password;
    }
    class APIManager
    {
        public static string createRequest(string header, string value)
        {
            return $"{header}\n" +
                "{\n" +
                $"request={value}\n" +
                "}";
        }
        public static LoginData getLoginData(string data)
        {

            string userName = "";
            string password = "";

            foreach (var line in data.Split('\n'))
            {                
                if (line.StartsWith("userName="))
                    userName = line.Substring(9);

                if (line.StartsWith("password="))
                    password = line.Substring(9);
            }

            LoginData loginData = new LoginData();
            loginData.userName = userName;
            loginData.password = password;
            return loginData;
        }               

    }
}
