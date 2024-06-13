using Server;
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
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        public static RoomsPage Instance { get; private set; }
        public RoomsPage()
        {
            InitializeComponent();
            Instance = this;
        }
        public void UpduteList()
        {
            listView.ItemsSource = Connect.GetListRooms;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditRoomWindow addEditRoomWindow = new AddEditRoomWindow(null);
            addEditRoomWindow.ShowDialog();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditRoomWindow addEditRoomWindow = new AddEditRoomWindow((sender as Button).DataContext as Rooms);
            addEditRoomWindow.ShowDialog();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {

            var deleteRooms = listView.SelectedItems.Cast<Rooms>().ToList();
            if (MessageBox.Show($"Удалить {deleteRooms.Count} запсией", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                foreach (var deleteRoom in deleteRooms)
                {
                    var listLink = Connect.GetLinks;
                    foreach (var link in listLink)
                        if (link.idRoom == deleteRoom.idRoom)
                            Connect.GetLinks.Remove(link);
                    Connect.GetRooms.Remove(deleteRoom);
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpduteList();
        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            var room = (sender as Button).DataContext as Rooms;
            MainPage.Instance.frameLinks.Navigate(new LinksPage(room.idRoom, true));
            MainPage.Instance.frameLinks.Visibility = Visibility.Visible;

        }
        private void Page_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpduteList();
        }
    }
}
