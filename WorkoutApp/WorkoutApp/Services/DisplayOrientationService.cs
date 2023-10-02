namespace WorkoutApp.Services;

public partial class DisplayOrientationService : IDisplayOrientationService
{
    private readonly IDeviceDisplay _deviceDisplay;

    public string Name => "display-orientation-service";

    public DisplayOrientationService(IDeviceDisplay deviceDisplay)
    {
        _deviceDisplay = deviceDisplay;
    }

    public DisplayOrientation GetDisplayOrientation()
    {
        return _deviceDisplay.MainDisplayInfo.Orientation;
    }

    public void SetDisplayOrientation(DisplayOrientation displayOrientation)
    {
        DoSetDisplayOrientation(displayOrientation);
    }

    public void SetDisplayOrientationLock(bool isLocked)
    {
        DoSetDisplayOrientationLock(isLocked);
    }

    #region Partial Methods

    partial void DoSetDisplayOrientation(DisplayOrientation displayOrientation);

    partial void DoSetDisplayOrientationLock(bool isLocked);

    #endregion

}