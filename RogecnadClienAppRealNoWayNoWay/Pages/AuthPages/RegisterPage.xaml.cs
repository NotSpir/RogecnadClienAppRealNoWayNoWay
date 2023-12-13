using Firebase.Auth;
using RogecnadClienAppRealNoWayNoWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBoxPass.Password;
            string err = "";
            if (string.IsNullOrWhiteSpace(username))
            {
                err += "Не введен логин!\n";
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                err += "Не введена почта!\n";
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                err += "Не введен пароль!\n";
            }
            if (!email.Contains('@'))
            {
                err += "Введена ненастоящая почта!\n";
            }
            if (!IsCorrectPassword(password))
                err += ("Пароль должен быть длинной минимум 8 символов и содержать минимум 1 заглавную букву, 1 строчную и 1 цифру");
            if (err.Length > 0)
            {
                MessageBox.Show(err);
                return;
            }

            try
            {
                RegisterLoginModel.Register(email, password, username);
                MessageBox.Show("Регистрация успешно выполнена");
            }
            catch
            {
                MessageBox.Show("Аккаунт с данной почтой уже существует!");
            }


            NavigationService.Navigate(new LoginPage());
            
        }

        bool IsCorrectPassword(string text)
        {
            if ((new Regex("[A-Z]+")).IsMatch(text) && (new Regex("[a-z]+")).IsMatch(text) && (new Regex("[0-9]+")).IsMatch(text) && text.Length >= 8)
                return true;
            return false;
        }

        private void DisplayPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Visibility == Visibility.Visible)
            {
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBoxPass.Visibility = Visibility.Visible;
                PasswordHideShowImage.Source = new BitmapImage(new Uri(@"/Resources/ClosedEyeIcon.png", UriKind.Relative));
            }
            else if (PasswordTextBox.Visibility == Visibility.Collapsed)
            {
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordBoxPass.Visibility = Visibility.Collapsed;
                PasswordHideShowImage.Source = new BitmapImage(new Uri(@"/Resources/EyeIcon.png", UriKind.Relative));
            }
        }

        private void PasswordBoxPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBoxPass.Password;
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordBoxPass.Password = PasswordTextBox.Text;
        }
    }
}
