using FluentResults;
using UnitsNet;
using WorkoutApp.Core.Constants;
using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Strategies.BarbellRacking;

public class MinimumPlateBarbellRackingStrategy : IBarbellRackingStrategy<BarbellRackingStrategyInput>
{
    public string Name => "minimum-plate-barbell-racking-strategy";

    public IResult<IDictionary<Plate, int>> Execute(BarbellRackingStrategyInput input)
    {
        // Validate desired weight not more than barbell
        var actualDesiredWeight = input.DesiredWeight - input.Barbell.Weight;
        if (actualDesiredWeight < Mass.Zero)
        {
            return Result.Fail<IDictionary<Plate, int>>("Barbell weight exceeds desired weight");
        }

        // Return early if barbell weight matches desired weight
        var platesToRack = new Dictionary<Plate, int>();
        if (actualDesiredWeight.Equals(Mass.Zero, Unit.MassTolerance))
        {
            return Result.Ok(platesToRack);
        }

        // Compute plates needed for one side
        var remainingWeightPerSide = actualDesiredWeight / 2;
        foreach (var plate in input.AvailablePlates.Keys.OrderByDescending(k => k.Weight))
        {
            // Get plate count, continue if minimum count of 2 not met
            var plateCount = input.AvailablePlates[plate];
            if (plateCount < 2) continue;
            
            // Compute weight to add per side
            var plateWeight = plate.Weight;
            if (plateWeight <= remainingWeightPerSide)
            {
                var maxPlateCountPerSide = (int)Math.Floor(remainingWeightPerSide / plateWeight);
                var platesToAddPerSide = Math.Min(maxPlateCountPerSide, plateCount);
                var weightToAddPerSide= Math.Min(platesToAddPerSide, plateCount) * plateWeight;
                remainingWeightPerSide -= weightToAddPerSide;
                platesToRack.Add(plate, 2*platesToAddPerSide);
            }

            // Break out of loop if no more weight needs to be added
            if (remainingWeightPerSide.Equals(Mass.Zero, Unit.MassTolerance))
            {
                break;
            }
        }

        // Check all weight was added
        if (!input.AllowRemainingWeight && remainingWeightPerSide > Mass.Zero)
        {
            return Result.Fail<IDictionary<Plate, int>>($"Non-zero remaining weight per side: {remainingWeightPerSide}");
        }

        return Result.Ok(platesToRack);
    }
}