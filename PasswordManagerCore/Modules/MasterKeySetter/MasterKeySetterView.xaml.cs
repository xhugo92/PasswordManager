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
            var Aux = (MasterKeySetterViewModel)DataContext;
            Aux.SetMasterKey(PasswordBox.SecurePassword);
        }

        private void PasswordChangedEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            if(PasswordBox.Password!=VerificationBox.Password)
            {
                VerificationBox.BorderBrush = System.Windows.Media.Brushes.Red;
                Result.Text = "As duas senhas não são iguais";
                return;
            }
            Result.Text = "";
            VerificationBox.BorderBrush = PasswordBox.BorderBrush;
            OkButton.IsEnabled = true;
        }
    }
}
