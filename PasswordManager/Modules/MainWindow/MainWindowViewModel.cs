using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using PasswordManager.Services;

namespace PasswordManager.Modules
{
    public class MainWindowViewModel:BaseViewModel
    {

        public MainWindowViewModel()
        {
            TesteCommand = new MvvmHelpers.Commands.AsyncCommand(Teste);
        }

        public ICommand TesteCommand { get; set; }

        private async Task Teste()
        {
            await NavigationService.NavigateAsync<HomeViewModel>();
        }
    }
}
