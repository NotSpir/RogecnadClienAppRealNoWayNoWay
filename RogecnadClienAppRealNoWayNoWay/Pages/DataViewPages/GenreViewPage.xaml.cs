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
        public GenreViewPage()
        {
            InitializeComponent();
            GetTableData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenreAddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new GenreAddEditPage(((sender as ListViewItem).DataContext as Genre).Id));
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GetTableData()
        {
            var result = FirebaseClientModel.client.Get("Genres");
            Dictionary<string, Genre> getGenres = result.ResultAs<Dictionary<string, Genre>>();
            foreach (var item in getGenres)
            {
                var genre = new Genre() { Id = item.Value.Id, GenreName = item.Value.GenreName };
                genreList.Add(genre);
            }
            LViewData.ItemsSource = genreList;
        }
    }
}
