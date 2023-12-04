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
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;


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
    }
}
