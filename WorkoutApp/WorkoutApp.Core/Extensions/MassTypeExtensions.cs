using UnitsNet.Units;
using WorkoutApp.DAL.Constants;

namespace WorkoutApp.Core.Extensions;

public static class MassTypeExtensions
{
    public static MassUnit ToMassUnit(this MassType massType)
    {
        return massType switch
        {
            MassType.Pound => MassUnit.Pound,
            MassType.Kilogram => MassUnit.Kilogram,
            _ => throw new ArgumentOutOfRangeException(nameof(massType), massType, null)
        };
    }
}