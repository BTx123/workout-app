using UnitsNet;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Models;

public interface IPlate<T> : IHasId, IEquatable<T>, IComparable
    where T : IPlate<T>
{
    public Mass Weight { get; }
}