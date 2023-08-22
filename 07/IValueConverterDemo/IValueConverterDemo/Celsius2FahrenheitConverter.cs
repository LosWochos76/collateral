using System;
using System.Globalization;
using System.Windows.Data;

namespace IValueConverterDemo;

class Celsius2FahrenheitConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return System.Convert.ToDouble(value) * 1.8 + 32;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (System.Convert.ToDouble(value) - 32) / 1.8;
    }
}
