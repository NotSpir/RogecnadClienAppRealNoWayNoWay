using Microsoft.Win32;
using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Models.DataModels;
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
    /// Логика взаимодействия для SongEditPage.xaml
    /// </summary>
    public partial class SongEditPage : Page
    {
        SoundTrack soundTrack = new SoundTrack();
        List<Genre> genreList = new List<Genre>();
        public SongEditPage(string trackID)
        {
            InitializeComponent();
            var result = FirebaseClientModel.client.Get("Soundtracks/" + trackID);
            soundTrack = result.ResultAs<SoundTrack>();
            TitleTextBox.Text = soundTrack.TrackName;
            GetGenreData();
            CheckReady();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                soundTrack.TrackName = TitleTextBox.Text;
                string genreID = genreList.Where(x => x.GenreName == GenreComboBox.Text).FirstOrDefault().Id;
                soundTrack.GenreId = genreID;

                FirebaseClientModel.client.Set("Soundtracks/" + soundTrack.Id, soundTrack);
                MessageBox.Show("Трек загружен");
                NavigationService.Navigate(new ChannelContentView(AppManager.currentUser.Id));
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так при загрузке аудио", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            GenreComboBox.Text = genreList.Where(x => x.Id == soundTrack.GenreId).FirstOrDefault().GenreName;
        }

        private void CheckReady()
        {
            if (!string.IsNullOrWhiteSpace(TitleTextBox.Text) && !string.IsNullOrWhiteSpace(GenreComboBox.Text))
                UploadButton.IsEnabled = true;
        }
        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckReady();
        }

        private void GenreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckReady();
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
