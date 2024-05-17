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

namespace ChatClient.Pages.Items
{
    /// <summary>
    /// Логика взаимодействия для ItemChat.xaml
    /// </summary>
    public partial class ItemChat : UserControl
    {
        public ImageSource avatar
        {
            get { return image.Source; }
            set { image.Source = value; }
        }
        public string userName
        {
            get { return Name.Text; }
            set { Name.Text = value; }
        }
        public string message
        {
            get { return lastMessage.Text; }
            set { lastMessage.Text = value; }
        }
        public ItemChat()
        {
            InitializeComponent();
            
        }
        public ItemChat(string userName, string message)
        {
            InitializeComponent();
            this.userName = userName;
            this.message = message;
        }
    }
}
