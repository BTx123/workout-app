using UnitsNet;
using WorkoutApp.Core.Library;
using WorkoutApp.Core.Strategies.OneRepMax;
using Xunit.Abstractions;

namespace UnitTest.WorkoutApp.Core.Strategies;

public class OneRepMaxStrategyTests
{
    public ITestOutputHelper Output { get; }

    public OneRepMaxStrategyTests(ITestOutputHelper output)
    {
        Output = output;
    }

    private IEnumerable<IOneRepMaxStrategy<OneRepMaxStrategyInput>> GetStrategies()
    {
        return new List<IOneRepMaxStrategy<OneRepMaxStrategyInput>>
        {
            new BrzyckiOneRepMaxStrategy(),
            new EpleyOneRepMaxStrategy(),
            new LandersOneRepMaxStrategy()
        };
    }

    [Theory]
    [InlineData(1, -1)]
    [InlineData(10, -10)]
    [InlineData(100, -100)]
    [InlineData(-1, 1)]
    [InlineData(-10, 10)]
    [InlineData(-100, 100)]
    public void Execute_BadInput_Fail(int weight, int reps)
    {
        var strategies = GetStrategies();
        var input = new OneRepMaxStrategyInput
        {
            Weight = Mass.FromKilograms(weight),
            Repetitions = reps
        };
        Output.WriteLine("Weight: {0}, Reps: {1}", input.Weight, input.Repetitions);

        foreach (var strategy in strategies)
        {
            var result = strategy.Execute(input);
            Assert.True(result.IsFailed);
        }
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(100, 0)]
    [InlineData(0, 100)]
    [InlineData(1, 1)]
    [InlineData(100, 1)]
    [InlineData(1, 100)]
    public void Execute_OneRepMaxInput_Ok(int weight, int reps)
    {
        var strategies = GetStrategies();
        var input = new OneRepMaxStrategyInput
        {
            Weight = Mass.FromKilograms(weight),
            Repetitions = reps
        };
        Output.WriteLine("Weight: {0}, Reps: {1}", input.Weight, input.Repetitions);

        foreach (var strategy in strategies)
        {
            var result = strategy.Execute(input);
            Assert.True(result.IsSuccess, result.ErrorMessage());

            var oneRepMax = result.Value;
            Output.WriteLine("{0}: {1}", strategy.Name, oneRepMax);
        }
    }

    [Theory]
    [InlineData(100, 2)]
    [InlineData(66.6, 10)]
    [InlineData(1000, 5)]
    public void Execute_HigherThanOneRepMaxInput_Ok(int weight, int reps)
    {
        var strategies = GetStrategies();
        var input = new OneRepMaxStrategyInput
        {
            Weight = Mass.FromKilograms(weight),
            Repetitions = reps
        };
        Output.WriteLine("Weight: {0}, Reps: {1}", input.Weight, input.Repetitions);

        foreach (var strategy in strategies)
        {
            var result = strategy.Execute(input);
            Assert.True(result.IsSuccess, result.ErrorMessage());

            var oneRepMax = result.Value;
            Assert.True(oneRepMax > input.Weight, $"{strategy.Name} predicted 1RM {oneRepMax} not more than lift {input.Weight} for reps > 1");
            Output.WriteLine("{0}: {1}", strategy.Name, oneRepMax);
        }
    }
}