using System;
using System.Globalization;
using System.Windows.Data;

namespace VGA.Index.Converters
{
    public class IsMaleToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isMale = (bool)value;

            return isMale ? "М" : "Ж";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
