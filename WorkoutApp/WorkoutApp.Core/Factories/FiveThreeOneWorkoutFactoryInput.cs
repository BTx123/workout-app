//using UnitsNet;
//using WorkoutApp.Core.Library;
//using WorkoutApp.Core.Models;

//namespace WorkoutApp.Core.Factories;

//public class FiveThreeOneWorkoutFactoryInput : IWorkoutFactoryInput
//{
//    public DateTime StartDay { get; init; } = DateTime.Today;

//    /// <summary>
//    /// 
//    /// </summary>
//    public IDictionary<DayOfWeek, IEnumerable<ExerciseSetup>> WorkoutSetup { get; init; } =
//        new Dictionary<DayOfWeek, IEnumerable<ExerciseSetup>>();

//    /// <summary>
//    /// Number of cycles.
//    /// </summary>
//    public int NumberOfCycles { get; init; } = 0;

//    /// <summary>
//    /// 
//    /// </summary>
//    public PercentageScheme PercentageScheme { get; init; } = PercentageScheme.Standard;
//}

//public class ExerciseSetup
//{
//    /// <summary>
//    /// Exercise.
//    /// </summary>
//    public Exercise Exercise { get; init; } = new();

//    /// <summary>
//    /// Estimated 1RM for the exercise.
//    /// </summary>
//    public Mass EstimatedOneRepMax { get; init; } = Mass.Zero;

//    /// <summary>
//    /// Increment to increase training max for this exercise.
//    /// </summary>
//    public Mass Increment { get; init; } = Mass.Zero;
//}

//public class PercentageScheme : INamed
//{
//    public static PercentageScheme Standard { get; } = new()
//    {
//        Name = "Standard",
//        Scheme = new List<List<Ratio>>
//        {
//            new()
//            {
//                Ratio.FromPercent(65),
//                Ratio.FromPercent(75),
//                Ratio.FromPercent(85),
//            },
//            new()
//            {
//                Ratio.FromPercent(70),
//                Ratio.FromPercent(80),
//                Ratio.FromPercent(90),
//            },
//            new()
//            {
//                Ratio.FromPercent(75),
//                Ratio.FromPercent(85),
//                Ratio.FromPercent(95),
//            },
//            new()
//            {
//                Ratio.FromPercent(40),
//                Ratio.FromPercent(50),
//                Ratio.FromPercent(60),
//            }
//        }
//    };

//    public static PercentageScheme Aggressive { get; } = new()
//    {
//        Name = "Aggressive",
//        Scheme = new List<List<Ratio>>
//        {
//            new()
//            {
//                Ratio.FromPercent(75),
//                Ratio.FromPercent(80),
//                Ratio.FromPercent(85),
//            },
//            new()
//            {
//                Ratio.FromPercent(80),
//                Ratio.FromPercent(85),
//                Ratio.FromPercent(90),
//            },
//            new()
//            {
//                Ratio.FromPercent(75),
//                Ratio.FromPercent(85),
//                Ratio.FromPercent(95),
//            },
//            new()
//            {
//                Ratio.FromPercent(40),
//                Ratio.FromPercent(50),
//                Ratio.FromPercent(60),
//            }
//        }
//    };

//    public string Name { get; init; }

//    public IEnumerable<IEnumerable<Ratio>> Scheme { get; init; }
//}
