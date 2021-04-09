using PasswordManager.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MvvmHelpers;
using System.Windows;

namespace PasswordManager.Services
{
    public static class NavigationService
    {
        private static List<Window> Windows = new List<Window>();
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
            Windows.Add(View);
            View.Show();
        }

        public static async Task ClearStack()
        {
            MainWindowView.Current.Content.Children.Clear();
        }

    }
}
