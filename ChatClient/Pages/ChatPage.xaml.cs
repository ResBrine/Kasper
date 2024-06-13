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
using static CommonLibrary.APIManager;

namespace ChatClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {

        private ItemCollection messages
        {
            get
            {
                return listMessage.Items;
            }
        }
        
        public ChatPage()
        {
            InitializeComponent();
            listMessage.ItemsSource = null;
        }
        public ChatPage(List<APIManager.Message> messages)
        {
            InitializeComponent();
            List<Items.Message> listMes = new List<Items.Message>();
            foreach (var item in messages)
            {
                listMes.Add(new Items.Message(item.message, item.date.Hour.ToString() + ":" + item.date.Minute.ToString()));
            }
            listMessage.ItemsSource = messages;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (
                e.Key == Key.Enter 
                && !(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) 
                && textBox.Text != ""
                )
            {
                bool isMeMessage = true;
                string message = textBox.Text;
                if (message[0] == '!')
                {
                    message = message.Remove(0, 1);
                    isMeMessage = false;
                }
                while (message[0] == '\n') message = message.Remove(0, 1);
                for(int i = message.Length - 1; message[i] == '\n'; i--) message =message.Remove(i--);

                var tempMessage = new Items.Message(message, DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString(), isMeMessage);
                messages.Add(tempMessage);
                textBox.Text = "";
                listMessage.Measure(new Size(1, 1));
                listMessage.ScrollIntoView(tempMessage);

            }
            else
            {
                int index = textBox.CaretIndex;
                if (e.Key == Key.Enter)
                {
                    textBox.Text = textBox.Text.Insert(index, "\n");
                    textBox.CaretIndex = index + 1;
                }
            }
        }
    }
}
