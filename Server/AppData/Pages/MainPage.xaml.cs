using Server.AppData.DataBase;
using System;
using System.Threading;
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
using Server;
using Server.AppData.Pages;
using Pages.Table;
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;
using System.Windows.Markup;

namespace Server.AppData.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static MainPage Instance { get; private set; }
        public static Frame FrameWorkspace { get; private set; }
        public static Frame FrameLinks { get; private set; }
        public MainPage()
        {
            InitializeComponent();
            Instance = this;
            FrameWorkspace = frameWorksapce;
            FrameLinks = frameLinks;
        }
        void BackUp()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Резервная копия(*.bak)|*.bak|Все файлы(*.*)|*.*";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string backupPath = System.IO.Path.Combine(@"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA", "KasperChat.mdf");
                string backupCommand = $@"BACKUP DATABASE [{backupPath}] TO DISK = N'{dialog.FileName}' WITH NOFORMAT, NOINIT, NAME = N'{dialog.FileName}', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

                using (var connection = new SqlConnection("Data Source=NB-IDEAPAD5-FVK\\SQLEXPRESS;Initial Catalog=KasperChat;Integrated Security=True;Encrypt=False;Application Name=EntityFramework"))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = backupCommand;
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Резервная копия создана");
            }
        }
        private void listViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (listViewMenu.SelectedIndex)
            {
                case 0:
                    FrameWorkspace.Navigate(new SearchPage()); break;
                case 1:
                    FrameWorkspace.Navigate(new ActivityPage()); break;
                case 2:
                    FrameWorkspace.Navigate(new UsersPage()); break;
                case 3:
                    FrameWorkspace.Navigate(new RoomsPage()); break;
                case 4:
                    FrameWorkspace.Navigate(new RatingsPage()); break;
                case 5:
                    FrameWorkspace.Navigate(new ReportPage()); break;
                case 6:
                    BackUp(); break;
                default:
                    break;
            }

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
