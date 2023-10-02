using Microsoft.Extensions.Logging;
using WorkoutApp.Services;

namespace WorkoutApp.ViewModels;

public partial class AboutPageViewModel : ViewModelBase<AboutPageViewModel>
{
    [ObservableProperty]
    private IAppInfo _appInfo;

    public AboutPageViewModel(IAppInfo appInfo, IDialogService dialogService,
        ISettingsService settingsService, ILogger<AboutPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        Title = "About";

        _appInfo = appInfo ?? throw new ArgumentNullException(nameof(appInfo));
    }

    [RelayCommand]
    public async Task VisitWebsite()
    {
        var uri = new Uri("https://workout-app.brian-tom.com");
        try
        {
            var success = await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            if (!success)
            {
                Logger.LogError("Failed to launch browser to {URI}", uri);
                await DialogService.DisplayAlertAsync("Failure", $"Failed to launch browser to {uri}", "OK");
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to open browser to {URI}", uri);
        }
    }
}