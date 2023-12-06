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
        public GenreViewPage()
        {
            InitializeComponent();
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

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
