using UnitsNet;
using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Strategies.BarbellRacking;

public interface IBarbellRackingStrategy<in TInput> : IStrategy<TInput, IDictionary<Mass, int>>
    where TInput : BarbellRackingStrategyInput
{
}
