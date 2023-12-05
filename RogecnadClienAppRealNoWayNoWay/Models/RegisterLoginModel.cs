using Firebase.Auth;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    internal class RegisterLoginModel
    {

        public static async Task<string> Register(string email, string password, string username)
        {
            
            try
            {
                await FirebaseAuthModel.client.CreateUserWithEmailAndPasswordAsync(email, password, username);
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

        public static async Task<string> SignInAsync(string email, string password)
        {
            var userCredentials = await FirebaseAuthModel.client.SignInWithEmailAndPasswordAsync(email, password);

            return userCredentials.User.GetIdTokenAsync().Result;//.firebaseToken.ToString();
        }

    }
}
