using System.Globalization;
using UnitsNet;

namespace WorkoutApp.Converters;

public class MassToValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Mass mass) return null;
        return mass.Value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string massString) return null;
        if (!Mass.TryParse(massString, out var mass)) return null;
        return mass;
    }
}