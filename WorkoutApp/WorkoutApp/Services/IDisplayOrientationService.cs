namespace WorkoutApp.Services;

public interface IDisplayOrientationService : IService
{
    /// <summary>
    /// Get the display orientation.
    /// </summary>
    /// <returns>The display orientation result.</returns>
    DisplayOrientation GetDisplayOrientation();

    /// <summary>
    /// Set the display orientation.
    /// </summary>
    /// <param name="displayOrientation">The display orientation to use.</param>
    /// <returns>Result indicating success or failure.</returns>
    void SetDisplayOrientation(DisplayOrientation displayOrientation);

    /// <summary>
    /// Set the display orientation lock.
    /// </summary>
    /// <returns>Result indicating success or failure.</returns>
    void SetDisplayOrientationLock(bool isLocked);
}