using MvvmHelpers;
using PasswordManagerCore.Modules;
using PasswordManagerCore.Resources;
using PasswordManagerCore.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace PasswordManagerCore.Model
{
    public class SignInInformation : ObservableObject
    {
        public SignInInformation(string Source, string Username, string Password, bool isEncrypted)
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
                if (MainWindowView.Current.InstanceVariables.HasPasswordSetted)
                {
                    EncryptedPassword = CryptographyService.EncryptData(Password, CryptographyService.DecryptData(MainWindowView.Current.InstanceVariables.EncryptedPassword, MainWindowView.Current.InstanceVariables.CrypthographyKey));
                }
                else
                {
                    EncryptedPassword = CryptographyService.EncryptData(Password, MainWindowView.Current.InstanceVariables.CrypthographyKey);
                }
            }
        }

        public SignInInformation(string Source, string Username, string EncryptedPassword)
        {
            this.Source = Source;
            this.Username = Username;
            this.EncryptedPassword = EncryptedPassword;
            this.PasswordVisibility = Visibility.Hidden;
            this.VisibilityState = "S";
        }

        [Key]
        public int Id { get; set; }

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

        [NotMapped]
        public Visibility PasswordVisibility
        {
            get { return passwordVisibility; }
            set { SetProperty(ref passwordVisibility, value); }
        }

        private string visibilityState;

        [NotMapped]
        public string VisibilityState
        {
            get { return visibilityState; }
            set { SetProperty(ref visibilityState, value); }
        }

    }
}
