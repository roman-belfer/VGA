using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Common.MVVM.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        // Example of usage:
        // Visibility="{Binding Path=IsVisible, Converter={x:Static converters:BooleanToVisibilityConverter.FalseToCollapsed}}"
        public static BooleanToVisibilityConverter FalseToCollapsed { get; private set; }
        public static BooleanToVisibilityConverter FalseToHidden { get; private set; }
        public static BooleanToVisibilityConverter TrueToCollapsed { get; private set; }
        public static BooleanToVisibilityConverter TrueToHidden { get; private set; }
        public static BooleanToVisibilityConverter NullToCollapsed { get; private set; }
        public static BooleanToVisibilityConverter NullToVisible { get; private set; }

        public Visibility VisibilityState { get; private set; }
        public Visibility AlternativeState { get; private set; }
        public bool Invert { get; private set; }
        public object Value { get; private set; }

        static BooleanToVisibilityConverter()
        {
            FalseToCollapsed = new BooleanToVisibilityConverter();

            FalseToHidden = new BooleanToVisibilityConverter
            {
                VisibilityState = Visibility.Hidden
            };
            TrueToCollapsed = new BooleanToVisibilityConverter
            {
                Value = true,
                Invert = true
            };
            TrueToHidden = new BooleanToVisibilityConverter
            {
                Value = true,
                Invert = true,
                VisibilityState = Visibility.Hidden
            };
            NullToVisible = new BooleanToVisibilityConverter()
            {
                Value = null,
                VisibilityState = Visibility.Visible,
                AlternativeState = Visibility.Collapsed
            };
            NullToCollapsed = new BooleanToVisibilityConverter()
            {
                Value = null,
                VisibilityState = Visibility.Collapsed
            };
        }

        public BooleanToVisibilityConverter()
        {
            VisibilityState = Visibility.Collapsed;
            AlternativeState = Visibility.Visible;
            Value = false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && Value is bool)
            {
                return (bool)value == (bool)Value ? VisibilityState : AlternativeState;
            }
            else
                return Value == value ? VisibilityState : AlternativeState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
