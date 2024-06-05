using WorkoutApp.Core.Library;
using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Strategies.BarbellRacking;

public interface IBarbellRackingStrategy<in TInput> : IStrategy<TInput, IDictionary<Plate, int>>
    where TInput : BarbellRackingStrategyInput
{
}
