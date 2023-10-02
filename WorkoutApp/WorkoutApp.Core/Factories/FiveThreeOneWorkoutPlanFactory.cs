//using FluentResults;
//using UnitsNet;
//using WorkoutApp.Core.Models;

//namespace WorkoutApp.Core.Factories;

//public class FiveThreeOneWorkoutPlanFactory : IWorkoutPlanFactory<FiveThreeOneWorkoutFactoryInput, FiveThreeOneWorkoutPlan>
//{
//    public static readonly Ratio TrainingMaxPercentage = Ratio.FromPercent(90);

//    public IResult<FiveThreeOneWorkoutPlan> Create(FiveThreeOneWorkoutFactoryInput input)
//    {
//        if (input.WorkoutSetup.Count is < 2 or > 4)
//            return Result.Fail<FiveThreeOneWorkoutPlan>("Number of training days must be between 2 and 4");

//        if (input.NumberOfCycles < 0)
//            return Result.Fail<FiveThreeOneWorkoutPlan>("Number of cycles cannot be less than 0");

//        var workouts = new List<WorkoutSetup>();
//        var sortedWorkoutSetup = input.WorkoutSetup.OrderBy(w => w.Key).ToList();
//        var lastWorkoutDay = input.StartDay;
//        for (var cycle = 0; cycle < input.NumberOfCycles; cycle++)
//        {
//            foreach (var scheme in input.PercentageScheme.Scheme)
//            {

//            }
//            for (var week = 0; week < input.PercentageScheme.Scheme.Count(); week++)
//            {
//                var scheme = input.PercentageScheme.Scheme.ToList()[week];
//                // var trainingPercentages = week switch
//                // {
//                //     0 => scheme[0],
//                //     1 => input.PercentageScheme.Week2,
//                //     2 => input.PercentageScheme.Week3,
//                //     3 => input.PercentageScheme.Week4,
//                //     _ => input.PercentageScheme.Week1
//                // };

//                var firstSetReps = week switch
//                {
//                    0 => 5,
//                    1 => 3,
//                    2 => 5,
//                    3 => 5,
//                    _ => 5
//                };

//                var secondSetReps = week switch
//                {
//                    0 => 5,
//                    1 => 3,
//                    2 => 3,
//                    3 => 5,
//                    _ => 5
//                };

//                var thirdSetReps = week switch
//                {
//                    0 => 5,
//                    1 => 3,
//                    2 => 1,
//                    3 => 5,
//                    _ => 5
//                };

//                foreach (var (day, setups) in sortedWorkoutSetup)
//                {
//                    var workoutDayResult = GetNextWorkoutDay(lastWorkoutDay, day);
//                    if (!workoutDayResult.IsSuccess)
//                        return Result.Fail<FiveThreeOneWorkoutPlan>(workoutDayResult.Errors);
//                    var workoutDay = workoutDayResult.Value;

//                    var exercises = new List<WorkoutExercise>();
//                    foreach (var setup in setups)
//                    {
//                        if (setup.EstimatedOneRepMax < Mass.Zero)
//                            return Result.Fail<FiveThreeOneWorkoutPlan>(
//                                $"{setup.Exercise.Name} estimated 1RM cannot be less than {Mass.Zero}");

//                        if (!(setup.Increment > Mass.Zero))
//                            return Result.Fail<FiveThreeOneWorkoutPlan>(
//                                $"{setup.Exercise.Name} increment must be larger than {Mass.Zero}");

//                        var trainingMax = TrainingMaxPercentage.Value * setup.EstimatedOneRepMax + cycle * setup.Increment;
//                        // var firstSetWeight = trainingPercentages.Percentage1.Value * trainingMax;
//                        // var secondSetWeight = trainingPercentages.Percentage2.Value * trainingMax;
//                        // var thirdSetWeight = trainingPercentages.Percentage3.Value * trainingMax;
//                        var sets = new List<Set>
//                        {
//                            new() { IsAmrap = false, IsDone = false, Reps = firstSetReps, Weight = Mass.Zero },
//                            new() { IsAmrap = false, IsDone = false, Reps = secondSetReps, Weight = Mass.Zero },
//                            new() { IsAmrap = true, IsDone = false, Reps = thirdSetReps, Weight = Mass.Zero },
//                        };

//                        var exercise = new WorkoutExercise
//                        {
//                            Name = setup.Exercise.Name,
//                            Description = setup.Exercise.Description,
//                            Image = setup.Exercise.Image,
//                            Sets = sets
//                        };
//                        exercises.Add(exercise);
//                    }

//                    var workout = new WorkoutSetup
//                    {
//                        Name = $"Cycle {cycle}: Week {week+1} - {day}",
//                        ScheduledDate = workoutDay,
//                        Exercises = exercises
//                    };
//                    lastWorkoutDay = workoutDay;
//                    workouts.Add(workout);
//                }
//            }
//        }

//        var workoutPlan = new FiveThreeOneWorkoutPlan
//        {
//            Workouts = workouts
//        };
//        return Result.Ok(workoutPlan);
//    }

//    private static Result<DateTime> GetNextWorkoutDay(DateTime dateTime, DayOfWeek desiredDay)
//    {
//        var startingDateTime = dateTime;
//        for (var i = 0; i < 6; i++)
//        {
//            if (startingDateTime.DayOfWeek == desiredDay)
//                return Result.Ok(startingDateTime);
//            startingDateTime = startingDateTime.AddDays(1);
//        }

//        return Result.Fail<DateTime>("Failed to get next workout day");
//    }
//}