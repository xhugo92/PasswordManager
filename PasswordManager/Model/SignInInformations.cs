using MvvmHelpers;
using PasswordManager.Services;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager.Model
{
    public class SignInInformations : ObservableObject
    {
        public SignInInformations(string Source, string Username, string Password, bool isEncrypted)
        {
            VisibilityState = "S";
            PasswordVisibility = Visibility.Hidden;
            this.Source = Source;
            this.Username = Username;
            if (isEncrypted)
            {
                EncryptedPassword = Password;
            }
            else
            {
                EncryptedPassword = CryptographyService.EncryptData(Password);
            }
        }

        private string source;

        public string Source
        {
            get { return source; }
            set { SetProperty(ref source, value); }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string encryptedPassword;

        public string EncryptedPassword
        {
            get { return encryptedPassword; }
            set { SetProperty(ref encryptedPassword, value); }
        }

        private Visibility passwordVisibility;

        public Visibility PasswordVisibility
        {
            get { return passwordVisibility; }
            set { SetProperty(ref passwordVisibility, value); }
        }

        private string visibilityState;

        public string VisibilityState
        {
            get { return visibilityState; }
            set { SetProperty(ref visibilityState, value); }
        }

    }
}
