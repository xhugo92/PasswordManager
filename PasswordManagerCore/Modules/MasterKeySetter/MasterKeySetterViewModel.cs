using MvvmHelpers;
using PasswordManagerCore.Services;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class MasterKeySetterViewModel :BaseViewModel
    {
        public MasterKeySetterViewModel()
        {
            ReturnButtonCommand = new MvvmHelpers.Commands.AsyncCommand(ReturnButton);
        }

        public ICommand ReturnButtonCommand { get; set; }

        private async Task ReturnButton()
        {
            await NavigationService.PopAsync();
        }

        public async Task SetMasterKey(SecureString Password)
        {
            await NavigationService.PopAsync();
        }
    }
}
