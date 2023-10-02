using UnitsNet;

namespace WorkoutApp.Core.Strategies.OneRepMax;

public interface IOneRepMaxStrategyInput
{
    Mass Weight { get; }

    long Repetitions { get; }
}