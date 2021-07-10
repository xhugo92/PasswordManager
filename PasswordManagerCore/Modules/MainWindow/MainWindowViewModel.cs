using MvvmHelpers;
using PasswordManagerCore.Database;
using PasswordManagerCore.Resources;
using PasswordManagerCore.Services;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            LoadAndUnloadService.Startup();
            if(MainWindowView.Current.InstanceVariables.HasPasswordSetted)
            {
                MainWindowView.Current.InstanceVariables.IsLogedIn = false;
                NavigationVisibility = Visibility.Collapsed;
                await NavigationService.NavigateAsync<LoginViewModel>("Home", new Action(()=> { NavigationVisibility = Visibility.Visible; }));
                return;
            }
            await NavigateHome();
        }

        private Visibility navigationVisibility;

        public Visibility NavigationVisibility
        {
            get { return navigationVisibility; }
            set { SetProperty(ref navigationVisibility, value); }
        }


        public ICommand CloseMainWindowCommand { get; set; }

        private async Task CloseMainWindow()
        {
            LoadAndUnloadService.SaveAll();
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
