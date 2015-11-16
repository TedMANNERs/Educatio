using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheNewEra
{
    public class FuelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double fuel = (int)value;
            double fuelScaled = fuel / Rocket.FuelTankSize * 100;
            return fuelScaled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}