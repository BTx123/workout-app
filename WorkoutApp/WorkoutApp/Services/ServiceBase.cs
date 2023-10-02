using Microsoft.Extensions.Logging;

namespace WorkoutApp.Services;

public abstract class ServiceBase<T> : IService
    where T : IService
{
    /// <inheritdoc />
    public abstract string Name { get; }

    /// <summary>
    /// Logger for service.
    /// </summary>
    protected ILogger<T> Logger { get; }

    protected ServiceBase(ILogger<T> logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}