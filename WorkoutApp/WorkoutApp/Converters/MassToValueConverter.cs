using System.Globalization;
using UnitsNet;
using UnitsNet.Units;

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
        if (value is not double massValue) return null;
        return Mass.FromKilograms(massValue);
    }
}