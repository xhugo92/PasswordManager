using System.Windows.Controls;

namespace PasswordManagerCore.Modules
{
    /// <summary>
    /// Interação lógica para MasterKeySetterView.xam
    /// </summary>
    public partial class MasterKeySetterView : UserControl
    {
        public MasterKeySetterView()
        {
            InitializeComponent();
        }

        private void SetMasterKeyEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            ((MasterKeySetterViewModel)DataContext).SetMasterKey(PasswordBox.SecurePassword);
        }

        private void PasswordChangedEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            if(PasswordBox.Password!=VerificationBox.Password)
            {
                VerificationBox.BorderBrush = System.Windows.Media.Brushes.Red;
                PasswordBox.BorderBrush = System.Windows.Media.Brushes.Red;
                Result.Foreground = System.Windows.Media.Brushes.Red;
                ((MasterKeySetterViewModel)DataContext).ChangeOkButtonAvaliability(false);
                ((MasterKeySetterViewModel)DataContext).ChangeResultText("As duas senhas não são iguais");
                return;
            }
            VerificationBox.BorderBrush = System.Windows.Media.Brushes.Green;
            PasswordBox.BorderBrush = System.Windows.Media.Brushes.Green;
            Result.Foreground = System.Windows.Media.Brushes.Green;
            VerificationBox.BorderBrush = PasswordBox.BorderBrush;
            ((MasterKeySetterViewModel)DataContext).ChangeResultText("As senhas são iguais");
            ((MasterKeySetterViewModel)DataContext).ChangeOkButtonAvaliability(true);
        }
    }
}
