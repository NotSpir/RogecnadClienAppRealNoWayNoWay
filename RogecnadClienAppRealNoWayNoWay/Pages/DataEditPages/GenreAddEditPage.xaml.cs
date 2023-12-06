using FireSharp;
using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
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
                Console.WriteLine("");
            }

            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            string err = "";

            if (genre == null && string.IsNullOrWhiteSpace(GenreIDTextBox.Text))
            {
                err += "Не введен идентификатор (пример: example_id).\n";
            }
            if (string.IsNullOrWhiteSpace(GenreNameTextBox.Text))
            {
                err += "Не введено название жанра.\n";
            }

            if (err == "")
            {
                if (genre == null)
                {
                    genre = new Genre();
                    genre.Id = GenreIDTextBox.Text;
                    genre.GenreName = GenreNameTextBox.Text;
                    FirebaseClientModel.client.Set("Genres/" + genre.Id, genre);
                }
                else
                {
                    //редактирование
                }

            }
            else MessageBox.Show(err);
        }
    }
}
