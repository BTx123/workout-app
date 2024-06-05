using WorkoutApp.DAL.Constants;

namespace WorkoutApp.DAL.Entities;

public class AppSetting : EntityBase
{
    public static AppSetting Default = new();

    public Theme Theme { get; set; } = Constants.Theme.System;

    public bool KeepScreenOn { get; set; } = false;

    public bool LockScreenOrientation { get; set; } = false;

    public DayOfWeek FirstDayOfWeek { get; set; } = DayOfWeek.Sunday;

    public MassType WeightUnit { get; set; } = MassType.Pound;

    public DistanceType DistanceUnit { get; set; } = DistanceType.Mile;

    public HeightType HeightUnit { get; set; } = HeightType.Inch;

    public OneRepMaxStrategy OneRepMaxStrategy { get; set; } = Constants.OneRepMaxStrategy.Brzycki;

    public bool ShowAssistanceExercises { get; set; } = true;

    public bool ShowRepsToBeat1Rm { get; set; } = true;

    public bool ShowRepsToBeatPr { get; set; } = true;

    public SupinationStrategy SupinationStrategy { get; set; } = Constants.SupinationStrategy.None;
}
