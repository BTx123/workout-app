using UnitsNet.Units;
using WorkoutApp.DAL.Constants;

namespace WorkoutApp.Core.Extensions;

public static class DistanceTypeExtensions
{
    public static LengthUnit ToLengthUnit(this DistanceType distanceType)
    {
        return distanceType switch
        {
            DistanceType.Mile => LengthUnit.Mile,
            DistanceType.Kilometer => LengthUnit.Kilometer,
            _ => throw new ArgumentOutOfRangeException(nameof(distanceType), distanceType, null)
        };
    }
}