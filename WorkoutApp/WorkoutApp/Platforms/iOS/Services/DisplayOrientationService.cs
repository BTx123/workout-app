
using UIKit;

// ReSharper disable once CheckNamespace
namespace WorkoutApp.Services;

public partial class DisplayOrientationService
{
    partial void DoSetDisplayOrientation(DisplayOrientation displayOrientation)
    {
        var orientation = displayOrientation switch
        {
            DisplayOrientation.Portrait => UIInterfaceOrientation.Portrait,
            DisplayOrientation.Landscape => UIInterfaceOrientation.LandscapeLeft,
            DisplayOrientation.Unknown => UIInterfaceOrientation.Unknown,
            _ => UIInterfaceOrientation.Unknown
        };

        UIApplication.SharedApplication.SetStatusBarOrientation(orientation, true);
    }

    partial void DoSetDisplayOrientationLock(bool isLocked)
    {
        // TODO: Is this even possible with MAUI?
    }
}