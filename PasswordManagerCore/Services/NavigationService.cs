using PasswordManagerCore.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using MvvmHelpers;
using System.Windows;

namespace PasswordManagerCore.Services
{
    public static class NavigationService
    {
        public static bool HasPopupOpen = false;

        private static List<Window> Popups = new List<Window>();

        public static async Task NavigateAsync<T>(params object[] parameters) where T : BaseViewModel
        {
            string name = typeof(T).AssemblyQualifiedName;
            Type ViewType = Type.GetType(name.Replace("ViewModel", "View"));

            IEnumerable<Type> paramTypes = parameters.Select(parameter => parameter.GetType());
            ConstructorInfo ViewModelConstructor = typeof(T).GetConstructor(paramTypes.ToArray());
            object ViewModel = ViewModelConstructor.Invoke(parameters);
            ConstructorInfo ViewConstructor = ViewType.GetConstructor(Type.EmptyTypes);
            UserControl View = (UserControl)ViewConstructor.Invoke(null);
            View.DataContext = ViewModel;
            //TODO: Quando colocar o background no user control pode tirar o clear para implementar o botão de back
            MainWindowView.Current.Content.Children.Clear();
            MainWindowView.Current.Content.Children.Add(View);
        }

        public static async Task OpenNewWindowAsync<T>(params object[] parameters) where T : BaseViewModel
        {
            string name = typeof(T).AssemblyQualifiedName;
            Type ViewType = Type.GetType(name.Replace("ViewModel", "View"));

            IEnumerable<Type> paramTypes = parameters.Select(parameter => parameter.GetType());
            ConstructorInfo ViewModelConstructor = typeof(T).GetConstructor(paramTypes.ToArray());
            object ViewModel = ViewModelConstructor.Invoke(parameters);
            ConstructorInfo ViewConstructor = ViewType.GetConstructor(Type.EmptyTypes);
            Window View = (Window)ViewConstructor.Invoke(null);
            View.DataContext = ViewModel;
            if (typeof(T) == typeof(GenericPopupViewModel) || typeof(T) == typeof(NotificationPopupViewModel))
            {
                Popups.Add(View);
            }
            View.Show();
        }

        public static async Task ClosePopup<T>() where T : BaseViewModel
        {
            Window openedPopup = null;
            foreach (Window popup in Popups)
            {
                if (popup.DataContext.GetType() == typeof(T))
                {
                    openedPopup = popup;
                    break;
                }
            }
            if (openedPopup != null)
            {
                Popups.Remove(openedPopup);
                openedPopup.Close();
            }
        }

        public static async Task CloseAllWindows()
        {
            App.Current.Shutdown();
        }
    }
}
