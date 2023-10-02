using WorkoutApp.Core.Constants;
using WorkoutApp.Core.Library;
using WorkoutApp.Core.Strategies.OneRepMax;

namespace WorkoutApp.Core.Factories;

public interface IOneRepMaxStrategyFactory : IFactory<OneRepMaxStrategy, IOneRepMaxStrategy<OneRepMaxStrategyInput>>
{
}