using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{
    class INFO
    {
        public static string PATH_CONFIG_SERVER
        {
            get 
            { 
                return "ConfigServer.ini";
            }
        }
        public static string PATH_LIST_USERS
        {
            get
            {
                return "ListUsers.ini";
            }
        }


        public static int PORT = 8301;
    }
}
