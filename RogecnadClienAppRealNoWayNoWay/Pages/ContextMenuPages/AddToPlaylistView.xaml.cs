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

namespace RogecnadClienAppRealNoWayNoWay.Pages.ContextMenuPages
{
    /// <summary>
    /// Логика взаимодействия для AddToPlaylistView.xaml
    /// </summary>
    public partial class AddToPlaylistView : Page
    {
        List<Playlist> playlists = new List<Playlist>();
        string trackID = string.Empty;
        public AddToPlaylistView()
        {
            InitializeComponent();
            trackID = AppManager.selectedTrackID;
            var result = FirebaseClientModel.client.Get("Playlists");
            Dictionary<string, Playlist> getTracks = result.ResultAs<Dictionary<string, Playlist>>();
            foreach (var item in getTracks)
            {
                var val = new Playlist() { Id = item.Value.Id, PlaylistName = item.Value.PlaylistName, CreatorId = item.Value.CreatorId };
                playlists.Add(val);
            }
            playlists = playlists.Where(x => x.CreatorId == AppManager.currentUser.Id).ToList();
            LViewData.ItemsSource = playlists;
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Playlist playlist = (sender as ListViewItem).DataContext as Playlist;
            FirebaseClientModel.client.Set("TracksPlaylist/" + playlist.Id + "/" + trackID, "");
            MessageBox.Show($"Добавлено в плейлист {playlist.PlaylistName}", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            trackID = AppManager.selectedTrackID;
        }
    }
}
