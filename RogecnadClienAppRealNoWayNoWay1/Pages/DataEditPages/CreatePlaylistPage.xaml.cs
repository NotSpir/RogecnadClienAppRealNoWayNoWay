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
    /// Логика взаимодействия для CreatePlaylistPage.xaml
    /// </summary>
    public partial class CreatePlaylistPage : Page
    {
        Playlist playlist = null;
        public CreatePlaylistPage()
        {
            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            string err = "";

            if (string.IsNullOrWhiteSpace(GenreNameTextBox.Text))
            {
                err += "Не введено название плейлиста.\n";
            }

            if (err == "")
            {
                if (playlist == null)
                {
                    playlist = new Playlist();
                    playlist.Id = RandomIdGenerator.GeneratePushID();
                    playlist.PlaylistName = GenreNameTextBox.Text;
                    playlist.CreatorId = AppManager.currentUser.Id;
                    FirebaseClientModel.client.Set("Playlists/" + playlist.Id, playlist);
                    MessageBox.Show("Плейлист создан", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
                }

            }
            else MessageBox.Show(err);
        }
    }
}
