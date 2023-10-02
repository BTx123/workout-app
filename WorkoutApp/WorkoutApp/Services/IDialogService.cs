namespace WorkoutApp.Services;

/// <summary>
/// Interface for dialog methods.
/// </summary>
public interface IDialogService : IService
{
    /// <summary>
    /// Display an alert without a close button.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="cancel"></param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task DisplayAlertAsync(string title, string message, string cancel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Display an alert with an acknowledge and close button.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="accept"></param>
    /// <param name="cancel"></param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Display an action sheet.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancel"></param>
    /// <param name="destruction"></param>
    /// <param name="buttons"></param>
    /// <returns></returns>
    public Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons);

    /// <summary>
    /// Display a text prompt.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="accept"></param>
    /// <param name="cancel"></param>
    /// <param name="placeholder"></param>
    /// <param name="maxLength"></param>
    /// <param name="initialValue"></param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel",
        string? placeholder = null, int maxLength = -1, string initialValue = "", CancellationToken cancellationToken = default);

    /// <summary>
    /// Display a snackbar.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="duration"></param>
    /// <param name="dismiss"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task DisplaySnackbarAsync(string text, TimeSpan? duration = null, string dismiss = "OK",
        Action? action = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Display a toast.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="useShortDuration"></param>
    /// <param name="fontSize"></param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task DisplayToastAsync(string text, bool useShortDuration = true, double fontSize = 14,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Display a color dialog.
    /// </summary>
    /// <param name="color">The initial selected color.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task containing the selected color.</returns>
    public Task<Color> DisplayColorDialogAsync(Color? color = null, CancellationToken cancellationToken = default);
}