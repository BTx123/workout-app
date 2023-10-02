using WorkoutApp.Core.Models.Settings;

namespace WorkoutApp.Core.Events;

public class AppSettingsChangedEventArgs : EventArgs
{
    public AppSettings? AppSettings { get; }

    public AppSettingsChangedEventArgs(AppSettings? appSettings)
    {
        AppSettings = appSettings;
    }
}