using UnitsNet;

namespace WorkoutApp.Core.Models;

public interface IPlate<T> : IEquatable<T>, IComparable
    where T : IPlate<T>
{
    public Mass Weight { get; }
}