using Microsoft.Extensions.Logging;
using WorkoutApp.DAL.Constants;
using WorkoutApp.Services;

namespace WorkoutApp.ViewModels;

public partial class SettingsPageViewModel : ViewModelBase<SettingsPageViewModel>
{
    public SettingsPageViewModel(IDialogService dialogService, ISettingsService settingsService, ILogger<SettingsPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        Title = "Settings";
    }

    [ObservableProperty]
    private IList<string> _searchStrings = new List<string>
    {
        SettingsKey.Theme, SettingsKey.GeneralSettings, SettingsKey.AppSettings, SettingsKey.DistanceUnit
    };

    [RelayCommand]
    public async Task Reset()
    {
        var confirm = await DialogService.DisplayAlertAsync("Confirmation",
            "Are you sure you want to reset to default settings?", "OK", "Cancel");
        if (!confirm) return;

        SettingsService.Reset();
    }

    [RelayCommand]
    private async Task GoToGeneralSettingsPage()
    {
        await Shell.Current.GoToAsync("general-settings");
    }

    [RelayCommand]
    private async Task GoToAboutPage()
    {
        await Shell.Current.GoToAsync("about");
    }
}