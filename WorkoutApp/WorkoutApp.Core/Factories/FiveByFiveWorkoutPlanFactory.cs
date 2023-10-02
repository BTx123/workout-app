using FluentResults;

namespace WorkoutApp.Core.Factories;

public class FiveByFiveWorkoutPlanFactory : IWorkoutPlanFactory<FiveByFiveWorkoutFactoryInput, FiveByFiveWorkoutPlan>
{
    public IResult<FiveByFiveWorkoutPlan> Create(FiveByFiveWorkoutFactoryInput input)
    {
        throw new NotImplementedException();
    }
}