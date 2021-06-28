using PasswordManagerCore.Modules;
using PasswordManagerCore.Resources;
using PasswordManagerCore.Services;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PasswordManagerCore.Converters
{
    public class CrypthographyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string Password)
            {
                return CryptographyService.DecryptData(Password, MainWindowView.Current.InstanceVariables.CrypthographyKey);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string Password)
            {
                return CryptographyService.EncryptData(Password, MainWindowView.Current.InstanceVariables.CrypthographyKey);
            }
            return null;
        }
    }
}
