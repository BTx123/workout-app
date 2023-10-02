using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace WorkoutApp.Logging;

public static class LoggingExtensions
{
    public static IDisposable? WithCallerScope(this ILogger logger, [CallerMemberName] string? memberName = null,
        [CallerFilePath] string? filePath = null, [CallerLineNumber] int? lineNumber = null)
    {
        var properties = new Dictionary<string, object?>
        {
            { "CallerMemberName", memberName },
            { "CallerFilePath", filePath },
            { "CallerLineNumber", lineNumber },
        };
        return logger.BeginScope(properties);
    }
}