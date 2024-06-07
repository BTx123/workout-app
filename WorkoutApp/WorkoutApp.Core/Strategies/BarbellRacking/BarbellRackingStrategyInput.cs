using UnitsNet;
using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Strategies.BarbellRacking;

public class BarbellRackingStrategyInput
{
    /// <summary>
    /// The barbell weight to use.
    /// </summary>
    public Mass BarbellWeight { get; init; } = Barbell.StandardBarbell.Weight;

    /// <summary>
    /// The collection of available plates for racking.
    /// </summary>
    public IDictionary<Mass, int> AvailablePlates { get; init; } = new Dictionary<Mass, int>();

    /// <summary>
    /// The desired total weight (barbell + plates).
    /// </summary>
    public Mass DesiredWeight { get; init; } = Mass.Zero;

    /// <summary>
    /// Allow non-exact rack weight, returning the maximum plates not exceeding <see cref="DesiredWeight"/>).
    /// </summary>
    public bool AllowRemainingWeight { get; init; } = false;
}