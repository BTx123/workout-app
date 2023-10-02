using UnitsNet.Units;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Constants;

public enum HeightUnit
{
    Inch,
    Foot,
    Centimeter,
    Meter
}

public static class HeightUnitExtensions
{
    public static LengthUnit ToLengthUnit(this HeightUnit unit)
    {
        return unit switch
        {
            HeightUnit.Inch => LengthUnit.Inch,
            HeightUnit.Foot => LengthUnit.Foot,
            HeightUnit.Centimeter => LengthUnit.Centimeter,
            HeightUnit.Meter => LengthUnit.Meter,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    }

    public static LengthType ToLengthType(this HeightUnit unit)
    {
        return unit switch
        {
            HeightUnit.Inch => LengthType.Inch,
            HeightUnit.Foot => LengthType.Foot,
            HeightUnit.Centimeter => LengthType.Centimeter,
            HeightUnit.Meter => LengthType.Meter,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    }
}