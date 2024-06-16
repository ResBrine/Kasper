using Server.AppData.DataBase;
using System;
using System.Windows;

namespace Server.AppData.Pages.AddEditPages
{
    /// <summary>
    /// Логика взаимодействия для AddEditRatingWindow.xaml
    /// </summary>
    public partial class AddEditRatingWindow : Window
    {
        Ratings rating;
        bool checkNew;
        public AddEditRatingWindow(Ratings c)
        {
            InitializeComponent();
            if (c == null)
            {
                c = new Ratings();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = rating = c;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
                Connect.GetRatings.Add(rating);
            try
            {
                Connect.Context.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

