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
            string file = "kzi#PMvrZVd";
            if (File.Exists(file))
            {
                ConfigurationVariables configurationVariables = new ConfigurationVariables();
                XmlSerializer formatter = new XmlSerializer(typeof(ConfigurationVariables));
                string content = File.ReadAllText(file);
                var encodedtext = Convert.FromBase64String(content);
                var plaintext = Encoding.UTF8.GetString(encodedtext);
                byte[] buffer = Encoding.Unicode.GetBytes(plaintext);
                MemoryStream stream = new MemoryStream(buffer);
                configurationVariables = (ConfigurationVariables)formatter.Deserialize(stream);
                MainWindowView.Current.InstanceVariables = configurationVariables;
                return;
            }
            string key = RandomTextGeneratorService.GenerateRandomString(12, PasswordCharConstants.Every);
            MainWindowView.Current.InstanceVariables.CrypthographyKey = key;
        }

        public static void SaveAll()
        {
            MainWindowView.Current.InstanceVariables.IsLogedIn = false;
            string path = "kzi#PMvrZVd";
            XmlSerializer formatter = new XmlSerializer(typeof(ConfigurationVariables));
            StringWriter textWriter = new StringWriter();
            formatter.Serialize(textWriter, MainWindowView.Current.InstanceVariables);
            var plaintext = Encoding.UTF8.GetBytes(textWriter.ToString());
            var encodedtext = Convert.ToBase64String(plaintext);
            File.WriteAllText(path, encodedtext);
        }
    }
}
