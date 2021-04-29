using PasswordManagerCore.Database;
using System.Windows;

namespace PasswordManagerCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DatabaseContext = new DatabaseContext();
        }
        public static DatabaseContext DatabaseContext { get; private set; }
    }
}
