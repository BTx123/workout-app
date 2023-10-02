using FluentResults;

namespace WorkoutApp.Core.Library;

public interface IFactory<in TInput, out TOutput>
{
    IResult<TOutput> Create(TInput input);
}