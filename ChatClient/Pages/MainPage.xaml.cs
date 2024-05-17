﻿using System;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static MainPage Instance { get; set; }
        public ListChatsPage listChatsPage = new ListChatsPage();
        private bool increaseTheSize = false;
        public MainPage()
        {
            InitializeComponent();
            Instance = this;
            frameListChats.Navigate(listChatsPage);
            frameChat.Navigate(new ChatPage());
        }
        Point GetMousePos() => PointFromScreen(MainWindow.Instance.PointToScreen(Mouse.GetPosition(MainWindow.Instance)));
        private void Grid_MouseDown(object sender, MouseEventArgs e)
        {
            increaseTheSize = true;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (increaseTheSize)
            {
                Cursor = Cursors.SizeWE;
                if (GetMousePos().X>200)
                {
                    if (GetMousePos().X > 300)
                        gridColumnListChats.Width = new GridLength(300);
                    else
                    {
                        gridColumnListChats.Width = new GridLength(GetMousePos().X);
                    }

                }
                else
                {
                    if (GetMousePos().X>80)
                    {
                        gridColumnListChats.Width = new GridLength(200);
                    }
                    else
                    {
                        gridColumnListChats.Width = new GridLength(64);

                    }
                }
            }
            else
            {
                Cursor = Cursors.Arrow;

            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            increaseTheSize = false;
        }
    }

}
