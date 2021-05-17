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
        }

        public ICommand ChangeCryptographyCommand { get; set; }

        private async Task ChangeCryptography()
        {
            NavigationService.OpenNewWindowAsync<EditCryptographyKeyViewModel>();
        }
    }
}
