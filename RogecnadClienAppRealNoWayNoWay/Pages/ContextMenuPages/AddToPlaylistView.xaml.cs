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

namespace RogecnadClienAppRealNoWayNoWay.Pages.ContextMenuPages
{
    /// <summary>
    /// Логика взаимодействия для AddToPlaylistView.xaml
    /// </summary>
    public partial class AddToPlaylistView : Page
    {
        public AddToPlaylistView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Херак по кнопке - и в ссылку в выбранный плейлист отправят
        }
    }
}
