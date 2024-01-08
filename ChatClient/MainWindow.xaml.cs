using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ip = "95.32.48.141";
        private NetworkStream stream;
        private Socket client;
        private Thread thread;
        private bool isConnect = false;
        public MainWindow()
        {
            InitializeComponent();
        }



        private void buttonSendMessge_Click(object sender, RoutedEventArgs e)
        {
            if (isConnect && !string.IsNullOrEmpty(textBoxMessage.Text))
            {
                SendMsg(textBoxMessage.Text);
                textBoxMessage.Text = string.Empty;
                textBoxMessage.Focus();
            }
        }

        private void buttonConectDisconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isConnect)
                {
                    Disconnect();
                    buttonConectDisconnect.Content = "Подключиться";
                    textBoxName.IsReadOnly = false;
                    isConnect = false;
                }
                else
                {
                    Connect();
                    buttonConectDisconnect.Content = "Отключиться";
                    textBoxName.IsReadOnly = true;
                    isConnect = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void Connect()
        {

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ip, 8301);
            stream = new NetworkStream(client);
            SendMsg(textBoxName.Text);
            thread = new Thread(new ThreadStart(ReadStream));
            thread.Start();
        }
        private void Disconnect()
        {
            if(thread!=null)
                thread.Abort();
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
        public void SendMsg(string msg)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(msg);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();                
        }
        void ReadStream()
        {
            byte[] data = new byte[1024];
            int bytes;
            while ((bytes = stream.Read(data, 0, data.Length)) != 0)
            {
                string responseData = Encoding.Unicode.GetString(data, 0, bytes);
                WriteListBox(responseData);
            }
        }
        void WriteListBox(string data)
        {
            Dispatcher.Invoke(
     DispatcherPriority.Normal,
     new Action(() => listViewHistory.Items.Add(data))
 );

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Disconnect();
        }
    }
}
