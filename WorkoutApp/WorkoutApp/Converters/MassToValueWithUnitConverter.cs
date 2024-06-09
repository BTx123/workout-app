using System.Globalization;
using UnitsNet;
using UnitsNet.Units;

namespace WorkoutApp.Converters;

public class MassToValueWithUnitMultiConverter : IMultiValueConverter
{
    public object Convert(object[]? values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null) return BindableProperty.UnsetValue;
        if (values.Length != 2) return BindableProperty.UnsetValue;
        if (values[0] is not Mass mass) return BindableProperty.UnsetValue;
        if (values[1] is not MassUnit unit) return BindableProperty.UnsetValue;
        return mass.ToUnit(unit).Value;
    }

    public object[] ConvertBack(object value, Type[]? targetTypes, object parameter, CultureInfo culture)
    {
        if (value is not double massValue) return [BindableProperty.UnsetValue, BindableProperty.UnsetValue];
        if (targetTypes == null) return [BindableProperty.UnsetValue, BindableProperty.UnsetValue];
        if (targetTypes.Length != 2) return [BindableProperty.UnsetValue, BindableProperty.UnsetValue];
        if (!targetTypes[0].IsAssignableFrom(typeof(Mass))) return [BindableProperty.UnsetValue, BindableProperty.UnsetValue];
        if (!targetTypes[1].IsAssignableFrom(typeof(MassUnit))) return [BindableProperty.UnsetValue, BindableProperty.UnsetValue];
        return [Mass.From(massValue, MassUnit.Kilogram), MassUnit.Kilogram];
    }
}