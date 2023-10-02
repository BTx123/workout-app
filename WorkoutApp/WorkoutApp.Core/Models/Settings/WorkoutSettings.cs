using WorkoutApp.Core.Constants;

namespace WorkoutApp.Core.Models.Settings;

[Serializable]
public class WorkoutSettings
{
    /// <summary>
    /// Show assistance exercises.
    /// </summary>
    public bool ShowAssistanceExercises { get; set; } = true;

    /// <summary>
    /// Show reps to beat 1RM.
    /// </summary>
    public bool ShowRepsToBeat1Rm { get; set; } = true;

    /// <summary>
    /// Show reps to beat PR.
    /// </summary>
    public bool ShowRepsToBeatPr { get; set; } = true;

    /// <summary>
    /// Deadlift supination.
    /// </summary>
    public bool EnableDeadliftSupination { get; set; } = false;

    /// <summary>
    /// Deadlift supination strategy.
    /// </summary>
    public SupinationStrategy SupinationStrategy { get; set; } = SupinationStrategy.AlternateDays;
}