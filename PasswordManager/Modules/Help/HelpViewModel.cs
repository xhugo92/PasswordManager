using MvvmHelpers;
using PasswordManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Modules
{
    public class HelpViewModel:BaseViewModel
    {
        public HelpViewModel()
        {
            CloseWindowCommand = new MvvmHelpers.Commands.AsyncCommand(CloseWindow);
        }

        public ICommand CloseWindowCommand { get; set; }

        private async Task CloseWindow()
        {
            await NavigationService.CloseWindow();
        }
    }
}
