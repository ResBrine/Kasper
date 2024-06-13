using Server.AppData.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommonLibrary.APIManager;

namespace Server.AppData.DataBase
{
    internal class ManagerData
    {
        public static List<Room> GetRoomsByIdUser(int idUser)
        {
            List<Room> list = new List<Room>();

            foreach (var item in Connect.GetLinks.Where(x => x.idUser == idUser).ToList()) 
            { 
                list.Add(new Room(item.idRoom, Connect.GetRooms.Where(x=> x.idRoom == item.idRoom).First().roomName ));
            }           
            return list;
        }
        public static List<int> GetUsersByIdRoom(int idRoom)
        {
            List<int> list = new List<int>();

            foreach (var item in Connect.GetLinks.Where(x => x.idRoom == idRoom).ToList())
            {
                list.Add(item.idUser);
            }
            return list;
        }
        public static List<Client> GetUsers()
        {
            List<Client> list = new List<Client>();

            foreach (var item in Connect.GetListUsers)
                list.Add(new Client(item.idUser,item.userName,item.password,GetRoomsByIdUser(item.idUser)));
            return list;
        }
    }
}
