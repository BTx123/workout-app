using WorkoutApp.Core.Library;
using WorkoutApp.Core.Strategies.OneRepMax;
using WorkoutApp.DAL.Constants;

namespace WorkoutApp.Core.Factories;

public interface IOneRepMaxStrategyFactory : IFactory<OneRepMaxStrategy, IOneRepMaxStrategy<OneRepMaxStrategyInput>>
{
}