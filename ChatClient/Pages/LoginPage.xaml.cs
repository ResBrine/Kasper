﻿using ChatClient.Misc;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void buttonConfim_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(new MainPage());
        }

        private void textBlockRegistration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Navigation.Navigate(new RegistrationPage());
        }
    }
}
