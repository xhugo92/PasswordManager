using PasswordManagerCore.Resources;
using System.Text;

namespace PasswordManagerCore.Services
{
    public static class StartupService
    {
        public static void Startup()
        {
            byte[] lines = System.IO.File.ReadAllBytes("config.cfg");
            ConfigurationVariables.CrypthographyKey = Encoding.ASCII.GetString(lines);
        }
    }
}
