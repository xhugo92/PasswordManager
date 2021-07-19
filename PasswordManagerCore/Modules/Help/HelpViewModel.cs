using MvvmHelpers;
using PasswordManagerCore.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    class HelpViewModel : BaseViewModel
    {
        public HelpViewModel()
        {
            HelpAddNavigateCommand = new MvvmHelpers.Commands.AsyncCommand(HelpAddNavigate);
            HelpGalleryNavigateCommand = new MvvmHelpers.Commands.AsyncCommand(HelpGalleryNavigate);
            HelpConfigNavigateCommand = new MvvmHelpers.Commands.AsyncCommand(HelpConfigNavigate);
            OnLoadCommand = new MvvmHelpers.Commands.AsyncCommand(OnLoad);
        }
        public ICommand OnLoadCommand { get; set; }

        private async Task OnLoad()
        {
            NavigationService.NavigateHelpAsync<HelpAddViewModel>();
        }

        public ICommand HelpGalleryNavigateCommand { get; set; }
        
        private async Task HelpGalleryNavigate()
        {
            await NavigationService.NavigateHelpAsync<HelpGalleryViewModel>();
        }
        
        public ICommand HelpAddNavigateCommand { get; set; }
        
        private async Task HelpAddNavigate()
        {
            await NavigationService.NavigateHelpAsync<HelpAddViewModel>();
        }
        
        public ICommand HelpConfigNavigateCommand { get; set; }
        
        private async Task HelpConfigNavigate()
        {
            await NavigationService.NavigateHelpAsync<HelpConfigViewModel>();
        }
    }
}
