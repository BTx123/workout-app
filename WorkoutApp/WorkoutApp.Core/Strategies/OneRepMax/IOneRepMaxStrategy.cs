using UnitsNet;
using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Strategies.OneRepMax;

public interface IOneRepMaxStrategy<in TInput> : IStrategy<TInput, Mass>
    where TInput : IOneRepMaxStrategyInput
{
}