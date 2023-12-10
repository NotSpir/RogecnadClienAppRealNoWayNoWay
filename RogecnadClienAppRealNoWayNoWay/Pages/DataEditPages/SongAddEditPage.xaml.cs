using Google.Apis.Download;
using Microsoft.Win32;
using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// Логика взаимодействия для SongAddEditPage.xaml
    /// </summary>
    public partial class SongAddEditPage : Page
    {
        TimeSpan duration = new TimeSpan();
        TimeSpan currentTime = new TimeSpan(0);

        string mediaFilePath = "";
        string coverImageFilePath = "";
        bool IsPlaying = false;

        SoundTrack soundTrack = new SoundTrack();
        PlayingTracks tracks = new PlayingTracks();
        List<Genre> genreList = new List<Genre>();
        public SongAddEditPage()
        {
            InitializeComponent();
            GetGenreData();
        }

        private void uploadSongButton_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog { Filter = "AudioFile | *.mp3" };
            if (ofd.ShowDialog() == true)
            {
                if (IsPlaying)
                {
                    AudioPlayer.Stop(AudioPlayer.soundPlayer);
                    currentTime = new TimeSpan(0);
                }
                mediaFilePath = ofd.FileName;
                AudioPlayer.soundPlayer = AudioPlayer.InitializePlayer(mediaFilePath);
                duration = AudioPlayer.GetMediaDuration(mediaFilePath, TimeSpan.FromMilliseconds(20000));
                duration = AudioPlayer.StripMilliseconds(duration);
                timeDisplayTextBlock.Text = $"{AudioPlayer.soundPlayer.Position}/{duration}";
                MediaPlayerStack.Visibility = Visibility.Visible;
            }

        }

        private void uploadCoverButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Image | *.png" };
            if (ofd.ShowDialog() == true)
            {
                coverImageFilePath = ofd.FileName;
                imageCover.Source = new BitmapImage(new Uri(coverImageFilePath));
            }
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            string pushID = RandomIdGenerator.GeneratePushID();
            Byte[] bytes = File.ReadAllBytes(mediaFilePath);
            String file = Convert.ToBase64String(bytes);
            tracks.ID = pushID;
            tracks.trackBytes = file;
            FirebaseClientModel.client.Set("TrackFiles/" + tracks.ID, tracks);
            MessageBox.Show("Трек загружен");

            soundTrack.Id = pushID;
            soundTrack.TrackName = TitleTextBox.Text;

            string genreID = genreList.Where(x => x.GenreName == GenreComboBox.Text).FirstOrDefault().Id;
            soundTrack.GenreId = genreID;
            Byte[] bytesCover = File.ReadAllBytes(coverImageFilePath);
            String fileCover = Convert.ToBase64String(bytesCover);
            soundTrack.TrackCoverBytes = fileCover;
            soundTrack.UploaderId = "rvdIy0Rnw4Xq0VWV18aPw5eXHwr1";
            FirebaseClientModel.client.Set("Soundtracks/" + soundTrack.Id, soundTrack);
            MessageBox.Show("Трек загружен");

        }

        private void ExitCompactBtn_Click(object sender, RoutedEventArgs e)
        {
            //It literally does nothing and is not planned.
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (AudioPlayer.soundPlayer.Position >= duration)
            {
                AudioPlayer.soundPlayer.Position = new TimeSpan(0);
                IsPlaying = false;
            }
            if (IsPlaying)
            {
                IsPlaying = false;
                currentTime = AudioPlayer.StripMilliseconds(AudioPlayer.soundPlayer.Position);
                timeDisplayTextBlock.Text = $"{currentTime}/{duration}";
                AudioPlayer.Stop(AudioPlayer.soundPlayer);
            }
            else
            {
                IsPlaying = true;
                AudioPlayer.Play(AudioPlayer.soundPlayer, currentTime);
                ChangeTimeValuesAsync();
            }
            
        }

        private void GetGenreData()
        {
            var result = FirebaseClientModel.client.Get("Genres");
            Dictionary<string, Genre> getGenres = result.ResultAs<Dictionary<string, Genre>>();
            foreach (var item in getGenres)
            {
                var genre = new Genre() { Id = item.Value.Id, GenreName = item.Value.GenreName };
                genreList.Add(genre);
            }
            GenreComboBox.ItemsSource = genreList;
        }

        private void audioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
         }

        private async void ChangeTimeValuesAsync()
        {
            System.Threading.Thread thread = new System.Threading.Thread(async delegate ()
            {
                try
                {
                    while (true)
                    while (IsPlaying)
                    {
                            currentTime = AudioPlayer.StripMilliseconds(AudioPlayer.soundPlayer.Position);
                            timeDisplayTextBlock.Text = $"{currentTime}/{duration}";
                    }
                }
                catch { }
            });
            thread.Start();
        }
    }
}
