using MvvmHelpers;
using PasswordManager.Model;
using PasswordManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManager.Modules
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
            OriginalListBoxItemSource = new List<SignInInformations>
            {

                new SignInInformations() {Source = "Steam", Username = "Hugofrn1992", EncryptedPassword = "teste" },
                new SignInInformations() { Source = "Steam", Username = "Jão", EncryptedPassword = "jao1234" }
            };
            ListBoxItemSource = new ObservableRangeCollection<SignInInformations>(OriginalListBoxItemSource.ToList());
            TextChangedCommand = new MvvmHelpers.Commands.AsyncCommand(TextChanged);
            ChangeSignInInformationCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformations>(ChangeSignInInformation);
            ChangePasswordVisibilityCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformations>(ChangePasswordVisibility);
            DeleteEntryCommand = new MvvmHelpers.Commands.AsyncCommand<SignInInformations>(DeleteEntry);

        }

        public ICommand ChangeSignInInformationCommand { get; set; }

        private async Task ChangeSignInInformation(SignInInformations signInInformations)
        {
            await NavigationService.OpenNewWindowAsync<EditInformationViewModel>(signInInformations);
        }
        
        public ICommand ChangePasswordVisibilityCommand { get; set; }

        private async Task ChangePasswordVisibility(SignInInformations signInInformations)
        {
            if (signInInformations.PasswordVisibility == Visibility.Hidden)
            {
                signInInformations.PasswordVisibility = Visibility.Visible;
                signInInformations.VisibilityState = "O";
            }
            else
            {
                signInInformations.PasswordVisibility = Visibility.Hidden;
                signInInformations.VisibilityState = "S";
            }
        }

        public ICommand DeleteEntryCommand { get; set; }

        private async Task DeleteEntry(SignInInformations signInInformations)
        {
            ListBoxItemSource.Remove(signInInformations);
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
        public ObservableRangeCollection<SignInInformations> ListBoxItemSource { get; set; }
        private List<SignInInformations> OriginalListBoxItemSource;


    }
}
