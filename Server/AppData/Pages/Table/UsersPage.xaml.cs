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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public static UsersPage Instance { get; private set; }
        public UsersPage()
        {
            InitializeComponent();
            Instance = this;
        }
        public void UpduteList()
        {
            listView.ItemsSource = Connect.GetListUsers;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditUserWindow addEditRoomWindow = new AddEditUserWindow(null);
            addEditRoomWindow.ShowDialog();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditUserWindow addEditRoomWindow = new AddEditUserWindow((sender as Button).DataContext as Users);
            addEditRoomWindow.ShowDialog();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var deleteUsers = listView.SelectedItems.Cast<Users>().ToList();
            if (MessageBox.Show($"Удалить {deleteUsers.Count} запсией", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                foreach (var deleteUser in deleteUsers)
                {
                    var listLink = Connect.GetLinks;
                    foreach (var link in listLink)
                        if (link.idUser == deleteUser.idUser)
                            Connect.GetLinks.Remove(link);
                    Connect.GetUsers.Remove(deleteUser);
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
            var user = (sender as Button).DataContext as Users;
            MainPage.Instance.frameLinks.Navigate(new LinksPage(user.idUser, false));
            MainPage.Instance.frameLinks.Visibility = Visibility.Visible;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpduteList();
        }

        private void Page_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpduteList();
        }
    }
}
