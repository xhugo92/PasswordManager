using PasswordManagerCore.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerCore.Services
{
    public static class RandomTextGeneratorService
    {
        public static string GenerateRandomString(int Length, string Chars)
        {
            return new string(Enumerable.Repeat(Chars, Length)
              .Select(s => s[RNGCryptoServiceProvider.GetInt32(s.Length)]).ToArray());
        }

    }
}
