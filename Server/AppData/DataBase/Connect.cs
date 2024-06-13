using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.AppData.DataBase
{
    internal class Connect
    {
        private static KasperChatEntities c;
        public static KasperChatEntities Context
        {
            get
            {
                if (c == null)
                    c = new KasperChatEntities();
                return c;
            }
        }
        public static System.Data.Entity.DbSet<Link> GetLinks
        {
            get
            {
                return Context.Link;
            }
        }
        public static System.Data.Entity.DbSet<Rooms> GetRooms
        {
            get
            {
                return Context.Rooms;
            }
        }
        public static System.Data.Entity.DbSet<Users> GetUsers
        {
            get
            {
                return Context.Users;
            }
        }

        public static List<Link> GetListLink
        {
            get
            {
                return Context.Link.ToList();
            }
        }
        public static List<Rooms> GetListRooms
        {
            get
            {
                return Context.Rooms.ToList();
            }
        }
        public static List<Users> GetListUsers
        {
            get
            {
                return Context.Users.ToList();
            }
        }


    }
}
