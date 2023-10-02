using System.Globalization;
using UnitsNet.Units;
using UnitsNet;

namespace WorkoutApp.Converters;

public class LengthToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Enum length) return null;
        return length.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string s && UnitsNetSetup.Default.UnitParser.TryParse(s, typeof(LengthUnit), out var unit))
        {
            return unit;
        }

        return null;
    }
}