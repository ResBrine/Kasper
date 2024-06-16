using System.Collections.Generic;
using System.Linq;

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
        public static void IncrementMessageCount(int userId)
        {
            var user = Context.Users.Find(userId);
            if (user != null)
            {
                user.countMessage++;
                foreach (var rating in GetRatings)
                    if (user.countMessage > rating.limit)
                        if (rating.idRating + 1 == GetRatings.Count())
                            user.rating = rating.idRating;
                        else
                            user.rating = rating.idRating + 1; 
                Context.SaveChanges();
            }
        }
        public static System.Data.Entity.DbSet<Ratings> GetRatings
        {
            get
            {
                return Context.Ratings;
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
