using Server.AppData.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace Pages.Table
{
    /// <summary>
    /// Логика взаимодействия для ActivityPage.xaml
    /// </summary>
    public partial class ActivityPage : Page
    {
        public ActivityPage()
        {
            InitializeComponent();
            UpdateList();
        }
        public void UpdateList()
        {
            listView.ItemsSource = Connect.GetListUsers;
        }
        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            Users user = (sender as Button).DataContext as Users;

            double countUsers = 0;
            double countMessgesAll = 0;
            foreach (var item in Connect.GetUsers)
            {
                if (item.idUser != user.idUser)
                {
                    countUsers++;
                    countMessgesAll += item.countMessage;
                }
            }
            double k = user.countMessage / (countMessgesAll / countUsers);
            MessageBox.Show($"{user.userName} в {k} круче других");
        }
    }
}
