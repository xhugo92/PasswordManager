using MvvmHelpers;
using PasswordManagerCore.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class NotificationPopupViewModel : BaseViewModel
    {
        public NotificationPopupViewModel(string notificationMessage, string notificationButton, double Seconds)
        {
            NotificationMessage = notificationMessage;
            NotificationButtonText = notificationButton;
            this.Seconds = Seconds;
            CloseNotificationPopupCommand = new MvvmHelpers.Commands.AsyncCommand(CloseNotificationPopup);
            StartTimerCommand = new MvvmHelpers.Commands.AsyncCommand(StartTimer);
            ClosingCommand = new MvvmHelpers.Commands.AsyncCommand(Closing);
        }
        private double Seconds;

        private bool isClosing = false;

        public ICommand ClosingCommand { get; set; }

        private async Task Closing()
        {
            isClosing = true;
        }

        public ICommand StartTimerCommand { get; set; }

        private async Task StartTimer()
        {
            await TimerService.CloseAfterSeconds<NotificationPopupViewModel>(TimeSpan.FromSeconds(Seconds));
        }

        public ICommand CloseNotificationPopupCommand { get; set; }

        private async Task CloseNotificationPopup()
        {
            NavigationService.HasPopupOpen = false;
            if (!isClosing)
            {
                await NavigationService.ClosePopup<NotificationPopupViewModel>();
            }
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
