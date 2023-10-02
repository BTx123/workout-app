using System.Numerics;
using UnitsNet;
using WorkoutApp.Core.Constants;

namespace WorkoutApp.Core.Models;

public class Plate : IEquatable<Plate>, IComparable<Plate>
{
    private readonly Mass _weight;

    public Mass Weight
    {
        get => _weight;
        init
        {
            if (value < Mass.Zero)
                throw new ArgumentException("Plate must have positive, non-zero weight");
            _weight = value;
        }
    }

    public override string ToString()
    {
        return Weight.ToString();
    }

    public bool Equals(Plate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Weight.Equals(other.Weight, Unit.MassTolerance);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj is Plate other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _weight.GetHashCode();
    }

    public int CompareTo(Plate? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return _weight.CompareTo(other._weight);
    }
}