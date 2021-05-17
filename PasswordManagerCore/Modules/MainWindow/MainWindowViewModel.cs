using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Resources;
using PasswordManagerCore.Services;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            NavigateHomeCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateHome);
            NavigateAddCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateAdd);
            NavigateGalleryCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateGallery);
            NavigateConfigurationCommand = new MvvmHelpers.Commands.AsyncCommand(NavigateConfiguration);
            OpenHelpWindowsCommand = new MvvmHelpers.Commands.AsyncCommand(OpenHelpWindows);
            CloseMainWindowCommand = new MvvmHelpers.Commands.AsyncCommand(CloseMainWindow);
            OnLoadCommand = new MvvmHelpers.Commands.AsyncCommand(OnLoad);
        }

        public ICommand OnLoadCommand { get; set; }

        private async Task OnLoad()
        {
            Task.Run(() =>
            {
                DatabaseContext DbContext = new DatabaseContext();
                DbContext.EnsureCreation();
            });
            StartupService.Startup();
            await NavigateHome();
        }

        public ICommand CloseMainWindowCommand { get; set; }

        private async Task CloseMainWindow()
        {
            System.IO.File.WriteAllBytes("config.cfg", Encoding.ASCII.GetBytes(ConfigurationVariables.CrypthographyKey));
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
