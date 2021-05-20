using PasswordManagerCore.Constants;
using PasswordManagerCore.Resources;
using System.Linq;
using System.Security.Cryptography;

namespace PasswordManagerCore.Services
{
    public static class StartupService
    {
        public static void Startup()
        {
            string key;
            try
            {
                string[] lines = System.IO.File.ReadAllLines("config");
                key = lines[0];
            }
            catch
            {
                key = new string(Enumerable.Repeat(PasswordCharConstants.chars, 12)
              .Select(s => s[RNGCryptoServiceProvider.GetInt32(s.Length)]).ToArray());
            }
            ConfigurationVariables.CrypthographyKey = key;
        }
    }
}
