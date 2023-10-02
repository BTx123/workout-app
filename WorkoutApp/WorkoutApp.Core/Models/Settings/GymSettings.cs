using UnitsNet;

namespace WorkoutApp.Core.Models.Settings;

[Serializable]
public class GymSettings
{
    /// <summary>
    /// Available barbells.
    /// </summary>
    public ICollection<IBarbell> AvailableBarbells { get; } = new List<IBarbell>
    {
        Barbell.StandardBarbell,
        Barbell.OlympicBarbell,
        Barbell.SafetySquatBar,
        Barbell.EzCurlBar
    };

    public Barbell DefaultBarbell { get; set; } = Barbell.StandardBarbell;

    /// <summary>
    /// Available plates for loading barbells.
    /// </summary>
    public IDictionary<Mass, int> AvailablePlates { get; } = new Dictionary<Mass, int>
    {
        { Mass.FromPounds(45), 10 },
        { Mass.FromPounds(35), 10 },
        { Mass.FromPounds(25), 10 },
        { Mass.FromPounds(10), 10 },
        { Mass.FromPounds(5), 10 },
        { Mass.FromPounds(2.5), 10 },
    };
}