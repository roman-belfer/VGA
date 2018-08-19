using System;
using System.Globalization;
using System.Windows.Data;
using Aurora.Install.Bootstrapper.Common.Localizer.Helpers;

namespace Aurora.Install.Bootstrapper.Common.Localizer.Converters
{
    public class TranslateParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = parameter?.ToString();
            var translated = text.Translate();
            return translated;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
