using MvvmHelpers;
using PasswordManagerCore.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class GenericPopupViewModel : BaseViewModel
    {
        public GenericPopupViewModel(string message, string ContentOkButton, string ContentCancelButton, Action OkButtonAction, Action CancelButtonAction)
        {
            Message = message;
            this.ContentOkButton = ContentOkButton;
            this.ContentCancelButton = ContentCancelButton;
            this.OkButtonAction = OkButtonAction;
            this.CancelButtonAction = CancelButtonAction;
            InitializeCommand();
        }

        public GenericPopupViewModel(string message, string ContentOkButton, string ContentCancelButton, Action OkButtonAction)
        {
            Message = message;
            this.ContentOkButton = ContentOkButton;
            this.ContentCancelButton = ContentCancelButton;
            this.OkButtonAction = OkButtonAction;
            CancelButtonAction = new Action(async () => { NavigationService.HasPopupOpen = false; await NavigationService.ClosePopup<GenericPopupViewModel>(); });
            InitializeCommand();

        }

        private void InitializeCommand()
        {
            OkButtonCommand = new MvvmHelpers.Commands.AsyncCommand(OkButton);
            CancelButtonCommand = new MvvmHelpers.Commands.Command(CancelButton);
        }


        public ICommand CancelButtonCommand { get; set; }

        public void CancelButton()
        {
            CancelButtonAction.Invoke();
        }

        public Action CancelButtonAction { get; set; }

        public ICommand OkButtonCommand { get; set; }

        public async Task OkButton()
        {
            OkButtonAction.Invoke();
            NavigationService.HasPopupOpen = false;
            await NavigationService.ClosePopup<GenericPopupViewModel>();
        }

        public Action OkButtonAction { get; set; }

        private string contentOkButton;

        public string ContentOkButton
        {
            get { return contentOkButton; }
            set { SetProperty(ref contentOkButton, value); }
        }

        private string contentCancelButton;

        public string ContentCancelButton
        {
            get { return contentCancelButton; }
            set { SetProperty(ref contentCancelButton, value); }
        }


        private string message;

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

    }
}
