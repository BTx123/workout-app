using FluentResults;
using UnitsNet;

namespace WorkoutApp.Core.Strategies.OneRepMax;

public class BrzyckiOneRepMaxStrategy : IOneRepMaxStrategy<IOneRepMaxStrategyInput>
{
    public string Name => "brzycki-one-rep-max-strategy";

    public IResult<Mass> Execute(IOneRepMaxStrategyInput input)
    {
        if (input.Weight < Mass.Zero) return Result.Fail<Mass>($"Weight lifted {input.Weight} less than 0");
        if (input.Repetitions < 0) return Result.Fail<Mass>($"Repetitions {input.Repetitions} less than 0"); 
        
        var oneRepMax = input.Weight * (36d / (37 - input.Repetitions));
        return Result.Ok(oneRepMax);
    }
}