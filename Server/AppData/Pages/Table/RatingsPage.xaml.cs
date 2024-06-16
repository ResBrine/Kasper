using Server.AppData.DataBase;
using Server.AppData.Pages.AddEditPages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pages.Table
{
    /// <summary>
    /// Логика взаимодействия для RatingsPage.xaml
    /// </summary>
    public partial class RatingsPage : Page
    {
        public static RatingsPage Instance { get; private set; }
        public RatingsPage()
        {
            InitializeComponent();
            Instance = this;
        }
        public void UpduteList()
        {
            listView.ItemsSource = Connect.GetRatings.ToList();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditRatingWindow addEditRatingWindow = new AddEditRatingWindow(null);
            addEditRatingWindow.ShowDialog();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditRatingWindow addEditRatingWindow = new AddEditRatingWindow((sender as Button).DataContext as Ratings);
            addEditRatingWindow.ShowDialog();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {

            var deleteRatings = listView.SelectedItems.Cast<Ratings>().ToList();
            if (MessageBox.Show($"Удалить {deleteRatings.Count} запсией", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                foreach (var deleteRating in deleteRatings)
                {
                    var listUsers = Connect.GetUsers;
                    bool isAllowDelet = true;
                    foreach (var user in listUsers)
                        if (user.rating == deleteRating.idRating)
                        {
                            MessageBox.Show($"Нельзя удалить рейтинг \"{deleteRating.name}\" так как его использует {user.userName}");
                            isAllowDelet = false;
                            break;
                        }
                    if (isAllowDelet)
                        Connect.GetRatings.Remove(deleteRating);
                }
            try
            {
                Connect.Context.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpduteList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpduteList();
        }

        private void Page_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpduteList();
        }
    }
}
