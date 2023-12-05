using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    internal class FirebaseAuthModel
    {
        public static FirebaseAuthClient client = InitializeClient();
        private static FirebaseAuthClient InitializeClient()
        {
            FirebaseAuthConfig config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyA5Xnx8ofgxs8VHQ6IM8FJnhX-7MWCXdVg",
                AuthDomain = "<DOMAIN>.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    // Add and configure individual providers
                    new EmailProvider()
                },
                // WPF:
                UserRepository = new FileUserRepository("FirebaseSample")
            };

            FirebaseAuthClient client = new FirebaseAuthClient(config);
            return client;
        }
    }
}
