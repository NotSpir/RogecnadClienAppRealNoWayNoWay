using Firebase.Auth;
using Firebase.Auth.Providers;
using FireSharp.Config;
using Microsoft.Extensions.Hosting;
using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    internal class RegisterLoginModel
    {
        static string directory = System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        public static async Task Register(string email, string password, string username)
        {

            try
            {
                var authProvider = FirebaseAuthModel.client;
                var auth = authProvider.CreateUserWithEmailAndPasswordAsync(email, password, username);
                var return_data = await auth;
                var UserID = return_data.User.Uid;
                var user = new ClientUser() { Id = UserID, Email = email, Login = username, Role = "user" };
                FirebaseClientModel.client.Set("Users/" + user.Id, user);
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Учетная запись с данной почтой уже существует", "Ошибка");
            }
        }

        public static async Task SignInAsync(string email, string password)
        {
            var authProvider = FirebaseAuthModel.client;
            var auth = authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var return_data = await auth;

            Properties.Settings.Default.email = email;
            Properties.Settings.Default.password = password;
            Properties.Settings.Default.Save();

            var result = FirebaseClientModel.client.Get("Users/" + return_data.User.Uid);
            AppManager.currentUser = result.ResultAs<ClientUser>();
        }

        public static async Task SignInOnStart()
        {
            string email = Properties.Settings.Default.email;
            string password = Properties.Settings.Default.password;
            var authProvider = FirebaseAuthModel.client;
            var auth = authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var return_data = await auth;

            var result = FirebaseClientModel.client.Get("Users/" + return_data.User.Uid);
            AppManager.currentUser = result.ResultAs<ClientUser>();

        }
    }
}
