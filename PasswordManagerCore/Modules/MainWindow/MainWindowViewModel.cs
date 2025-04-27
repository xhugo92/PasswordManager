using System;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

using MvvmHelpers;
using MvvmHelpers.Commands;

using PasswordManagerCore.Database;
using PasswordManagerCore.Services;

namespace PasswordManagerCore.Modules
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region ENUMS
        public enum Tab
        {
            Home,
            Add,
            Gallery,
            Config
        }
        #endregion

        #region ATRIBUTOS PRIVADOS
        private Tab? _CurrentTab;
        private Visibility navigationVisibility;
        #endregion

        #region ATRIBUTOS PÚBLICOS
        public Tab? CurrentTab
        {
            get => _CurrentTab;
            set => SetProperty(ref _CurrentTab, value);
        }

        public Visibility NavigationVisibility
        {
            get { return navigationVisibility; }
            set { SetProperty(ref navigationVisibility, value); }
        }
        #endregion

        #region COMANDOS
        public ICommand OnLoadCommand { get; set; }
        public ICommand CloseMainWindowCommand { get; set; }
        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateAddCommand { get; set; }
        public ICommand NavigateGalleryCommand { get; set; }
        public ICommand NavigateConfigurationCommand { get; set; }
        public ICommand OpenHelpWindowsCommand { get; set; }
        #endregion

        #region CONSTRUTOR
        public MainWindowViewModel()
        {
            NavigateHomeCommand = new AsyncCommand(() => NavigateToAsync(Tab.Home)); // use lambda quando a função que quer rodar precisa de parametros.
            NavigateAddCommand = new AsyncCommand(() => NavigateToAsync(Tab.Add));
            NavigateGalleryCommand = new AsyncCommand(() => NavigateToAsync(Tab.Gallery));
            NavigateConfigurationCommand = new AsyncCommand(() => NavigateToAsync(Tab.Config));
            OpenHelpWindowsCommand = new AsyncCommand(OpenHelpWindows);
            CloseMainWindowCommand = new AsyncCommand(CloseMainWindow);
            OnLoadCommand = new MvvmHelpers.Commands.AsyncCommand(OnLoad);
        }

        #endregion

        #region METODOS PRIVADOS
        private async Task OnLoad()
        {
            InitializeDatabase();
            LoadAndUnloadService.Startup();

            await NavigateToAsync(Tab.Home);
        }
        private async Task CloseMainWindow()
        {
            LoadAndUnloadService.SaveAll();
            await NavigationService.CloseAllWindows();
        }
        private async Task OpenHelpWindows()
        {
            await NavigationService.OpenNewWindowAsync<HelpViewModel>();
        }
        private async Task NavigateToAsync(Tab tab)
        {
            CurrentTab = tab;

            switch (tab)
            {
                case Tab.Home:
                    await NavigationService.NavigateAsync<HomeViewModel>();
                    break;

                case Tab.Add:
                    await NavigationService.NavigateAsync<AddViewModel>();
                    break;

                case Tab.Gallery:
                    await NavigationService.NavigateAsync<GalleryViewModel>();
                    break;

                case Tab.Config:
                    await NavigationService.NavigateAsync<ConfigurationViewModel>();
                    break;
            }
        }
        private void InitializeDatabase()
        {
            using var dbContext = new DatabaseContext();
            dbContext.EnsureCreation();
        }
        #endregion
    }

}
