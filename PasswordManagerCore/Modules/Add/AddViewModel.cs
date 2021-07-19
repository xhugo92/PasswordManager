using MvvmHelpers;
using PasswordManagerCore.Constants;
using PasswordManagerCore.Database;
using PasswordManagerCore.Model;
using PasswordManagerCore.Services;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class AddViewModel : BaseViewModel
    {
        public AddViewModel()
        {
            GeneratePasswordCommand = new MvvmHelpers.Commands.AsyncCommand(GeneratePassword);
            AddToDatabaseCommand = new MvvmHelpers.Commands.AsyncCommand(AddToDatabase);
            ShowHidePasswordMenuCommand = new MvvmHelpers.Commands.AsyncCommand(ShowHidePasswordMenu);
            ClearFieldsCommand = new MvvmHelpers.Commands.Command(ClearFields);
            LengthText = "12";
            PasswordMenuVisibility = Visibility.Collapsed;
            NumberIsChecked = true;
            UpperIsChecked = true;
            LowerIsChecked = true;
            SpecialIsChecked = true;
            DbContext = new DatabaseContext();
        }

        #region Methods
        private string GeneratePasswordSeedString()
        {
            string seed ="";
            if(NumberIsChecked)
            {
                seed += PasswordCharConstants.Number;
            }
            if(UpperIsChecked)
            {
                seed += PasswordCharConstants.Upper;
            }
            if(LowerIsChecked)
            {
                seed += PasswordCharConstants.Lower;
            }
            if( SpecialIsChecked)
            {
                seed += PasswordCharConstants.Special;
            }
            return seed;
        }
        #endregion

        #region Commands

        public DatabaseContext DbContext { get; set; }
        public ICommand GeneratePasswordCommand { get; set; }

        public async Task GeneratePassword()
        {
            bool tryparser = Int32.TryParse(LengthText, out int passwordLength);
            if (!tryparser)
            {
                await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Tamanho de senha invalido, tente novamente", "Ok", 10);
                return;
            }
            string PasswordChars = GeneratePasswordSeedString();
            PasswordText = RandomTextGeneratorService.GenerateRandomString(passwordLength, PasswordChars);
        }


        public ICommand ShowHidePasswordMenuCommand { get; set; }

        public async Task ShowHidePasswordMenu()
        {
            if (PasswordMenuVisibility == Visibility.Collapsed)
            {
                PasswordMenuVisibility = Visibility.Visible;
                return;
            }
            PasswordMenuVisibility = Visibility.Collapsed;
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
        #endregion

        #region properties

        private bool specialIsChecked;

        public bool SpecialIsChecked
        {
            get { return specialIsChecked; }
            set { SetProperty(ref specialIsChecked, value); }
        }


        private bool lowerIsChecked;

        public bool LowerIsChecked
        {
            get { return lowerIsChecked; }
            set { SetProperty(ref lowerIsChecked, value); }
        }


        private bool upperIsChecked;

        public bool UpperIsChecked
        {
            get { return upperIsChecked; }
            set { SetProperty(ref upperIsChecked, value); }
        }


        private Visibility passwordMenuVisibility;

        public Visibility PasswordMenuVisibility
        {
            get { return passwordMenuVisibility; }
            set { SetProperty(ref passwordMenuVisibility, value); }
        }

        private bool numberIsChecked;

        public bool NumberIsChecked
        {
            get { return numberIsChecked; }
            set { SetProperty(ref numberIsChecked, value); }
        }

        private string lengthText;

        public string LengthText
        {
            get { return lengthText; }
            set { SetProperty(ref lengthText, value); }
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
        #endregion
    }
}
