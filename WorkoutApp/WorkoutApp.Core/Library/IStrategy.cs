using FluentResults;

namespace WorkoutApp.Core.Library;

public interface IStrategy<out TOutput> : INamed
{
    /// <summary>
    /// Execute the strategy.
    /// </summary>
    /// <returns>Result of the strategy.</returns>
    IResult<TOutput> Execute();
}

public interface IStrategy<in TInput, out TOutput> : INamed
{
    /// <summary>
    /// Execute the strategy.
    /// </summary>
    /// <param name="input">The input the use.</param>
    /// <returns>Result of the strategy.</returns>
    IResult<TOutput> Execute(TInput input);
}