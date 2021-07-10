using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Services;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(string Destiny)
        {
            destiny = Destiny;
        }

        public LoginViewModel(string Destiny, Action action)
        {
            destiny = Destiny;
            this.action = action;
        }

        private string destiny;
        private readonly Action action;


        public bool AuthenticationProcess(SecureString Password)
        {
            if (CryptographyService.DecryptData(MainWindowView.Current.InstanceVariables.EncryptedPassword, MainWindowView.Current.InstanceVariables.CrypthographyKey) == SecureStringToString(Password))
            {
                NavigationService.PopAsync();
                switch(destiny)
                {
                    case "Home":
                        MainWindowView.Current.InstanceVariables.IsLogedIn = true;
                        action.Invoke();
                        NavigationService.NavigateAsync<HomeViewModel>();
                        break;
                    case "Change":
                        NavigationService.NavigateAsync<MasterKeySetterViewModel>();
                        break;
                    case "Erase":
                        MainWindowView.Current.InstanceVariables.HasPasswordSetted = false;
                        CryptographyService.ChangeAllPasswordKeys(CryptographyService.DecryptData(MainWindowView.Current.InstanceVariables.EncryptedPassword, MainWindowView.Current.InstanceVariables.CrypthographyKey), MainWindowView.Current.InstanceVariables.CrypthographyKey) ;
                        MainWindowView.Current.InstanceVariables.EncryptedPassword = "";
                        NavigationService.NavigateAsync<ConfigurationViewModel>();
                        break;
                    case "Wype":
                        NavigationService.OpenNewWindowAsync<GenericPopupViewModel>("Você tem certeza que deseja limpar o banco de dados?", "Sim", "Não", new Action(() =>
                        {
                            DatabaseContext DbContext = new DatabaseContext();
                            DbContext.SignInInformations.RemoveRange(DbContext.SignInInformations);
                            DbContext.SaveChangesAsync();
                        }));
                        NavigationService.NavigateAsync<ConfigurationViewModel>();
                        break;
                }
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
