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
            IsEnabled = false;
        }

        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }

        public ICommand ReturnButtonCommand { get; set; }

        private async Task ReturnButton()
        {
            await NavigationService.PopAsync();
        }

        private string resultText;

        public string ResultText
        {
            get { return resultText; }
            set { SetProperty( ref resultText, value); }
        }


        public async Task SetMasterKey(SecureString Password)
        {
            await NavigationService.PopAsync();
        }

        public void ChangeOkButtonAvaliability(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public void ChangeResultText (string Result)
        {
            ResultText = Result;
        }
    }
}
