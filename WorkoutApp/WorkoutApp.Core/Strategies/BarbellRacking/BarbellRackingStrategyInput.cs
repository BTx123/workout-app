using UnitsNet;
using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Strategies.BarbellRacking;

public class BarbellRackingStrategyInput
{
    /// <summary>
    /// The barbell to use.
    /// </summary>
    public IBarbell Barbell { get; init; } = Models.Barbell.StandardBarbell;

    /// <summary>
    /// The collection of available plates for racking.
    /// </summary>
    public IDictionary<Plate, int> AvailablePlates { get; init; } = new Dictionary<Plate, int>();

    /// <summary>
    /// The desired total weight (barbell + plates).
    /// </summary>
    public Mass DesiredWeight { get; init; } = Mass.Zero;

    /// <summary>
    /// Allow non-exact rack weight, returning the maximum plates not exceeding <see cref="DesiredWeight"/>). 
    /// </summary>
    public bool AllowRemainingWeight { get; init; } = false;
}