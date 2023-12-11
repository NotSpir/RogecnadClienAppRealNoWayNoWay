using RogecnadClienAppRealNoWayNoWay.Models;
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
        public AudioPlayerWindow()
        {
            InitializeComponent();
            AppManager.audioWindow = this;
        }

        private void audioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ChannelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            string trackID = "a";
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

        private void moreButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async Task<string> GetTrack(string ID)
        {
            var result = FirebaseClientModel.client.Get("TrackFiles\\" + ID);
            return result.ResultAs<PlayingTracks>().trackBytes;
        }
    }
}
