using RogecnadClienAppRealNoWayNoWay.Models;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using RogecnadClienAppRealNoWayNoWay.Pages;
using RogecnadClienAppRealNoWayNoWay.Pages.DataEditPages;
using RogecnadClienAppRealNoWayNoWay.Pages.DataViewPages;
using RogecnadClienAppRealNoWayNoWay.Windows;
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
using System.Windows.Threading;

namespace RogecnadClienAppRealNoWayNoWay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PlaylistDataGetter> playlistsData = new List<PlaylistDataGetter>();


        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        public MainWindow()
        {
            InitializeComponent();
            if (AppManager.currentUser.Id != null)
            {
                RegisterAndAuthorizeStack.Visibility = Visibility.Collapsed;
                UserStack.Visibility = Visibility.Visible;
                if (AppManager.currentUser.Role == "admin")
                {
                    ViewGenresButton.Visibility = Visibility.Visible;
                    usernameTB.Text = "[Администратор] " + AppManager.currentUser.Login;
                }
                else
                    usernameTB.Text = AppManager.currentUser.Login;
            }
            if (AppManager.currentUser.Id == null)
            {
                PlaylistStack.Visibility = Visibility.Collapsed;
            }
            AppManager.mainWindow = this;
            GetTableData();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;

            AppManager.mainWindow = this;
            AppManager.mainFrame = this.MainFrame;
            AppManager.mainFrame.Navigate(new MainPage());
            AppManager.audioWindow = new AudioPlayerWindow();
            panelWidth = sidePanel.Width;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (hidden)
            {
                sidePanel.Width += 2;
                if (sidePanel.Width >= panelWidth)
                {
                    timer.Stop();
                    ExpandShortBtn.IsEnabled = true;
                    IconExpandImage.Source = new BitmapImage(new Uri(@"/Resources/CircledArrowLeft.png", UriKind.Relative));
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= 2;
                if (sidePanel.Width <= 100)
                {
                    timer.Stop();
                    ExpandShortBtn.IsEnabled = true;
                    IconExpandImage.Source = new BitmapImage(new Uri(@"/Resources/CircledArrowRight.png", UriKind.Relative));
                    hidden = true;
                }
            }
        }


        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ExpandShortBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizeWindow authWindow = new AuthorizeWindow();
            authWindow.SillyFrame.Navigate(new LoginPage());
            authWindow.Show();
            this.Hide();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizeWindow authWindow = new AuthorizeWindow();
            authWindow.SillyFrame.Navigate(new RegisterPage());
            authWindow.Show();
            this.Hide();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            AppManager.mainFrame.Navigate(new SongSearchPage(srcBarTextBox.Text));
        }

        private void ChartsButton_Click(object sender, RoutedEventArgs e)
        {
            AppManager.mainFrame.Navigate(new SongAddEditPage());
        }

        private void MainPageButton_Click(object sender, RoutedEventArgs e)
        {
            AppManager.mainFrame.Navigate(new MainPage());
        }

        public void GetTableData()
        {
            playlistsData.Clear();
            List<Playlist> playlists = new List<Playlist>();
            var result = FirebaseClientModel.client.Get("Playlists");
            Dictionary<string, Playlist> getTracks = result.ResultAs<Dictionary<string, Playlist>>();
            foreach (var item in getTracks)
            {
                var val = new Playlist() { Id = item.Value.Id, PlaylistName = item.Value.PlaylistName, CreatorId = item.Value.CreatorId };
                playlists.Add(val);
            }
            
            foreach (var item in playlists)
            {
                if (item.CreatorId == AppManager.currentUser.Id)
                {
                    var val = new PlaylistDataGetter() { playlist = item };
                    playlistsData.Add(val);
                }
                
            }
            playlistsListView.ItemsSource = playlistsData;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppManager.FullShutdown();
        }

        private void CreatePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            AppManager.mainFrame.Navigate(new CreatePlaylistPage());
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a = ((sender as ListViewItem).DataContext as PlaylistDataGetter).playlist.Id;
            AppManager.mainFrame.Navigate(new PlaylistViewPage(a));
        }

        private void RefreshPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            GetTableData();
            playlistsListView.Items.Refresh();
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            FirebaseAuthModel.client.SignOut();
            Properties.Settings.Default.Reset();
            AppManager.currentUser = new ClientUser();
            AppManager.mainWindow.Hide();
            AppManager.mainWindow = null;
            AppManager.mainWindow = new MainWindow();
            AppManager.mainWindow.Show();
        }

        private void MyChannelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewGenresButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GenreViewPage());
        }
    }
}
