using Server.AppData.Pages;
using Server.AppData.Server;
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

namespace Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public static Frame Navigation { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Navigation = MFrame;
            Navigation.Navigate(new MainPage());

            AppData.Server.Server server = new AppData.Server.Server(8301);
            server.Start();
        }
    }
}
