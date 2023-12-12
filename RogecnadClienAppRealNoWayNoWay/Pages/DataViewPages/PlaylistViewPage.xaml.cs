using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RogecnadClienAppRealNoWayNoWay.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlaylistViewPage.xaml
    /// </summary>
    public partial class PlaylistViewPage : Page
    {
        List<SoundTrack> mainTrackList = new List<SoundTrack>();
        string directory = System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        MediaPlayer soundPlayer = new MediaPlayer();
        bool IsPlaying = false;
        List<Playlist> playlists = new List<Playlist>();
        Playlist activePlaylist = new Playlist();
        public PlaylistViewPage(string playlistID)
        {
            InitializeComponent();
            
            var result = FirebaseClientModel.client.Get("Playlists");
            Dictionary<string, Playlist> getTracks = result.ResultAs<Dictionary<string, Playlist>>();
            foreach (var item in getTracks)
            {
                var val = new Playlist() { Id = item.Value.Id, PlaylistName = item.Value.PlaylistName, CreatorId = item.Value.CreatorId };
                playlists.Add(val);
            }
            activePlaylist = playlists.Where(x => x.Id == playlistID).ToList().FirstOrDefault();
            GetTableData();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            string trackID = ((sender as Button).DataContext as SoundTrack).Id;
            string trackByteString = await GetTrack(trackID);
            byte[] bytes = Convert.FromBase64String(trackByteString);
            string fileName = directory + @"\костыль.mp3";

            try
            {
                // Check if file already exists. If yes, delete it.
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                File.WriteAllBytes(fileName, bytes);
            }
            catch
            {
                MessageBox.Show("Ошибка при загрузке трека", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AudioPlayer.soundPlayer = AudioPlayer.InitializePlayer(fileName);
            AudioPlayer.Play(AudioPlayer.soundPlayer, new TimeSpan(0));
        }

        private void volumeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChannelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetTableData()
        {
            PlaylistDataGetter playlistDataGet = new PlaylistDataGetter() { playlist = activePlaylist};
            List<string> trackIds = playlistDataGet.Tracks;
            playlistFirstCover.Source = playlistDataGet.GetCoverImage;

            List<SoundTrack> soundTracks = new List<SoundTrack>();
            var result = FirebaseClientModel.client.Get("Soundtracks");
            Dictionary<string, SoundTrack> getTracks = result.ResultAs<Dictionary<string, SoundTrack>>();
            if (getTracks != null)
            {
                foreach (var item in getTracks)
                {
                    var val = new SoundTrack() { Id = item.Value.Id, TrackName = item.Value.TrackName, GenreId = item.Value.GenreId, TrackCoverBytes = item.Value.TrackCoverBytes, UploaderId = item.Value.UploaderId, Duration = item.Value.Duration };
                    soundTracks.Add(val);
                }
                
                foreach (var item in trackIds)
                {
                    var val = soundTracks.Where(x => x.Id == item).FirstOrDefault();
                    if (val != null)
                        mainTrackList.Add(val);
                }
                LViewMainSongs.ItemsSource = mainTrackList;
                songCountNameTextBox.Text = playlistDataGet.TrackCount.ToString();
                playlistNameTextBox.Text = activePlaylist.PlaylistName;
            }
        }

        private async Task<string> GetTrack(string ID)
        {
            var result = FirebaseClientModel.client.Get("TrackFiles/" + ID);
            return result.ResultAs<PlayingTracks>().trackBytes;
        }

        private void audioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private async void moreButton_Click(object sender, RoutedEventArgs e)
        {
            Playlist playlist = playlists.FirstOrDefault();
            string trackID = ((sender as Button).DataContext as SoundTrack).Id;
            //Добавить
            FirebaseClientModel.client.Set("TracksPlaylist/" + playlist.Id + "/" + trackID, "");
            MessageBox.Show($"Добавлено в плейлист {playlist.PlaylistName}", "Успех", MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
