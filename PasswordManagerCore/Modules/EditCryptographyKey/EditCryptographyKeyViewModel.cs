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
            KeyText = ConfigurationVariables.CrypthographyKey;
            ChangeKeyCommand = new MvvmHelpers.Commands.AsyncCommand(ChangeKey);
        }

        public ICommand ChangeKeyCommand { get; set; }

        private async Task ChangeKey()
        {
            if (KeyText == ConfigurationVariables.CrypthographyKey)
            {
                await NavigationService.ClosePopup<EditCryptographyKeyViewModel>();
                return;
            }
            await CryptographyService.ChangeAllPasswordKeys(ConfigurationVariables.CrypthographyKey, KeyText);
            ConfigurationVariables.CrypthographyKey = KeyText;
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
