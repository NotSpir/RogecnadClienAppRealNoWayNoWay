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
        public const string API_KEY = "AIzaSyA5Xnx8ofgxs8VHQ6IM8FJnhX-7MWCXdVg";
        public static string token = null;


        public static MainWindow mainWindow = new MainWindow();
        public static AuthorizeWindow authWindow = new AuthorizeWindow();
        public static AudioPlayerWindow audioWindow = new AudioPlayerWindow();

        public static Frame mainFrame = new Frame();
    }
}
