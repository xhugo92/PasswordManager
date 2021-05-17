using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Model;
using PasswordManagerCore.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class EditInformationViewModel : BaseViewModel
    {
        public EditInformationViewModel(SignInInformation signInInformation)
        {
            this.signInInformation = signInInformation;
            SourceText = signInInformation.Source;
            UsernameText = signInInformation.Username;
            PasswordText = signInInformation.EncryptedPassword;
            DbContext = new DatabaseContext();
            CancelButtonCommand = new MvvmHelpers.Commands.AsyncCommand(CancelButton);
            SaveChangesCommand = new MvvmHelpers.Commands.AsyncCommand(SaveChanges);
        }
        private DatabaseContext DbContext;
        private SignInInformation signInInformation;

        public ICommand SaveChangesCommand { get; set; }

        private async Task SaveChanges()
        {
            if (!CanAdd())
            {
                await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Por favor verifique que todos os campos estão preenchidos", "Ok", 10);
                return;
            }
            signInInformation.Source = SourceText;
            signInInformation.Username = UsernameText;
            signInInformation.EncryptedPassword = PasswordText;
            DbContext.SignInInformations.Update(signInInformation);
            int Sucess = await DbContext.SaveChangesAsync();
            if (Sucess != 1)
            {
                await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Falha ao atualizar as informações, tente novamente mais tarde", "Ok", 10);
                return;
            }
            await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Dados salvos com sucesso", "Ok", 10);
            await NavigationService.ClosePopup<EditInformationViewModel>();
        }

        public ICommand CancelButtonCommand { get; set; }

        private async Task CancelButton()
        {
            await NavigationService.ClosePopup<EditInformationViewModel>();
        }

        private string sourceText;

        public string SourceText
        {
            get { return sourceText; }
            set { SetProperty(ref sourceText, value); }
        }

        private string usernameText;

        public string UsernameText
        {
            get { return usernameText; }
            set { SetProperty(ref usernameText, value); }
        }

        private string passwordText;

        public string PasswordText
        {
            get { return passwordText; }
            set { SetProperty(ref passwordText, value); }
        }

        private bool CanAdd()
        {
            if (string.IsNullOrWhiteSpace(SourceText) || string.IsNullOrWhiteSpace(UsernameText) || string.IsNullOrWhiteSpace(PasswordText))
            {
                return false;
            }
            return true;
        }
    }
}
