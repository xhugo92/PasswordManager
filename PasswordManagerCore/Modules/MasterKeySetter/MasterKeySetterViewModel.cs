using MvvmHelpers;
using PasswordManagerCore.Resources;
using PasswordManagerCore.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        private string resultText;

        public string ResultText
        {
            get { return resultText; }
            set { SetProperty(ref resultText, value); }
        }

        public ICommand ReturnButtonCommand { get; set; }

        private async Task ReturnButton()
        {
            await NavigationService.PopAsync();
        }     

        public async Task SetMasterKey(SecureString Password)
        {
            MainWindowView.Current.InstanceVariables.HasPasswordSetted = true;
            MainWindowView.Current.InstanceVariables.EncryptedPassword = CryptographyService.EncryptData(SecureStringToString(Password), MainWindowView.Current.InstanceVariables.CrypthographyKey);
            await NavigationService.PopAsync();
        }

        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
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
