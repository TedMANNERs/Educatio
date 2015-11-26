using System;
using System.Globalization;
using System.Windows.Data;
using TheNewEra.Util;

namespace TheNewEra
{
    public class AngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AngleUtils.ConvertToDegrees((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AngleUtils.ConvertToRadians((double)value);
        }
    }
}