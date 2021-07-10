using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    class ConfigurationViewModel : BaseViewModel
    {
        public ConfigurationViewModel()
        {
            ChangeCryptographyCommand = new MvvmHelpers.Commands.AsyncCommand(ChangeCryptography);
            WypeDatabaseCommand = new MvvmHelpers.Commands.AsyncCommand(WypeDatabase);
            ErasePasswordCommand = new MvvmHelpers.Commands.AsyncCommand(ErasePassword);
            CanChangeCrypthographyKey = !MainWindowView.Current.InstanceVariables.HasPasswordSetted;
            if (MainWindowView.Current.InstanceVariables.HasPasswordSetted)
            {
                MasterKeyText = "Alterar senha mestra do aplicativo";
                MasterKeyButton = "Alterar";
                ErasePasswordVisibility = Visibility.Visible;
                CrypthografyKeyVisibility = Visibility.Collapsed;
                ConfigMasterKeyCommand = new MvvmHelpers.Commands.AsyncCommand(ChangeMasterKey);
            }
            else
            {
                MasterKeyText = "Definir senha mestra para o aplicativo";
                MasterKeyButton = "Definir";
                CrypthografyKeyVisibility = Visibility.Visible;
                ErasePasswordVisibility = Visibility.Collapsed;
                ConfigMasterKeyCommand = new MvvmHelpers.Commands.AsyncCommand(ConfigMasterKey);
            }
        }

        private Visibility crypthografyKeyVisibility;

        public Visibility CrypthografyKeyVisibility
        {
            get { return crypthografyKeyVisibility; }
            set { SetProperty( ref crypthografyKeyVisibility, value); }
        }


        public ICommand WypeDatabaseCommand { get; set; }

        private async Task WypeDatabase()
        {
            if(MainWindowView.Current.InstanceVariables.HasPasswordSetted)
            {
                await NavigationService.NavigateAsync<LoginViewModel>("Wype");
                return;
            }
            await NavigationService.OpenNewWindowAsync<GenericPopupViewModel>("Você tem certeza que deseja limpar o banco de dados?", "Sim", "Não", new Action(() =>
            {
                DatabaseContext DbContext = new DatabaseContext();
                DbContext.SignInInformations.RemoveRange(DbContext.SignInInformations);
                DbContext.SaveChangesAsync();
            }));
        }

        public ICommand ChangeCryptographyCommand { get; set; }

        private async Task ChangeCryptography()
        {
            await NavigationService.OpenNewWindowAsync<EditCryptographyKeyViewModel>();
        }
        
        public ICommand ErasePasswordCommand { get; set; }

        private async Task ErasePassword()
        {
            await NavigationService.NavigateAsync<LoginViewModel>("Erase");
        }

        public ICommand ConfigMasterKeyCommand { get; set; }

        private async Task ConfigMasterKey()
        {
            await NavigationService.NavigateAsync<MasterKeySetterViewModel>();
        }

        private async Task ChangeMasterKey()
        {
            await NavigationService.NavigateAsync<LoginViewModel>("Change");
        }

        private string masterKeyText;

        public string MasterKeyText
        {
            get { return masterKeyText; }
            set { SetProperty(ref masterKeyText, value); }
        }

        private Visibility erasePasswordVisibility;

        public Visibility ErasePasswordVisibility
        {
            get { return erasePasswordVisibility; }
            set { SetProperty( ref erasePasswordVisibility, value); }
        }


        private string masterKeyButton;

        public string MasterKeyButton
        {
            get { return masterKeyButton; }
            set { SetProperty(ref masterKeyButton, value); }
        }

        private bool canChangeCrypthographyKey;

        public bool CanChangeCrypthographyKey
        {
            get { return canChangeCrypthographyKey; }
            set { SetProperty(ref canChangeCrypthographyKey, value); }
        }


    }
}
