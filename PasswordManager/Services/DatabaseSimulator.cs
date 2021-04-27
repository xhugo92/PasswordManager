using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services
{
    public class DatabaseSimulator
    {
        public static List<SignInInformations> Server = new List<SignInInformations>
        {
            new SignInInformations("Steam", "Hugofrn1992", "teste", false),
            new SignInInformations("Steam", "Jão", "jao1234", false)
        };

        public static void Add( SignInInformations signInInformations)
        {
            Server.Add(signInInformations);
        }

        public static void Remove(SignInInformations signInInformations)
        {
            Server.Remove(signInInformations);
        }
    }
}
