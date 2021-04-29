using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Model;
using PasswordManagerCore.Services;
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
            OriginalListBoxItemSource.AddRange(App.DatabaseContext.SignInInformations.ToList());
            ListBoxItemSource = new ObservableRangeCollection<SignInInformation>(OriginalListBoxItemSource.ToList());
            TextChangedCommand = new MvvmHelpers.Commands.AsyncCommand(TextChanged);
            ChangeSignInInformationCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(ChangeSignInInformation);
            ChangePasswordVisibilityCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(ChangePasswordVisibility);
            DeleteEntryCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformation>(DeleteEntry);

        }

        public ICommand ChangeSignInInformationCommand { get; set; }

        private async Task ChangeSignInInformation(SignInInformation SignInInformation)
        {
            await NavigationService.OpenNewWindowAsync<EditInformationViewModel>(SignInInformation);
        }

        public ICommand ChangePasswordVisibilityCommand { get; set; }

        private async Task ChangePasswordVisibility(SignInInformation SignInInformation)
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

        public ICommand DeleteEntryCommand { get; set; }

        private async Task DeleteEntry(SignInInformation SignInInformation)
        {
            ListBoxItemSource.Remove(SignInInformation);
            OriginalListBoxItemSource.Remove(SignInInformation);
        }

        public ICommand TextChangedCommand { get; set; }

        private async Task TextChanged()
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

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }

        public ComboBoxItens SelectedItem { get; set; }

        public List<ComboBoxItens> ComboBoxItemSource { get; set; }
        public ObservableRangeCollection<SignInInformation> ListBoxItemSource { get; set; }
        private List<SignInInformation> OriginalListBoxItemSource;


    }
}
