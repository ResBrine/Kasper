using Server;
using Server.AppData.DataBase;
using Server.AppData.Pages;
using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pages.Table
{
    /// <summary>
    /// Логика взаимодействия для LinksPage.xaml
    /// </summary>
    public partial class LinksPage : Page
    {
        private int id{ get; set; }
        private bool isRooms = true;
        public LinksPage(int id, bool isRoom)
        {
            this.isRooms = isRoom;
            this.id = id;
            InitializeComponent();
        }
        private void UpduteListAdd(string search = "")
        {
            List<Rooms> rooms = new List<Rooms>();
            List<Users> users = new List<Users>();
            if (isRooms)
            {
                foreach (var item in Connect.GetListUsers)
                    if (item.userName.ToLower().StartsWith(search))
                    {
                        int count = 0;
                        foreach (var item1 in Connect.GetLinks)
                            if (item1.idRoom == id && item1.idUser == item.idUser)
                                count ++;
                            if (count == 0)
                                users.Add(item);
                    }    
                        
                listViewAll.ItemsSource = users;

            }
            else
            {
                foreach (var item in Connect.GetListRooms)
                    if (item.roomName.ToLower().StartsWith(search))
                    {
                        int count = 0;
                        foreach (var item1 in Connect.GetLinks)
                            if (item1.idUser == id && item1.idRoom == item.idRoom)
                                count++;
                        if (count == 0)
                            rooms.Add(item);
                    }
                listViewAll.ItemsSource = rooms;

            }
        }
        private void UpduteList(string search = "")
        {
            List<Rooms> rooms = new List<Rooms>();
            List<Users> users = new List<Users>();
            if (isRooms)
            {
                foreach (var item in Connect.GetLinks)
                    if (item.idRoom == id && item.Users.userName.ToLower().StartsWith(search))
                        users.Add(item.Users);
                listView.ItemsSource = users;

            }
            else
            {
                foreach (var item in Connect.GetLinks)
                    if (item.idUser == id && item.Rooms.roomName.ToLower().StartsWith(search))
                        rooms.Add(item.Rooms);
                listView.ItemsSource = rooms;

            }
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isRooms)
                Connect.GetLinks.Add(new Link(id,((sender as Button).DataContext as Users).idUser, ""));
            else
                Connect.GetLinks.Add(new Link(((sender as Button).DataContext as Rooms).idRoom,id,""));
            try
            {
                Connect.Context.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpduteList();
            UpduteListAdd(textBoxSearch.Text.ToLower());
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {

            if (isRooms)
            {
                var user = (sender as Button).DataContext as Users;
                foreach (var item in Connect.GetLinks)
                    if (item.idRoom == id && item.idUser == user.idUser)
                        Connect.GetLinks.Remove(item);
            }
            else
            {
                var room = (sender as Button).DataContext as Rooms;
                foreach (var item in Connect.GetLinks)
                    if (item.idUser == id && item.idRoom == room.idRoom)
                        Connect.GetLinks.Remove(item);
            }
           
            try
            {
                Connect.Context.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpduteList();
            UpduteListAdd(textBoxSearch.Text.ToLower());

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpduteList();
            UpduteListAdd();
        }

        private void HidBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage.FrameLinks.Visibility = Visibility.Hidden;
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpduteListAdd(textBoxSearch.Text.ToLower());
        }

        private void Page_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
