using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using PasswordManagerCore.Services;

namespace PasswordManagerCore.Modules
{
    public class MainWindowViewModel :BaseViewModel
    {
        public MainWindowViewModel()
        {
            NavigateHomeCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateHome);
            NavigateAddCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateAdd);
            NavigateGalleryCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateGallery);
            NavigateConfigurationCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateConfiguration);
            OpenHelpWindowsCommand = new MvvmHelpers.Commands.AsyncCommand(OpenHelpWindows);
            CloseAllWindowsCommand = new MvvmHelpers.Commands.AsyncCommand(CloseAllWindows);
        }

        public ICommand CloseAllWindowsCommand { get; set; }

        private async Task CloseAllWindows()
        {
            await NavigationService.CloseAllWindows();
        }

        public ICommand NavigateHomeCommand { get; set; }

        private async Task NavigateHome()
        {
            await NavigationService.NavigateAsync<HomeViewModel>();
        }

        public ICommand NavigateAddCommand { get; set; }

        private async Task NavigateAdd()
        {
            await NavigationService.NavigateAsync<AddViewModel>();
        }

        public ICommand NavigateGalleryCommand { get; set; }

        private async Task NavigateGallery()
        {
            await NavigationService.NavigateAsync<GalleryViewModel>();
        }

        public ICommand NavigateConfigurationCommand { get; set; }

        private async Task NavigateConfiguration()
        {
            await NavigationService.NavigateAsync<ConfigurationViewModel>();
        }

        public ICommand OpenHelpWindowsCommand { get; set; }

        private async Task OpenHelpWindows()
        {
            await NavigationService.OpenNewWindowAsync<HelpViewModel>();
        }
    }

}
