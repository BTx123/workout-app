using Microsoft.Extensions.Logging;
using WorkoutApp.Services;

namespace WorkoutApp.ViewModels;

public partial class ViewModelBase<T> : ObservableValidator
    where T : ObservableObject
{
    /// <summary>
    /// Dialog service.
    /// </summary>
    protected IDialogService DialogService { get; }

    /// <summary>
    /// Settings service.
    /// </summary>
    protected ISettingsService SettingsService { get; }

    /// <summary>
    /// Logger.
    /// </summary>
    protected ILogger<T> Logger { get; }

    /// <summary>
    /// Create an instance.
    /// </summary>
    /// <param name="dialogService">The dialog service to use.</param>
    /// <param name="settingsService">The settings service to use.</param>
    /// <param name="logger">The logger to use.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected ViewModelBase(IDialogService dialogService, ISettingsService settingsService, ILogger<T> logger)
    {
        DialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        SettingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Properties

    /// <summary>
    /// Title of the view model.
    /// </summary>
    [ObservableProperty]
    private string _title = string.Empty;

    /// <summary>
    /// View model is busy.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    /// <summary>
    /// View model is not busy.
    /// </summary>
    public bool IsNotBusy => !IsBusy;

    #endregion
}