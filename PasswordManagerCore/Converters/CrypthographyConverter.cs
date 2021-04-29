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
                return CryptographyService.DecryptData(Password);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string Password)
            {
                return CryptographyService.EncryptData(Password);
            }
            return null;
        }
    }
}
