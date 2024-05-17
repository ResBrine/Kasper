using ChatClient.Pages.Items;
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

namespace ChatClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListChatsPage : Page
    {
        public ListChatsPage()
        {
            InitializeComponent();
            var listChats = new List<ItemChat>();
            listViewChats.ItemsSource = listChats;
            listChats.Add(new ItemChat("Roma","Ну что ты?"));
            listChats.Add(new ItemChat("Kasper", "Алл райт"));
        }
    }
}
