using PasswordManagerCore.Services;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManagerCore.Modules
{
    /// <summary>
    /// Interação lógica para LoginView.xam
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            bool Sucess = ((LoginViewModel)DataContext).AuthenticationProcess(EntryPassword.SecurePassword);
            if (!Sucess)
            {
                Result.Foreground = System.Windows.Media.Brushes.Red;
                EntryPassword.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void EnterPressed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SignUp(sender, e);
            }
        }
    }
}
