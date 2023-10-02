using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Extensions.Logging;

namespace WorkoutApp.Services;

public class DialogService : ServiceBase<DialogService>, IDialogService
{
    public override string Name => "dialog-service";

    public DialogService(ILogger<DialogService> logger) : base(logger)
    {
    }

    public async Task DisplayAlertAsync(string title, string message, string cancel, CancellationToken cancellationToken = default)
    {
        await Application.Current!.MainPage!.DisplayAlert(title, message, cancel);
    }

    public async Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel, CancellationToken cancellationToken = default)
    {
        return await Application.Current!.MainPage!.DisplayAlert(title, message, accept, cancel);
    }

    public async Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
    {
        return await Application.Current!.MainPage!.DisplayActionSheet(title, cancel, destruction, buttons);
    }

    public async Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel",
        string? placeholder = null, int maxLength = -1, string initialValue = "", CancellationToken cancellationToken = default)
    {
        return await Application.Current!.MainPage!.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, Keyboard.Default, initialValue);
    }

    public async Task DisplaySnackbarAsync(string text, TimeSpan? duration = null, string dismiss = "OK", Action? action = null, CancellationToken cancellationToken = default)
    {
        var snackbar = Snackbar.Make(text, action, dismiss, duration);
        await snackbar.Show(cancellationToken);
    }

    public async Task DisplayToastAsync(string text, bool useShortDuration = true, double fontSize = 14, CancellationToken cancellationToken = default)
    {
        var duration = useShortDuration ? ToastDuration.Short : ToastDuration.Long;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationToken);
    }

    public async Task<Color> DisplayColorDialogAsync(Color? color = null, CancellationToken cancellationToken = default)
    {
        var dialog = color == null ? new Controls.ColorDialogPopup() : new Controls.ColorDialogPopup(color);
        var result = await Application.Current!.MainPage!.ShowPopupAsync(dialog);
        if (result is Color selectedColor)
        {
            return selectedColor;
        }

        return Controls.ColorDialogPopup.DefaultColor;
    }
}