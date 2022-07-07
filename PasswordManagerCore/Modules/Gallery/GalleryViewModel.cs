using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Model;
using PasswordManagerCore.Resources;
using PasswordManagerCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    class GalleryViewModel : BaseViewModel
    {
        public GalleryViewModel()
        {
            ComboBoxItemSource = new List<ComboBoxItens>
            {
                new ComboBoxItens("Source", true, FiltertypeEnum.Source),
                new ComboBoxItens("Username", false, FiltertypeEnum.Username)
            };
            SelectedItem = ComboBoxItemSource[0];
            OriginalListBoxItemSource = new List<SignInInformation>();
            DbContext = new DatabaseContext();
            OriginalListBoxItemSource.AddRange(DbContext.SignInInformations.ToList());
            ListBoxItemSource = new ObservableRangeCollection<SignInInformation>(OriginalListBoxItemSource.ToList());
            TextChangedCommand = new MvvmHelpers.Commands.Command(TextChanged);
            ChangePasswordVisibilityCommand = new MvvmHelpers.Commands.Command<SignInInformation>(ChangePasswordVisibility);
            DeleteEntryCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(DeleteEntry);
            ChangeSignInInformationCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(ChangeSignInInformation);
            SendToClypboardCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(SendToClypboard);
            SortUserCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(SortUser);
            SortSourceCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(SortSource);

        }
        #region Commands
        public ICommand SendToClypboardCommand { get; private set; }

        private async Task SendToClypboard(SignInInformation signInInformation)
        {
            if (MainWindowView.Current.InstanceVariables.HasPasswordSetted)
            {
                Clipboard.SetText(CryptographyService.DecryptData(signInInformation.EncryptedPassword,
                    CryptographyService.DecryptData(MainWindowView.Current.InstanceVariables.EncryptedPassword, MainWindowView.Current.InstanceVariables.CrypthographyKey)));
            }
            else
            {
                Clipboard.SetText(CryptographyService.DecryptData(signInInformation.EncryptedPassword, MainWindowView.Current.InstanceVariables.CrypthographyKey));
            }
            await NavigationService.OpenNewWindowAsync<NotificationPopupViewModel>("Enviado para a zona de transferencia", "Ok", 2);
        }


        public ICommand ChangeSignInInformationCommand { get; private set; }

        private async Task ChangeSignInInformation(SignInInformation SignInInformation)
        {
            await NavigationService.OpenNewWindowAsync<EditInformationViewModel>(SignInInformation);
        }

        public ICommand ChangePasswordVisibilityCommand { get; private set; }

        private void ChangePasswordVisibility(SignInInformation SignInInformation)
        {
            if (SignInInformation.PasswordVisibility == Visibility.Hidden)
            {
                SignInInformation.PasswordVisibility = Visibility.Visible;
                SignInInformation.VisibilityState = "O";
            }
            else
            {
                SignInInformation.PasswordVisibility = Visibility.Hidden;
                SignInInformation.VisibilityState = "S";
            }
        }

        public ICommand DeleteEntryCommand { get; private set; }

        private async Task DeleteEntry(SignInInformation SignInInformation)
        {
            //TODO por que está abrindo duas janelas?
            if (!NavigationService.HasPopupOpen)
            {

                NavigationService.HasPopupOpen = true;
                await NavigationService.OpenNewWindowAsync<GenericPopupViewModel>("Deseja realmente deletar essas informações?", "Deletar", "Cancelar", new Action(async () =>
                {
                    ListBoxItemSource.Remove(SignInInformation);
                    OriginalListBoxItemSource.Remove(SignInInformation);
                    DbContext.SignInInformations.Remove(SignInInformation);
                    await DbContext.SaveChangesAsync();
                }));
            }
        }


        public ICommand TextChangedCommand { get; set; }

        private void TextChanged()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ListBoxItemSource.ReplaceRange(OriginalListBoxItemSource);
                return;
            }

            if (SelectedItem.FilterType == FiltertypeEnum.Source)
            {
                var Filter = OriginalListBoxItemSource.Where(Item => Item.Source.ToLower().StartsWith(SearchText.ToLower()));
                ListBoxItemSource.ReplaceRange(Filter);
            }
            else
            {
                var Filter = OriginalListBoxItemSource.Where(Item => Item.Username.ToLower().StartsWith(SearchText.ToLower()));
                ListBoxItemSource.ReplaceRange(Filter);
            }
        }

        public ICommand SortUserCommand { get; set; }

        private Task SortUser(SignInInformation SignInInformation)
        {
            if(TypeUserSort >= 0)
            {
                var Sort = OriginalListBoxItemSource.OrderBy(x => x.Username).ThenBy(x => x.Source);
                ListBoxItemSource.ReplaceRange(Sort);
                TypeUserSort = -1;
            }
            else
            {
                var Sort = OriginalListBoxItemSource.OrderByDescending(x => x.Username).ThenBy(x => x.Source);
                ListBoxItemSource.ReplaceRange(Sort);
                TypeUserSort = 1;
            }

            return Task.CompletedTask;
        }

        public ICommand SortSourceCommand { get; set; }

        private Task SortSource(SignInInformation SignInInformation)
        {
            if(TypeSourceSort >= 0)
            {
                var Sort = OriginalListBoxItemSource.OrderBy(x => x.Source).ThenBy(x => x.Username);
                ListBoxItemSource.ReplaceRange(Sort);
                TypeSourceSort = -1;
            }
            else
            {
                var Sort = OriginalListBoxItemSource.OrderByDescending(x => x.Source).ThenBy(x => x.Username);
                ListBoxItemSource.ReplaceRange(Sort);
                TypeSourceSort = 1;
            }
            
            return Task.CompletedTask;
        }

        #endregion
        #region Variables
        public DatabaseContext DbContext { get; set; }
        private string searchText;

        private int TypeUserSort { get; set; }
        private int TypeSourceSort { get; set; }

        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }

        public ComboBoxItens SelectedItem { get; set; }

        public List<ComboBoxItens> ComboBoxItemSource { get; set; }
        public ObservableRangeCollection<SignInInformation> ListBoxItemSource { get; set; }
        private List<SignInInformation> OriginalListBoxItemSource;
        #endregion
    }
}
