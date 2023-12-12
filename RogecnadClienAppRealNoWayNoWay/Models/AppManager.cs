using Firebase.Auth;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
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
        public static ClientUser currentUser = new ClientUser() { Id = "rvdIy0Rnw4Xq0VWV18aPw5eXHwr1" };

        public static MainWindow mainWindow = new MainWindow();
        public static AuthorizeWindow authWindow = new AuthorizeWindow();
        public static AudioPlayerWindow audioWindow = new AudioPlayerWindow();

        public static Frame mainFrame = new Frame();

        public static double SongVolume = 1.0;

        public static void FullShutdown()
        {
            Environment.Exit(0);
        }
    }
}

