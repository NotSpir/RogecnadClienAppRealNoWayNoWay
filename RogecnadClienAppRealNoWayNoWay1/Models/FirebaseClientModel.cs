using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    internal class FirebaseClientModel
    {
        public static IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "cJyLTWfarPN3GTCAqmdFQzJcXBZcIARjg27oazGo",
            BasePath = "https://rogecnad-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        public static IFirebaseClient client;
    }
}
