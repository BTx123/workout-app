using UnitsNet.Units;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Constants
{
    public static class MassTypeExtensions
    {
        public static MassUnit ToMassUnit(this MassType type)
        {
            return type switch
            {
                MassType.Pound => MassUnit.Pound,
                MassType.Kilogram => MassUnit.Kilogram,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
