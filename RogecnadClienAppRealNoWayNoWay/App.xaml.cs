using Firebase.Auth;
using Firebase.Auth.Providers;
using FireSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RogecnadClienAppRealNoWayNoWay.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RogecnadClienAppRealNoWayNoWay
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, serviceCollection) =>
                {
                    string firebaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");

                    serviceCollection.AddSingleton<MainWindow>((services) => new MainWindow());
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {

            try
            {
                FirebaseClientModel.client = new FireSharp.FirebaseClient(FirebaseClientModel.firebaseConfig);
            }
            catch
            {
                MessageBox.Show("Отсутствует подключение к интернету. Попробуйте зайти позже");
                Application.Current.Shutdown();
            }
            try
            {
                await RegisterLoginModel.SignInOnStart();
            }
            catch
            {

            }
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
