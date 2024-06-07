using FluentResults;
using WorkoutApp.Core.Strategies.OneRepMax;
using WorkoutApp.DAL.Constants;

namespace WorkoutApp.Core.Factories;

public class OneRepMaxStrategyFactory : IOneRepMaxStrategyFactory
{
    public IResult<IOneRepMaxStrategy<OneRepMaxStrategyInput>> Create(OneRepMaxStrategy input)
    {
        return input switch
        {
            OneRepMaxStrategy.Brzycki => Result.Ok(new BrzyckiOneRepMaxStrategy()),
            OneRepMaxStrategy.Epley => Result.Ok(new EpleyOneRepMaxStrategy()),
            OneRepMaxStrategy.Landers => Result.Ok(new LandersOneRepMaxStrategy()),
            _ => Result.Fail<IOneRepMaxStrategy<OneRepMaxStrategyInput>>("Invalid one rep max strategy")
        };
    }
}