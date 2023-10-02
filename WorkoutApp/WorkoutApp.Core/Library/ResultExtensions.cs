using FluentResults;

namespace WorkoutApp.Core.Library;

public static class ResultExtensions
{
    public static string? Message<T>(this IResult<T> result)
    {
        return string.Join(", ", result.Reasons.Select(r => r.Message));
    }
    
    public static string? ErrorMessage<T>(this IResult<T> result)
    {
        return string.Join(", ", result.Errors.Select(r => r.Message));
    }
    
    public static string? SuccessMessage<T>(this IResult<T> result)
    {
        return string.Join(", ", result.Successes.Select(r => r.Message));
    }
}