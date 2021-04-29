using MvvmHelpers;
using PasswordManagerCore.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class NotificationPopupViewModel : BaseViewModel
    {
        public NotificationPopupViewModel(string notificationMessage, string notificationButton)
        {
            NotificationMessage = notificationMessage;
            NotificationButtonText = notificationButton;
            CloseNotificationPopupCommand = new MvvmHelpers.Commands.AsyncCommand(CloseNotificationPopup);
        }

        public ICommand CloseNotificationPopupCommand { get; set; }

        public async Task CloseNotificationPopup()
        {
            NavigationService.HasPopupOpen = false;
            await NavigationService.ClosePopup<NotificationPopupViewModel>();
        }

        private string notificationMessage;

        public string NotificationMessage
        {
            get { return notificationMessage; }
            set { SetProperty(ref notificationMessage, value); }
        }

        private string notificationButtonText;

        public string NotificationButtonText
        {
            get { return notificationButtonText; }
            set { SetProperty(ref notificationButtonText, value); }
        }

    }
}
