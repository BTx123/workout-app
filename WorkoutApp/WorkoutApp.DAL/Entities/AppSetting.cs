using System.ComponentModel.DataAnnotations.Schema;
using WorkoutApp.DAL.Constants;

namespace WorkoutApp.DAL.Entities;

public class AppSetting : EntityBase
{
    [NotMapped]
    public static AppSetting Default = new();

    [Column("theme")]
    public Theme Theme { get; set; } = Constants.Theme.System;

    [Column("keep_screen_on")]
    public bool KeepScreenOn { get; set; } = false;

    [Column("lock_screen_orientation")]
    public bool LockScreenOrientation { get; set; } = false;

    [Column("first_day_of_week")]
    public DayOfWeek FirstDayOfWeek { get; set; } = DayOfWeek.Sunday;

    [Column("weight_unit")]
    public MassType WeightUnit { get; set; } = MassType.Pound;

    [Column("distance_unit")]
    public DistanceType DistanceUnit { get; set; } = DistanceType.Mile;

    [Column("height_unit")]
    public HeightType HeightUnit { get; set; } = HeightType.Inch;

    [Column("one_rep_max_strategy")]
    public OneRepMaxStrategy OneRepMaxStrategy { get; set; } = OneRepMaxStrategy.Brzycki;

    [Column("show_assistance_exercises")]
    public bool ShowAssistanceExercises { get; set; } = true;

    [Column("show_reps_to_beat_1rm")]
    public bool ShowRepsToBeat1Rm { get; set; } = true;

    [Column("show_reps_to_beat_pr")]
    public bool ShowRepsToBeatPr { get; set; } = true;

    [Column("supination_strategy")]
    public SupinationStrategy SupinationStrategy { get; set; } = SupinationStrategy.None;
}
