using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Models;
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
using RogecnadClienAppRealNoWayNoWay.Models.DataModels;
using System.Media;
using System.IO;
using RogecnadClienAppRealNoWayNoWay.Pages.DataViewPages;

namespace RogecnadClienAppRealNoWayNoWay.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<SoundTrack> mainTrackList = new List<SoundTrack>();
        string directory = System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        MediaPlayer soundPlayer = new MediaPlayer();
        bool IsPlaying = false;
        List<Playlist> playlists = new List<Playlist>();
        public MainPage()
        {
            InitializeComponent();
            GetTableData();
            var result = FirebaseClientModel.client.Get("Playlists");
            Dictionary<string, Playlist> getTracks = result.ResultAs<Dictionary<string, Playlist>>();
            foreach (var item in getTracks)
            {
                var val = new Playlist() { Id = item.Value.Id, PlaylistName = item.Value.PlaylistName, CreatorId = item.Value.CreatorId };
                playlists.Add(val);
            }
            playlists = playlists.Where(x => x.CreatorId == AppManager.currentUser.Id).ToList();
    }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            AppManager.audioWindow.ReloadPlayer(((sender as Button).DataContext as SoundTrack).Id);
        }

        private void volumeButton_Click(object sender, RoutedEventArgs e)
        {
                Button sentItem = sender as Button;
                ContextMenu contextMenu = sentItem.ContextMenu;
                contextMenu.PlacementTarget = sentItem;
                contextMenu.IsOpen = true;   

                e.Handled = true;
        }

        private void ChannelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChannelContentView(((sender as Button).DataContext as SoundTrack).UploaderId));
        }

        private void GetTableData()
        {
            var result = FirebaseClientModel.client.Get("Soundtracks");


            Dictionary<string, SoundTrack> getTracks = result.ResultAs<Dictionary<string, SoundTrack>>();
            if (getTracks != null)
            {
                foreach (var item in getTracks)
                {
                    var val = new SoundTrack() { Id = item.Value.Id, TrackName = item.Value.TrackName, GenreId = item.Value.GenreId, TrackCoverBytes = item.Value.TrackCoverBytes, UploaderId = item.Value.UploaderId, Duration = item.Value.Duration };
                    mainTrackList.Add(val);
                }
                LViewMainSongs.ItemsSource = mainTrackList;
            }
        }

        private async Task<string> GetTrack(string ID)
        {
            var result = FirebaseClientModel.client.Get("TrackFiles/" + ID);
            return result.ResultAs<PlayingTracks>().trackBytes;
        }

        private async void moreButton_Click(object sender, RoutedEventArgs e)
        {
            Button sentItem = sender as Button;
            AppManager.selectedTrackID = (sentItem.DataContext as SoundTrack).Id;
            ContextMenu contextMenu = sentItem.ContextMenu;
            contextMenu.PlacementTarget = sentItem;
            contextMenu.IsOpen = true;
            MenuItem menuItem = contextMenu.Items[0] as MenuItem;

            e.Handled = true;
        }

        private void OnOpened(object sender, RoutedEventArgs e)
        {

        }

        private void OnClosed(object sender, RoutedEventArgs e)
        {

        }
    }
}
