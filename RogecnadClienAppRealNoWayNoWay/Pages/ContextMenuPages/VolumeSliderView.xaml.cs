using RogecnadClienAppRealNoWayNoWay.Models;
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
    /// Логика взаимодействия для VolumeSliderView.xaml
    /// </summary>
    public partial class VolumeSliderView : Page
    {
        public VolumeSliderView()
        {
            InitializeComponent();
            sliderVolume.Value = AppManager.SongVolume;

        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AppManager.SongVolume = sliderVolume.Value;
            AudioPlayer.changeVolume(AudioPlayer.soundPlayer, AppManager.SongVolume);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sliderVolume.Value = AppManager.SongVolume;
        }
    }
}
