using MvvmHelpers;
using PasswordManagerCore.Services;
using System.Windows.Input;
using PasswordManagerCore.Model;
using System.Threading.Tasks;
using PasswordManagerCore.Database;

namespace PasswordManagerCore.Modules
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
                App.DatabaseContext.SignInInformations.Add(new SignInInformation(SourceText, UsernameText, PasswordText, false));
                int Sucess = App.DatabaseContext.SaveChanges();
                if (Sucess != 1)
                {
                    await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Houve um erro ao salvar as informações, por favor verifique e tente novamente", "Ok");
                    return;
                }
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
