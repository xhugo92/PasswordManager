using MvvmHelpers;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PasswordManagerCore.Services
{
    public static class TimerService
    {
        private static DispatcherTimer dispatcherTimer;
        private static Window ObjectToClose;

        public static async Task CloseAfterSeconds<T>(TimeSpan Timer) where T : BaseViewModel
        {

            ObjectToClose = NavigationService.FindPopupWindow<T>();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick; ;
            dispatcherTimer.Interval = Timer;
            dispatcherTimer.Start();
        }

        private static void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (ObjectToClose != null)
            {
                ObjectToClose.Close();
                NavigationService.Popups.Remove(ObjectToClose);
            }
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= DispatcherTimer_Tick;
        }
    }
}
