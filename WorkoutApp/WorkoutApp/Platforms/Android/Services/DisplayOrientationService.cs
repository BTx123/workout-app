using Android.Content.PM;

// ReSharper disable once CheckNamespace
namespace WorkoutApp.Services;

public partial class DisplayOrientationService
{
    partial void DoSetDisplayOrientation(DisplayOrientation displayOrientation)
    {
        var currentActivity = ActivityStateManager.Default.GetCurrentActivity();

        if (currentActivity == null) return;

        currentActivity.RequestedOrientation = displayOrientation switch
        {
            DisplayOrientation.Portrait => ScreenOrientation.Portrait,
            DisplayOrientation.Landscape => ScreenOrientation.Landscape,
            DisplayOrientation.Unknown => ScreenOrientation.Unspecified,
            _ => ScreenOrientation.Unspecified
        };
    }

    partial void DoSetDisplayOrientationLock(bool isLocked)
    {
        var currentActivity = ActivityStateManager.Default.GetCurrentActivity();

        if (currentActivity == null) return;

        currentActivity.RequestedOrientation = isLocked ? ScreenOrientation.Locked : ScreenOrientation.Unspecified;
    }
}