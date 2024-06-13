using Pages.Table;
using Server.AppData.DataBase;
using Server.AppData.Pages;
using Server.AppData.Pages.AddEditPages;
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
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public static SearchPage Instance { get; private set; }
        public SearchPage()
        {
            InitializeComponent();
            Instance = this;
        }
        private void UpduteList(string search = "")
        {
            List<RoomAndUser> list = new List<RoomAndUser>();
            foreach (var item in Connect.GetListRooms)
                if (item.roomName.ToLower().Contains(search.ToLower()))
                    list.Add(new RoomAndUser(item));
            foreach (var item in Connect.GetListUsers)
                if (item.userName.ToLower().Contains(search.ToLower()))
                list.Add(new RoomAndUser(item));
            listView.ItemsSource = list;
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var roomAndUser = (sender as Button).DataContext as RoomAndUser;
            if (roomAndUser.isUser)
            {
                AddEditUserWindow addEditUserWindow = new AddEditUserWindow(roomAndUser.User);
                addEditUserWindow.ShowDialog();
            }
            else
            {
                AddEditRoomWindow addEditRoomWindow = new AddEditRoomWindow(roomAndUser.Room);
                addEditRoomWindow.ShowDialog();

            }
           
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var roomAndUser = (sender as Button).DataContext as RoomAndUser;
            if (roomAndUser.isUser)
            {
                if (MessageBox.Show($"Удалить {roomAndUser.UserName}", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var listLink = Connect.GetLinks;
                        foreach (var link in listLink)
                            if (link.idUser == roomAndUser.IdUser)
                                Connect.GetLinks.Remove(link);
                        Connect.GetUsers.Remove(roomAndUser.User);
                }
            }
            else
            {
                if (MessageBox.Show($"Удалить {roomAndUser.RoomName}", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var listLink = Connect.GetLinks;
                    foreach (var link in listLink)
                        if (link.idRoom == roomAndUser.IdRoom)
                            Connect.GetLinks.Remove(link);
                    Connect.GetRooms.Remove(roomAndUser.Room);
                }

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
        }
        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            var roomAndUser = (sender as Button).DataContext as RoomAndUser;
            if (roomAndUser.isUser)
            {
                MainPage.Instance.frameLinks.Navigate(new LinksPage(roomAndUser.IdUser, false));
            }
            else
            {
                MainPage.Instance.frameLinks.Navigate(new LinksPage(roomAndUser.IdRoom, false));

            }
            MainPage.Instance.frameLinks.Visibility = Visibility.Visible;


        }
        private void editTextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpduteList(editTextSearch.Text.ToLower());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpduteList();
        }

        class RoomAndUser
        {
            public Users User {
                get
                {
                    foreach (var item in Connect.GetUsers)
                        if (item.idUser == IdUser)
                            return item;
                    return null;
                }
            }
            public Rooms Room
            {
                get
                {
                    foreach (var item in Connect.GetRooms)
                        if (item.idRoom == IdRoom)
                            return item;
                    return null;
                }
            }
            public bool isUser { get; set; }
            public int IdUser { get; set; }
            public int IdRoom { get; set; }
            public string UserName { get; set; }
            public string RoomName { get; set; }
            public string Password { get; set; }
            public RoomAndUser(int idUser, string userName, string password)
            {
                isUser = true;
                IdUser = idUser;
                UserName = userName;
                Password = password;
            }
            public RoomAndUser(int idRoom, string roomName)
            {
                isUser = false;
                IdRoom = idRoom;
                RoomName = roomName;
            }
            public RoomAndUser(Rooms rooms) : this(rooms.idRoom, rooms.roomName) { }
            public RoomAndUser(Users users) : this(users.idUser, users.userName, users.password) { }
            public RoomAndUser() { }
        }

        private void Page_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpduteList(editTextSearch.Text.ToLower());
        }
    }
}
