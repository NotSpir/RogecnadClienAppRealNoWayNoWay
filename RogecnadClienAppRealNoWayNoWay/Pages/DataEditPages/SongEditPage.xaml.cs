using Microsoft.Win32;
using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Models.DataModels;
using RogecnadClienAppRealNoWayNoWay.Pages.DataViewPages;
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

namespace RogecnadClienAppRealNoWayNoWay.Pages.DataEditPages
{
    /// <summary>
    /// Логика взаимодействия для SongEditPage.xaml
    /// </summary>
    public partial class SongEditPage : Page
    {
        SoundTrack soundTrack = new SoundTrack();
        List<Genre> genreList = new List<Genre>();
        string coverImageFilePath = "";
        public SongEditPage(string trackID)
        {
            InitializeComponent();
            var result = FirebaseClientModel.client.Get("Soundtracks/" + trackID);
            soundTrack = result.ResultAs<SoundTrack>();
            TitleTextBox.Text = soundTrack.TrackName;
            imageCover.Source = soundTrack.GetCoverImage;
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
                if (coverImageFilePath != "")
                {
                    Byte[] bytesCover = File.ReadAllBytes(coverImageFilePath);
                    String fileCover = Convert.ToBase64String(bytesCover);
                    soundTrack.TrackCoverBytes = fileCover;
                }

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

        private void uploadCoverButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Image | *.png; *.jpg; *.jpeg" };
            if (ofd.ShowDialog() == true)
            {
                coverImageFilePath = ofd.FileName;
                imageCover.Source = new BitmapImage(new Uri(coverImageFilePath));
                CheckReady();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удалить трек?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    FirebaseClientModel.client.Delete("Soundtracks/" + soundTrack.Id);
                    FirebaseClientModel.client.Delete("TrackFiles/" + soundTrack.Id);
                    MessageBox.Show("Трек удален!");
                    NavigationService.Navigate(new ChannelContentView(AppManager.currentUser.Id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
