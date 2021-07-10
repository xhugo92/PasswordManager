using MvvmHelpers;
using PasswordManagerCore.Resources;
using PasswordManagerCore.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class EditCryptographyKeyViewModel : BaseViewModel
    {
        public EditCryptographyKeyViewModel()
        {
            ChangeKeyCommand = new MvvmHelpers.Commands.AsyncCommand(ChangeKey);
        }

        public ICommand ChangeKeyCommand { get; set; }

        private async Task ChangeKey()
        {
            if (KeyText == MainWindowView.Current.InstanceVariables.CrypthographyKey)
            {
                await NavigationService.ClosePopup<EditCryptographyKeyViewModel>();
                return;
            }
            await CryptographyService.ChangeAllPasswordKeys(MainWindowView.Current.InstanceVariables.CrypthographyKey, KeyText);
            MainWindowView.Current.InstanceVariables.CrypthographyKey = KeyText;
            await NavigationService.ClosePopup<EditCryptographyKeyViewModel>();
        }

        private string keyText;

        public string KeyText
        {
            get { return keyText; }
            set { SetProperty(ref keyText, value); }
        }

    }
}
