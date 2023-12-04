using Firebase.Auth;
using Newtonsoft.Json;
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

namespace RogecnadClienAppRealNoWayNoWay.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        async Task<string> SignInAsync(string email, string password)
        {
            try
            {
                FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(AppManager.API_KEY));

                FirebaseAuthLink firebaseAuthLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);

                return firebaseAuthLink.FirebaseToken.ToString();
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так при аутентификации. Повторите попытку позже");
                return null;
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBoxPassword.Password;
            try
            {
                AppManager.token = SignInAsync(email, password).Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пользователь не найден");
            }
            if (AppManager.token == null)
            {
                MessageBox.Show("Пользователь не найден");
            }
            else
            {
                MessageBox.Show("Вы вошли в учетную запись");
                AppManager.mainWindow.Show();
                AppManager.authWindow.Hide();
                
            }
                
        }
    }
}
