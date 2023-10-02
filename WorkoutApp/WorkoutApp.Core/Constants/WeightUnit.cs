using UnitsNet.Units;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Constants;

public enum WeightUnit
{
    Pound,
    Kilogram
}

public static class MassUnitExtensions
{
    public static MassUnit ToMassUnit(this WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.Pound => MassUnit.Pound,
            WeightUnit.Kilogram => MassUnit.Kilogram,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    }

    public static MassType ToMassType(this WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.Pound => MassType.Pound,
            WeightUnit.Kilogram => MassType.Kilogram,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    }
}