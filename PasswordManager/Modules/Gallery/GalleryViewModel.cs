using MvvmHelpers;
using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager.Modules
{
    class GalleryViewModel:BaseViewModel
    {
        public GalleryViewModel()
        {
            ItemSource = new ObservableRangeCollection<SignInInformations>();
            ItemSource.Add(new SignInInformations(){ Source = "Steam", Username = "Hugofrn1992", EncryptedPassword="teste"});
            ItemSource.Add(new SignInInformations(){ Source = "Steam", Username = "Jão", EncryptedPassword="jao1234"});
            ChangePasswordVisibilityCommand = new MvvmHelpers.Commands.AsyncCommand(ChangePasswordVisibility);
            VisibilityState = "S";
            PasswordVisibility = Visibility.Hidden;
        }

        public ICommand ChangePasswordVisibilityCommand { get; set; }

        private async Task ChangePasswordVisibility()
        {
            if(PasswordVisibility==Visibility.Hidden)
            {
                PasswordVisibility = Visibility.Visible;
                VisibilityState = "O";
            }
            else
            {
                PasswordVisibility = Visibility.Hidden;
                VisibilityState = "S";
            }
        }
        private Visibility passwordVisibility;

        public Visibility PasswordVisibility
        {
            get { return passwordVisibility; }
            set { SetProperty(ref passwordVisibility, value); }
        }

        private string visibilityState;

        public string VisibilityState
        {
            get { return visibilityState; }
            set { SetProperty(ref visibilityState, value); }
        }

        public ObservableRangeCollection<SignInInformations> ItemSource { get; set; }


    }
}
