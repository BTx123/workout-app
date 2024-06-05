using UnitsNet;
using WorkoutApp.Core.Library;
using WorkoutApp.Core.Models;
using WorkoutApp.Core.Strategies.BarbellRacking;
using Xunit.Abstractions;

namespace UnitTest.WorkoutApp.Core.Strategies;

public class MinimumPlateBarbellRackingStrategyTests
{
    private readonly IDictionary<Plate, int> _standardAvailablePlates = new Dictionary<Plate, int>
    {
        { new Plate { Weight = Mass.FromPounds(45) }, 100 },
        { new Plate { Weight = Mass.FromPounds(35) }, 100 },
        { new Plate { Weight = Mass.FromPounds(25) }, 100 },
        { new Plate { Weight = Mass.FromPounds(10) }, 100 },
        { new Plate { Weight = Mass.FromPounds(5) }, 100 },
        { new Plate { Weight = Mass.FromPounds(2.5) }, 100 },
    };

    private readonly IDictionary<Plate, int> _olympicAvailablePlates = new Dictionary<Plate, int>
    {
        { new Plate { Weight = Mass.FromKilograms(20) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(17.5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(15) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(12.5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(10) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(7.5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(2.5) }, 100 },
    };

    private readonly IDictionary<Plate, int> _alternativeOlympicAvailablePlates = new Dictionary<Plate, int>
    {
        { new Plate { Weight = Mass.FromKilograms(20) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(18.75) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(17.5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(16.25) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(15) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(13.75) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(12.5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(11.25) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(10) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(8.75) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(7.5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(6.25) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(4.75) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(2.5) }, 100 },
        { new Plate { Weight = Mass.FromKilograms(1.25) }, 100 },
    };

    private ITestOutputHelper Output { get; }

    public MinimumPlateBarbellRackingStrategyTests(ITestOutputHelper output)
    {
        Output = output;
    }

    private static MinimumPlateBarbellRackingStrategy GetStrategy()
    {
        return new MinimumPlateBarbellRackingStrategy();
    }

    private static string PlateDictionaryToString(IDictionary<Plate, int> dictionary)
    {
        if (dictionary.Count == 0) return "{ }";

        var s = dictionary.Select(d => $"{d.Value}x{d.Key.Weight}");
        return $"{{ {string.Join(", ", s)} }}";
    }

    [Fact]
    public void Execute_ZeroWeight_Fail()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.OlympicBarbell,
            AvailablePlates = _olympicAvailablePlates,
            DesiredWeight = Mass.FromKilograms(0)
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void Execute_NegativeWeight_Fail()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.AlternativeOlympicBarbell,
            AvailablePlates = _alternativeOlympicAvailablePlates,
            DesiredWeight = Mass.FromKilograms(-10)
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void Execute_LessThanBarbellWeight_Fail()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.OlympicBarbell,
            AvailablePlates = _olympicAvailablePlates,
            DesiredWeight = Barbell.OlympicBarbell.Weight / 2
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void Execute_NoPlates100kg_Fail()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.OlympicBarbell,
            AvailablePlates = new Dictionary<Plate, int>(),
            DesiredWeight = Mass.FromKilograms(100)
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void Execute_NoPlatesAllowRemaining_Ok()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.OlympicBarbell,
            AvailablePlates = new Dictionary<Plate, int>(),
            DesiredWeight = Mass.FromKilograms(100),
            AllowRemainingWeight = true
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsSuccess, result.ErrorMessage());
    }

    [Fact]
    public void Execute_NoPlatesBarbellWeight_Ok()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.OlympicBarbell,
            AvailablePlates = new Dictionary<Plate, int>(),
            DesiredWeight = Barbell.OlympicBarbell.Weight
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsSuccess, result.ErrorMessage());

        var solution = result.Value;
        Output.WriteLine(PlateDictionaryToString(solution));
        Assert.Empty(solution);
    }

    [Fact]
    public void Execute_Olympic100kg_Ok()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.OlympicBarbell,
            AvailablePlates = _olympicAvailablePlates,
            DesiredWeight = Mass.FromKilograms(100)
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsSuccess, result.ErrorMessage());

        var expected = new Dictionary<Plate, int>
        {
            { new Plate { Weight = Mass.FromKilograms(20) }, 4 },
        };
        var solution = result.Value;
        Output.WriteLine(PlateDictionaryToString(solution));
        foreach (var (key, value) in expected)
        {
            Assert.Contains(key, solution);
            Assert.Equal(expected[key], value);
        }
    }

    [Fact]
    public void Execute_AlternativeOlympic65kg_Ok()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.AlternativeOlympicBarbell,
            AvailablePlates = _alternativeOlympicAvailablePlates,
            DesiredWeight = Mass.FromKilograms(65)
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsSuccess, result.ErrorMessage());

        var expected = new Dictionary<Plate, int>
        {
            { new Plate { Weight = Mass.FromKilograms(20) }, 2 },
            { new Plate { Weight = Mass.FromKilograms(5) }, 2 },
        };
        var solution = result.Value;
        Output.WriteLine(PlateDictionaryToString(solution));
        foreach (var (key, value) in expected)
        {
            Assert.Contains(key, solution);
            Assert.Equal(expected[key], value);
        }
    }

    [Fact]
    public void Execute_Standard275lbPlates_Ok()
    {
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = Barbell.StandardBarbell,
            AvailablePlates = _standardAvailablePlates,
            DesiredWeight = Mass.FromPounds(275)
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsSuccess, result.ErrorMessage());

        var expected = new Dictionary<Plate, int>
        {
            { new Plate { Weight = Mass.FromPounds(45) }, 4 },
            { new Plate { Weight = Mass.FromPounds(25) }, 2 },
        };
        var solution = result.Value;
        Output.WriteLine(PlateDictionaryToString(solution));
        foreach (var (key, value) in expected)
        {
            Assert.Contains(key, solution);
            Assert.Equal(expected[key], value);
        }
    }

    [Fact]
    public void Execute_Olympic65kgNotEnoughPlates_Fail()
    {
        const int desiredWeight = 65;
        var strategy = GetStrategy();
        var input = new BarbellRackingStrategyInput
        {
            Barbell = new Barbell {Name = "None", Weight = Mass.FromPounds(0) },
            AvailablePlates = new Dictionary<Plate, int>
            {
                { new Plate { Weight = Mass.FromKilograms(1) }, desiredWeight - 1 },
            },
            DesiredWeight = Mass.FromKilograms(desiredWeight)
        };

        var result = strategy.Execute(input);
        Assert.True(result.IsFailed);
    }
}