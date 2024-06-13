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
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public bool isMeMessage
        {
            get 
            { 
                if (grid.HorizontalAlignment == HorizontalAlignment.Right)
                    return true;
                else 
                    return false;
            }
            set 
            {
                if (value)
                {
                    grid.HorizontalAlignment = HorizontalAlignment.Right;
                    textBoxText.TextAlignment = TextAlignment.Right;
                    Grid.SetColumn(textBoxText, 0);
                    Grid.SetColumn(textBoxDate, 1);
                }
                else
                {
                    grid.HorizontalAlignment = HorizontalAlignment.Left;
                    textBoxText.TextAlignment = TextAlignment.Left;
                    Grid.SetColumn(textBoxText, 1);
                    Grid.SetColumn(textBoxDate, 0);

                }
            }
        }
        public string text
        {
            get { return textBoxText.Text; }
            set { textBoxText.Text = value; }
        }
        public string date
        {
            get { return textBoxDate.Text; }
            set { textBoxDate.Text = value; }
        }
        public Message()
        {
            InitializeComponent();
            
        }
        public Message(string text, string date)
        {
            InitializeComponent();
            this.text = text;
            this.date = date;
        }
        public Message(string text, string date, bool isMeMessage)
        {
            InitializeComponent();
            this.text = text;
            this.date = date;
            this.isMeMessage = isMeMessage;
        }
    }
}
