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
        #region ATRIBUTOS PRIVADOS
        private bool _isHidden;
        private bool _specialIsChecked;
        private bool _lowerIsChecked;
        private bool _upperIsChecked;
        private bool _numberIsChecked;
        private string _lengthText;
        private string _sourceText;
        private string _passwordText;
        private string _usernameText;
        private string _password;        
        #endregion

        #region ATRIBUTOS PÚBLICOS       
        public bool specialIsChecked
        {
            get { return _specialIsChecked; }
            set { SetProperty(ref _specialIsChecked, value); }
        }
        public bool lowerIsChecked
        {
            get { return _lowerIsChecked; }
            set { SetProperty(ref _lowerIsChecked, value); }
        }
        public bool upperIsChecked
        {
            get { return _upperIsChecked; }
            set { SetProperty(ref _upperIsChecked, value); }
        }
        public bool numberIsChecked
        {
            get { return _numberIsChecked; }
            set { SetProperty(ref _numberIsChecked, value); }
        }
        public bool isHidden
        {
            get { return _isHidden; }
            set { SetProperty(ref _isHidden, value); }
        }       
        public string lengthText
        {
            get { return _lengthText; }
            set { SetProperty(ref _lengthText, value); }
        }
        public string sourceText
        {
            get { return _sourceText; }
            set { SetProperty(ref _sourceText, value); }
        }
        public string usernameText
        {
            get { return _usernameText; }
            set { SetProperty(ref _usernameText, value); }
        }
        public string passwordText
        {
            get { return _passwordText; }
            set { SetProperty(ref _passwordText, value); }
        }
        public string password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }       
        public DatabaseContext DbContext { get; set; }        
        #endregion

        #region COMANDOS
        public ICommand ChangePasswordVisibilityCommand { get; set; }
        public ICommand ChangingPasswordCommand { get; set; }
        public ICommand GeneratePasswordCommand { get; set; }
        public ICommand ShowHidePasswordMenuCommand { get; set; }
        public ICommand AddToDatabaseCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }
        #endregion

        #region CONSTRUTOR
        public AddViewModel()
        {
            GeneratePasswordCommand = new MvvmHelpers.Commands.AsyncCommand(GeneratePassword);
            AddToDatabaseCommand = new MvvmHelpers.Commands.AsyncCommand(AddToDatabase);            
            ChangePasswordVisibilityCommand = new MvvmHelpers.Commands.AsyncCommand(ChangePasswordVisibility);
            ClearFieldsCommand = new MvvmHelpers.Commands.Command(ClearFields);
            ChangingPasswordCommand = new MvvmHelpers.Commands.Command(ChangingPassword);

            DbContext = new DatabaseContext();

            numberIsChecked = true;
            upperIsChecked = true;
            lowerIsChecked = true;
            specialIsChecked = true;
            isHidden = true;
           
            lengthText = "12";            
        }
        #endregion

        #region METODOS PUBLICOS
        public void ClearFields()
        {
            sourceText = "";
            usernameText = "";
            passwordText = "";
        }
        
        public void ChangingPassword()
        {
            if (!string.IsNullOrEmpty(passwordText.Replace("\u25CF", "")))
            {
                password = passwordText;
                return;
            }
        }    

        public async Task GeneratePassword()
        {
            bool tryparser = Int32.TryParse(lengthText, out int passwordLength);
            
            if (!tryparser)
            {
                await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Tamanho de senha invalido, tente novamente", "Ok", 10);
                return;
            }
            string PasswordChars = GeneratePasswordSeedString();
            password = RandomTextGeneratorService.GenerateRandomString(passwordLength, PasswordChars);
            
            if (isHidden)
            {
                passwordText = MultiplyString("\u25CF", passwordLength);
                return;
            }

            passwordText = password;
        }

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
                await AddInDatabase(sourceText, usernameText, password, "Adicionado com sucesso, Senha enviada para a Area de Transferencia");

            }
        }
        #endregion

        #region METODOS PRIVADOS
       
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
            if (string.IsNullOrWhiteSpace(sourceText) || string.IsNullOrWhiteSpace(usernameText) || string.IsNullOrWhiteSpace(passwordText))
            {
                return false;
            }
            return true;
        }

        private string MultiplyString(string a, int b)
        {
            string final = "";
            for (int i = 0; i < b; i++)
            {
                final += a;
            }
            return final;
        }

        private string GeneratePasswordSeedString()
        {
            string seed = "";
            if (numberIsChecked)
            {
                seed += PasswordCharConstants.Number;
            }
            if (upperIsChecked)
            {
                seed += PasswordCharConstants.Upper;
            }
            if (lowerIsChecked)
            {
                seed += PasswordCharConstants.Lower;
            }
            if (specialIsChecked)
            {
                seed += PasswordCharConstants.Special;
            }
            return seed;
        }
        #endregion

        public async Task ChangePasswordVisibility()
        {
            if (isHidden)
            {
                isHidden = false;
                passwordText = password;
                return;
            }

            isHidden = true;
            passwordText = MultiplyString("\u25CF", password.Length);
        }        
    }
}
