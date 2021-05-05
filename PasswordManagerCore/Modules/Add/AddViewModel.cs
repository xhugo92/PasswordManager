using MvvmHelpers;
using PasswordManagerCore.Services;
using System.Windows.Input;
using PasswordManagerCore.Model;
using System.Threading.Tasks;
using PasswordManagerCore.Database;
using System.Linq;
using System.Security.Cryptography;
using PasswordManagerCore.Constants;
using System.Windows;

namespace PasswordManagerCore.Modules
{
    public class AddViewModel : BaseViewModel
    {
        public AddViewModel()
        {
            AddToDatabaseCommand = new MvvmHelpers.Commands.AsyncCommand(AddToDatabase);
            ClearFieldsCommand = new MvvmHelpers.Commands.Command(ClearFields);
            GeneratePasswordCommand = new MvvmHelpers.Commands.Command(GeneratePassword);
            DbContext = new DatabaseContext();
        }

        public DatabaseContext DbContext { get; set; }
        public ICommand GeneratePasswordCommand { get; set; }

        public void GeneratePassword()
        {
            //TODO: Colocar o tamanho para ser configuravel            
            string RandomPassword = new string(Enumerable.Repeat(PasswordCharConstants.chars, 12)
              .Select(s => s[RNGCryptoServiceProvider.GetInt32(s.Length)]).ToArray());
            PasswordText = RandomPassword;
        }
        
        public ICommand AddToDatabaseCommand { get; set; }

        public async Task AddToDatabase()
        {
            if (!NavigationService.HasPopupOpen)
            {
                if (!CanAdd())
                {

                    NavigationService.HasPopupOpen = true;
                    await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Por favor preencha todos os campos", "Ok", 5);
                    return;
                }
               await AddInDatabase(SourceText, UsernameText, PasswordText, "Adicionado com sucesso, Senha enviada para a Area de Transferencia");

            }
        }

        private async Task AddInDatabase(string Source, string User, string Password, string SucessMessage)
        {
            DbContext.SignInInformations.Add(new SignInInformation(Source, User, Password, false));
            int Sucess = await DbContext.SaveChangesAsync();
            NavigationService.HasPopupOpen = true;
            if (Sucess != 1)
            {
                await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Houve um erro ao salvar as informações, por favor verifique as informações e tente novamente", "Ok", 10);
                return;
            }
            await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>(SucessMessage, "Ok", 10);
            Clipboard.SetText(Password);
            ClearFields();
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
