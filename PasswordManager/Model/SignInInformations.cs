using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager.Model
{
    class SignInInformations:ObservableObject
    {
        public SignInInformations()
        {
            VisibilityState = "S";
            PasswordVisibility = Visibility.Hidden;
        }

        public string Source { get; set; }

        public string Username { get; set; }

        public string EncryptedPassword { get; set; }

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

    }
}
