using FluentResults;
using UnitsNet;

namespace WorkoutApp.Core.Strategies.OneRepMax;

public class LandersOneRepMaxStrategy : IOneRepMaxStrategy<IOneRepMaxStrategyInput>
{
    public string Name => "landers-one-rep-max-strategy";

    public IResult<Mass> Execute(IOneRepMaxStrategyInput input)
    {
        if (input.Weight < Mass.Zero) return Result.Fail<Mass>($"Weight lifted {input.Weight} less than 0");
        if (input.Repetitions < 0) return Result.Fail<Mass>($"Repetitions {input.Repetitions} less than 0");

        var oneRepMax = 100 * input.Weight / (101.3 - 2.67123 * input.Repetitions);
        return Result.Ok(oneRepMax);
    }
}