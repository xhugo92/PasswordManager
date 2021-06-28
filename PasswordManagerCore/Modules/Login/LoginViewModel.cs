using MvvmHelpers;
using PasswordManagerCore.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace PasswordManagerCore.Modules
{
    class LoginViewModel:BaseViewModel
    {

        public bool AuthenticationProcess(SecureString Password)
        {
            if(CryptographyService.DecryptData(MainWindowView.Current.InstanceVariables.EncryptedPassword, MainWindowView.Current.InstanceVariables.CrypthographyKey) == SecureStringToString(Password))
            {
                NavigationService.NavigateAsync<HomeViewModel>();
                return true;
            }
            ResultText = "Falha na autenticação, por favor verifique a sua senha";
            return false;
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

        private string resultText;

        public string ResultText
        {
            get { return resultText; }
            set { SetProperty(ref resultText, value); }
        }

    }
}
