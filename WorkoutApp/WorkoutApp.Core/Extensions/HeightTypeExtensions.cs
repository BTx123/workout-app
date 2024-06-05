using UnitsNet.Units;
using WorkoutApp.DAL.Constants;

namespace WorkoutApp.Core.Extensions;

public static class HeightTypeExtensions
{
    public static LengthUnit ToLengthUnit(this HeightType heightType)
    {
        return heightType switch
        {
            HeightType.Inch => LengthUnit.Inch,
            HeightType.Foot => LengthUnit.Foot,
            HeightType.Centimeter => LengthUnit.Centimeter,
            HeightType.Meter => LengthUnit.Meter,
            _ => throw new ArgumentOutOfRangeException(nameof(heightType), heightType, null)
        };
    }
}