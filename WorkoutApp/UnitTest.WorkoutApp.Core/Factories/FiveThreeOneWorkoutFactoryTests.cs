//using UnitsNet;
//using WorkoutApp.Core.Factories;
//using WorkoutApp.Core.Library;
//using WorkoutApp.Core.Models;
//using Xunit.Abstractions;

//namespace UnitTest.WorkoutApp.Core.Factories;

//public class FiveThreeOneWorkoutFactoryTests
//{
//    private ITestOutputHelper Output { get; }
    
//    public FiveThreeOneWorkoutFactoryTests(ITestOutputHelper output)
//    {
//        Output = output;
//    }

//    private FiveThreeOneWorkoutPlanFactory GetFactory()
//    {
//        return new FiveThreeOneWorkoutPlanFactory();
//    }

//    [Fact]
//    public void Create_ValidInput_Ok()
//    {
//        var factory = GetFactory();
//        var input = new FiveThreeOneWorkoutFactoryInput
//        {
//            NumberOfCycles = 12,
//            PercentageScheme = PercentageScheme.Standard,
//            StartDay = DateTime.Today,
//            WorkoutSetup = new Dictionary<DayOfWeek, IEnumerable<ExerciseSetup>>
//            {
//                {
//                    DayOfWeek.Monday, new[]
//                    {
//                        new ExerciseSetup
//                        {
//                            Exercise = Exercise.BenchPress,
//                            Increment = Mass.FromPounds(5),
//                            EstimatedOneRepMax = Mass.FromPounds(135)
//                        }
//                    }
//                },
//                {
//                    DayOfWeek.Wednesday, new[]
//                    {
//                        new ExerciseSetup
//                        {
//                            Exercise = Exercise.Squat,
//                            Increment = Mass.FromPounds(10),
//                            EstimatedOneRepMax = Mass.FromPounds(225)
//                        }
//                    }
//                },
//                {
//                    DayOfWeek.Friday, new[]
//                    {
//                        new ExerciseSetup
//                        {
//                            Exercise = Exercise.Deadlift,
//                            Increment = Mass.FromPounds(10),
//                            EstimatedOneRepMax = Mass.FromPounds(315)
//                        },
//                        new ExerciseSetup
//                        {
//                            Exercise = Exercise.MilitaryPress,
//                            Increment = Mass.FromPounds(5),
//                            EstimatedOneRepMax = Mass.FromPounds(95)
//                        }
//                    }
//                }
//            }
//        };

//        var result = factory.Create(input);
//        Assert.True(result.IsSuccess, result.ErrorMessage());

//        var workoutPlan = result.Value;
//        Assert.Equal(input.NumberOfCycles * input.WorkoutSetup.Keys.Count, workoutPlan.Workouts.Count());
//    }
//}