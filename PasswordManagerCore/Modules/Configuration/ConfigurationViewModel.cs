using MvvmHelpers;
using PasswordManagerCore.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    class ConfigurationViewModel : BaseViewModel
    {
        public ConfigurationViewModel()
        {
            ChangeCryptographyCommand = new MvvmHelpers.Commands.AsyncCommand(ChangeCryptography);
            ConfigMasterKeyCommand = new MvvmHelpers.Commands.AsyncCommand(ConfigMasterKey);
            MasterKeyText = "Definir senha mestra para o aplicativo";
            MasterKeyButton = "Definir";
        }

        public ICommand ChangeCryptographyCommand { get; set; }

        private async Task ChangeCryptography()
        {
            await NavigationService.OpenNewWindowAsync<EditCryptographyKeyViewModel>();
        }
        
        public ICommand ConfigMasterKeyCommand { get; set; }

        private async Task ConfigMasterKey()
        {
            await NavigationService.NavigateAsync<MasterKeySetterViewModel>();
        }        

        private string masterKeyText;

        public string MasterKeyText
        {
            get { return masterKeyText; }
            set { SetProperty( ref masterKeyText, value); }
        }
        
        private string masterKeyButton;

        public string MasterKeyButton
        {
            get { return masterKeyButton; }
            set { SetProperty( ref masterKeyButton, value); }
        }

    }
}
