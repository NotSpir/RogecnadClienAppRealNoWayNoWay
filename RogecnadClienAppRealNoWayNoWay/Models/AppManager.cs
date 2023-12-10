using Firebase.Auth;
using RogecnadClienAppRealNoWayNoWay.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    internal class AppManager
    {
        public static string token = null;


        public static MainWindow mainWindow = new MainWindow();
        public static AuthorizeWindow authWindow = new AuthorizeWindow();
        public static AudioPlayerWindow audioWindow = new AudioPlayerWindow();

        public static Frame mainFrame = new Frame();

        public static double SongVolume = 10;
    }
}
