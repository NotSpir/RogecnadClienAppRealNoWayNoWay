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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RogecnadClienAppRealNoWayNoWay.Windows
{
    /// <summary>
    /// Логика взаимодействия для AudioPlayerWindow.xaml
    /// </summary>
    public partial class AudioPlayerWindow : Window
    {
        string directory = System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        TimeSpan duration = new TimeSpan();
        TimeSpan currentTime = new TimeSpan(0);
        bool IsPlaying = false;
        string currentTrackID = "";
        public AudioPlayerWindow()
        {
            InitializeComponent();
        }

        private void audioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ChannelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = directory + @"\костыль.mp3";

            if (AudioPlayer.soundPlayer.Position >= duration)
            {
                AudioPlayer.soundPlayer.Position = new TimeSpan(0);
                IsPlaying = false;
            }
            if (IsPlaying)
            {
                IsPlaying = false;
                imagePlayButton.Source = new BitmapImage(new Uri(@"/Resources/PlayButton.png", UriKind.Relative));
                currentTime = AudioPlayer.StripMilliseconds(AudioPlayer.soundPlayer.Position);
                timeDisplayTextBlock.Text = $"{currentTime}/{duration}";
                AudioPlayer.Stop(AudioPlayer.soundPlayer);
            }
            else
            {
                IsPlaying = true;
                imagePlayButton.Source = new BitmapImage(new Uri(@"/Resources/StopButton.png", UriKind.Relative));
                AudioPlayer.Play(AudioPlayer.soundPlayer, currentTime);
            }
        }

        private void volumeButton_Click(object sender, RoutedEventArgs e)
        {
            Button sentItem = sender as Button;
            ContextMenu contextMenu = sentItem.ContextMenu;
            contextMenu.PlacementTarget = sentItem;
            contextMenu.IsOpen = true;

            e.Handled = true;
        }

        private void moreButton_Click(object sender, RoutedEventArgs e)
        {
            Button sentItem = sender as Button;
            AppManager.selectedTrackID = currentTrackID;
            ContextMenu contextMenu = sentItem.ContextMenu;
            contextMenu.PlacementTarget = sentItem;
            contextMenu.IsOpen = true;
            MenuItem menuItem = contextMenu.Items[0] as MenuItem;

            e.Handled = true;
        }

        private async Task<string> GetTrack(string ID)
        {
            var result = FirebaseClientModel.client.Get("TrackFiles\\" + ID);
            return result.ResultAs<PlayingTracks>().trackBytes;
        }

        private async Task<BitmapImage> GetCover(string ID)
        {
            var result = FirebaseClientModel.client.Get("Soundtracks\\" + ID);
            return result.ResultAs<SoundTrack>().GetCoverImage;
        }

        public async void ReloadPlayer(string trackID)
        {
            AppManager.audioWindow.Hide();
            currentTrackID = trackID;
            imageCover.Source = await GetCover(trackID);
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
            AppManager.audioWindow.Show();
            AudioPlayer.soundPlayer = AudioPlayer.InitializePlayer(fileName);
            AudioPlayer.Play(AudioPlayer.soundPlayer, new TimeSpan(0));
        }

        private void OnOpened(object sender, RoutedEventArgs e)
        {

        }

        private void OnClosed(object sender, RoutedEventArgs e)
        {

        }
    }
}
