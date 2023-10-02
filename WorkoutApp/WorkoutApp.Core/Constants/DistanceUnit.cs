using UnitsNet.Units;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Constants;

public enum DistanceUnit
{
    Mile,
    Kilometer
}

public static class DistanceUnitExtensions
{
    public static LengthUnit ToLengthUnit(this DistanceUnit unit)
    {
        return unit switch
        {
            DistanceUnit.Mile => LengthUnit.Mile,
            DistanceUnit.Kilometer => LengthUnit.Kilometer,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    }

    public static LengthType ToLengthType(this DistanceUnit unit)
    {
        return unit switch
        {
            DistanceUnit.Mile => LengthType.Mile,
            DistanceUnit.Kilometer => LengthType.Kilometer,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    }
}