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
        private void listViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (listViewMenu.SelectedIndex)
            {
                case 0:
                    FrameWorkspace.Navigate(new SearchPage()); break;
                case 2: 
                        FrameWorkspace.Navigate(new UsersPage()); break;
                    case 3:
                        FrameWorkspace.Navigate(new RoomsPage()); break;
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
