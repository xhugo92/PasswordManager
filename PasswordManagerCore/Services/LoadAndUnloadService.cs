using PasswordManagerCore.Constants;
using PasswordManagerCore.Resources;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
using PasswordManagerCore.Modules;
using System.Text;
using System;

namespace PasswordManagerCore.Services
{
    public static class LoadAndUnloadService
    {
        public static void Startup()
        {
            try
            {
                string file = "config";
                ConfigurationVariables configurationVariables = new ConfigurationVariables();
                XmlSerializer formatter = new XmlSerializer(typeof(ConfigurationVariables));
                //FileStream aFile = new FileStream(file, FileMode.Open);
                //byte[] buffer = new byte[aFile.Length];
                //aFile.Read(buffer, 0, (int)aFile.Length);
                string content = File.ReadAllText(file);
                var encodedtext = Convert.FromBase64String(content);
                var plaintext = Encoding.UTF8.GetString(encodedtext);
                byte[] buffer = Encoding.Unicode.GetBytes(plaintext);
                MemoryStream stream = new MemoryStream(buffer);
                configurationVariables = (ConfigurationVariables)formatter.Deserialize(stream);
                MainWindowView.Current.InstanceVariables.CrypthographyKey = configurationVariables.CrypthographyKey;
                MainWindowView.Current.InstanceVariables.HasPasswordSetted = configurationVariables.HasPasswordSetted;
                if(configurationVariables.HasPasswordSetted)
                {
                    MainWindowView.Current.InstanceVariables.EncryptedPassword = configurationVariables.EncryptedPassword;
                }

            }
            catch (FileNotFoundException FNFE)
            {
                string key = new string(Enumerable.Repeat(PasswordCharConstants.chars, 12)
              .Select(s => s[RNGCryptoServiceProvider.GetInt32(s.Length)]).ToArray());
                MainWindowView.Current.InstanceVariables.CrypthographyKey = key;
            }
        }

        public static void SaveAll()
        {
            string path = "config";
            XmlSerializer formatter = new XmlSerializer(typeof(ConfigurationVariables));
            StringWriter textWriter = new StringWriter();
            formatter.Serialize(textWriter, MainWindowView.Current.InstanceVariables);
            var plaintext = Encoding.UTF8.GetBytes(textWriter.ToString());
            var encodedtext = Convert.ToBase64String(plaintext);
            File.WriteAllText(path, encodedtext);
        }
    }
}
