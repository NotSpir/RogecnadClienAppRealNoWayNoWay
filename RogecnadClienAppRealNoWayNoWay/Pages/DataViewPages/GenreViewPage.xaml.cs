using Newtonsoft.Json;
using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Pages.DataEditPages;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RogecnadClienAppRealNoWayNoWay.Pages.DataViewPages
{
    /// <summary>
    /// Логика взаимодействия для GenreViewPage.xaml
    /// </summary>
    public partial class GenreViewPage : Page
    {
        List<Genre> genreList = new List<Genre>();
        List<SoundTrack> mainTrackList = new List<SoundTrack>();
        public GenreViewPage()
        {
            InitializeComponent();
            GetTableData();
            LViewData.ItemsSource = genreList;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenreAddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (mainTrackList.Count == 0)
                GetMusicData();
            var dataForRemoval = LViewData.SelectedItems.Cast<Genre>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {dataForRemoval.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var item in dataForRemoval)
                    {
                        var tempList = mainTrackList.Where(x => x.GenreId == item.Id).ToList();
                        if (tempList.Count > 0)
                        {
                            if (MessageBox.Show($"Обнаружены треки с указаным жанром {item.GenreName}. Хотите удалить жанр и все треки с ним?", "Внимание",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                foreach (var track in tempList)
                                {
                                    FirebaseClientModel.client.Delete("Soundtracks/" + track.Id);
                                    FirebaseClientModel.client.Delete("TrackFiles/" + track.Id);
                                }
                                FirebaseClientModel.client.Delete("Genres/" + item.Id);
                            }
                        }
                    }
                    MessageBox.Show("Данные удалены!");
                    GetTableData();
                    LViewData.ItemsSource = genreList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ListViewItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new GenreAddEditPage(((sender as ListViewItem).DataContext as Genre).Id));
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetTableData();
            LViewData.ItemsSource = genreList.Where(x => x.GenreName == TBoxSearch.Text).ToList();
        }

        private void GetTableData()
        {
            genreList.Clear();
            var result = FirebaseClientModel.client.Get("Genres");
            Dictionary<string, Genre> getGenres = result.ResultAs<Dictionary<string, Genre>>();
            foreach (var item in getGenres)
            {
                var genre = new Genre() { Id = item.Value.Id, GenreName = item.Value.GenreName };
                genreList.Add(genre);
            }
        }

        private void GetMusicData()
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
            }
        }
    }
}
