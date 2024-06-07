using FluentResults;
using UnitsNet;
using WorkoutApp.Core.Constants;
using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Strategies.BarbellRacking;

public class MinimumPlateBarbellRackingStrategy : IBarbellRackingStrategy<BarbellRackingStrategyInput>
{
    public string Name => "minimum-plate-barbell-racking-strategy";

    public IResult<IDictionary<Mass, int>> Execute(BarbellRackingStrategyInput input)
    {
        // Validate desired weight not more than barbell
        var actualDesiredWeight = input.DesiredWeight - input.BarbellWeight;
        if (actualDesiredWeight < Mass.Zero)
        {
            return Result.Fail<IDictionary<Mass, int>>("Barbell weight exceeds desired weight");
        }

        // Return early if barbell weight matches desired weight
        var platesToRack = new Dictionary<Mass, int>();
        if (actualDesiredWeight.Equals(Mass.Zero, MassConstants.Tolerance))
        {
            return Result.Ok(platesToRack);
        }

        // Compute plates needed for one side
        var remainingWeightPerSide = actualDesiredWeight / 2;
        foreach (var weight in input.AvailablePlates.Keys.OrderByDescending(k => k))
        {
            // Get plate count, continue if minimum count of 2 not met
            var plateCount = input.AvailablePlates[weight];
            if (plateCount < 2) continue;

            // Compute weight to add per side
            var plateWeight = weight;
            if (plateWeight <= remainingWeightPerSide)
            {
                var maxPlateCountPerSide = (int)Math.Floor(remainingWeightPerSide / plateWeight);
                var platesToAddPerSide = Math.Min(maxPlateCountPerSide, plateCount);
                var weightToAddPerSide= Math.Min(platesToAddPerSide, plateCount) * plateWeight;
                remainingWeightPerSide -= weightToAddPerSide;
                platesToRack.Add(weight, 2*platesToAddPerSide);
            }

            // Break out of loop if no more weight needs to be added
            if (remainingWeightPerSide.Equals(Mass.Zero, MassConstants.Tolerance))
            {
                break;
            }
        }

        // Check all weight was added
        if (!input.AllowRemainingWeight && remainingWeightPerSide > Mass.Zero)
        {
            return Result.Fail<IDictionary<Mass, int>>($"Non-zero remaining weight per side: {remainingWeightPerSide}");
        }

        return Result.Ok(platesToRack);
    }
}