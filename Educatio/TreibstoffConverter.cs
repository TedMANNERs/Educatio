using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Educatio
{
    public class TreibstoffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double treibstoff = (int)value;
            double treibstoffSkaliert = treibstoff / Rakete.TankGrösse * 100;
            return treibstoffSkaliert;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}