using Pages.Table;
using Server.AppData.DataBase;
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
using System.Windows.Shapes;

namespace Server.AppData.Pages.AddEditPages
{
    /// <summary>
    /// Логика взаимодействия для AddEditRoomWindow.xaml
    /// </summary>
    public partial class AddEditRoomWindow : Window
    {
        Rooms room;
        bool checkNew;
        public AddEditRoomWindow(Rooms c)
        {
            InitializeComponent();
            if (c == null)
            {
                c = new Rooms();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = room = c;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
                Connect.GetRooms.Add(room);
            try
            {
                Connect.Context.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
