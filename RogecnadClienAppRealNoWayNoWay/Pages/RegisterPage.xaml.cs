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
            string password = PasswordTextBox.Password;
            string err = "";
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

            string status = Register(email, password, username).Result;
            MessageBox.Show(status);
            if (status == "Учетная запись успешно создана")
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        async Task<string> Register(string email, string password, string username)
        {
            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(AppManager.API_KEY));
            try
            {

                FirebaseAuthLink firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, password, username);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                    return "Учетная запись с данной почтой уже существует";
                else
                    return "Что-то пошло не так. Забейте того, кто писал бэкенд тапком, если этот придурок умудрился неверно написать регистрацию.";
            }
            return "Учетная запись успешно создана";
        }



        bool IsCorrectPassword(string text)
        {
            if ((new Regex("[A-Z]+")).IsMatch(text) && (new Regex("[a-z]+")).IsMatch(text) && (new Regex("[0-9]+")).IsMatch(text) && text.Length >= 8)
                return true;
            return false;
        }


    }
}
