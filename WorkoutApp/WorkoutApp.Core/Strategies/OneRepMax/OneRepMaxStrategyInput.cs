using UnitsNet;

namespace WorkoutApp.Core.Strategies.OneRepMax;

public class OneRepMaxStrategyInput : IOneRepMaxStrategyInput
{
    public Mass Weight { get; init; } = Mass.Zero;

    public long Repetitions { get; init; } = 0;
}