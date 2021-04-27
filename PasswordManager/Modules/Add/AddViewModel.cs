using MvvmHelpers;
using PasswordManager.Services;
using System.Windows.Input;
using PasswordManager.Model;
using System.Threading.Tasks;

namespace PasswordManager.Modules
{
    public class AddViewModel : BaseViewModel
    {
        public AddViewModel()
        {
            AddToDatabaseCommand = new MvvmHelpers.Commands.AsyncCommand(AddToDatabase);
            ClearFieldsCommand = new MvvmHelpers.Commands.Command(ClearFields);
        }
        public ICommand AddToDatabaseCommand { get; set; }

        public async Task AddToDatabase()
        {
            if (!NavigationService.HasPopupOpen)
            {
                if (!CanAdd())
                {

                    NavigationService.HasPopupOpen = true;
                    await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Por favor preencha todos os campos", "Ok");
                    return;
                }
                NavigationService.HasPopupOpen = true;
                DatabaseSimulator.Add(new SignInInformations(SourceText, UsernameText, PasswordText, false));
                await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Adicionado com sucesso", "Ok");
                ClearFields();
            }
        }

        private bool CanAdd()
        {
            if (string.IsNullOrWhiteSpace(SourceText) || string.IsNullOrWhiteSpace(UsernameText) || string.IsNullOrWhiteSpace(PasswordText))
            {
                return false;
            }
            return true;
        }

        public ICommand ClearFieldsCommand { get; set; }

        public void ClearFields()
        {
            SourceText = "";
            UsernameText = "";
            PasswordText = "";
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
    }
}
