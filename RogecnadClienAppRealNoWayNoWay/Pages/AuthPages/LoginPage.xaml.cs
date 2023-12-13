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


        private async void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBoxPassword.Password;
            try
            {
                await RegisterLoginModel.SignInAsync(email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            MessageBox.Show("Вы вошли в учетную запись");
            AppManager.mainWindow = null;
            AppManager.mainWindow = new MainWindow();
            AppManager.mainWindow.Show();
            AppManager.authWindow.Hide();
        }
    }
}
