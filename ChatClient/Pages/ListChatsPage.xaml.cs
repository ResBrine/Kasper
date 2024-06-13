using ChatClient.Pages.Items;
using CommonLibrary;
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
        public List<APIManager.Chat> Chats = new List<APIManager.Chat>();
        public ListChatsPage()
        {
            InitializeComponent();
            var chat0 = new APIManager.Chat(0);
            chat0.messages.Add(new APIManager.Message(0, 0, "BOMB", "Привет", DateTime.Now.AddHours(-1).AddMinutes(-34)));
            chat0.messages.Add(new APIManager.Message(0, 0, "BOMB", "Аууу", DateTime.Now.AddHours(-1).AddMinutes(-12)));
            chat0.messages.Add(new APIManager.Message(0, 0, "BOMB", "Ответь игнорщик", DateTime.Now.AddMinutes(-54)));
            chat0.messages.Add(new APIManager.Message(0, 0, "BOMB", "Ясно понятно", DateTime.Now.AddMinutes(-23)));
            Chats.Add(chat0);
            var chat1 = new APIManager.Chat(1);
            chat1.messages.Add(new APIManager.Message(1, 1, "Kasper", "Ой", DateTime.Now.AddHours(-1).AddMinutes(-45)));
            chat1.messages.Add(new APIManager.Message(1, 1, "Kasper", "Я не игнорил теебя", DateTime.Now.AddHours(-1).AddMinutes(-33)));
            chat1.messages.Add(new APIManager.Message(1, 1, "Kasper", "У меня инет проподает", DateTime.Now.AddMinutes(-34)));
            chat1.messages.Add(new APIManager.Message(1, 1, "Kasper", "Прости", DateTime.Now.AddMinutes(-12)));
            Chats.Add(chat1);
            var listChats = new List<ItemChat>();
            foreach (var item in Chats)
            {
                listChats.Add(new ItemChat(item));
            }

            listViewChats.ItemsSource = listChats;
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                MainPage.navigation.Navigate(new ChatPage((item.Content as ItemChat).chat.messages));
            }
        }
    }
}
