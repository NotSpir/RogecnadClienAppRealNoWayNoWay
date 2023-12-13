using FireSharp;
using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Pages.DataViewPages;
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

namespace RogecnadClienAppRealNoWayNoWay.Pages.DataEditPages
{
    /// <summary>
    /// Логика взаимодействия для GenreAddEditPage.xaml
    /// </summary>
    public partial class GenreAddEditPage : Page
    {
        Genre genre = null;
        public GenreAddEditPage(string ID)
        {
            if (ID != null)
            {
                var result = FirebaseClientModel.client.Get("Genres/" + ID);
                genre = result.ResultAs<Genre>();
            }

            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            if (string.IsNullOrWhiteSpace(GenreNameTextBox.Text))
            {
                err += "Не введено название жанра.\n";
            }

            if (err == "")
            {
                if (genre == null)
                {
                    genre = new Genre();
                    genre.Id = RandomIdGenerator.GeneratePushID();
                    genre.GenreName = GenreNameTextBox.Text;
                    FirebaseClientModel.client.Set("Genres/" + genre.Id, genre);
                    MessageBox.Show("Успешно добавлен жанр.", "Успех");
                    NavigationService.Navigate(new GenreViewPage());
                }
                else
                {
                    genre.GenreName = GenreNameTextBox.Text;
                    FirebaseClientModel.client.Set("Genres/" + genre.Id, genre);
                    MessageBox.Show("Успешно изменен жанр.", "Успех");
                    NavigationService.Navigate(new GenreViewPage());
                }

            }
            else MessageBox.Show(err);
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
