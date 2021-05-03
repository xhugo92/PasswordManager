﻿using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Model;
using PasswordManagerCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        }
        public DatabaseContext DbContext { get; set; }

        public ICommand ChangeSignInInformationCommand { get; set; }

        private async Task ChangeSignInInformation(SignInInformation SignInInformation)
        {
            await NavigationService.OpenNewWindowAsync<EditInformationViewModel>(SignInInformation);
        }

        public ICommand ChangePasswordVisibilityCommand { get; set; }

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

        public ICommand DeleteEntryCommand { get; set; }

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
