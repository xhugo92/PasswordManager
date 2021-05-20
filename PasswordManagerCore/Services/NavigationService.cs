using MvvmHelpers;
using PasswordManagerCore.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManagerCore.Services
{
    public static class NavigationService
    {
        public static bool HasPopupOpen = false;

        public static List<Window> Popups { get; } = new List<Window>();


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
            Popups.Add(View);
            View.Owner = MainWindowView.Current;
            View.Show();
        }

        public static async Task PopAsync()
        {
            var actualChildren = MainWindowView.Current.Content.Children;
            actualChildren.Remove(actualChildren[actualChildren.Count - 1]);
        }

        public static async Task ClosePopup<T>() where T : BaseViewModel
        {
            Window openedPopup = FindPopupWindow<T>();
            if (openedPopup != null)
            {
                Popups.Remove(openedPopup);
                openedPopup.Close();
            }
        }

        public static Window FindPopupWindow<T>() where T : BaseViewModel
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
            return openedPopup;

        }

        public static async Task CloseAllWindows()
        {
            App.Current.Shutdown();
        }
    }
}
