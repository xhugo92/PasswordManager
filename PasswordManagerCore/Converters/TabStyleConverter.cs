using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PasswordManagerCore.Converters
{
    class TabStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string currentTab = value?.ToString();
            string tab = parameter?.ToString();

            if(currentTab == tab)
            {
                return Application.Current.FindResource("ActiveTabButtonStyle") as Style;
            }

            return Application.Current.FindResource("TabButtonStyle") as Style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // Não vamos usar de volta
        }
    }
}
