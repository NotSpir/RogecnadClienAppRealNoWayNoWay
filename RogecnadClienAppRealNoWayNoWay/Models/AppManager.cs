﻿using Firebase.Auth;
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
        public static ClientUser currentUser = new ClientUser();

        public static MainWindow mainWindow;
        public static AuthorizeWindow authWindow = new AuthorizeWindow();
        public static AudioPlayerWindow audioWindow = new AudioPlayerWindow();

        public static Frame mainFrame = new Frame();

        public static double SongVolume = 1.0;

        public static void FullShutdown()
        {
            Environment.Exit(0);
        }

        public static string selectedTrackID = "what";
    }
}

