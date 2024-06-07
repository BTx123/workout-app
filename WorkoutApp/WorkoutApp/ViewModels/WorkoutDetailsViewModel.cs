using Microsoft.Extensions.Logging;
using WorkoutApp.DAL.Entities;
using WorkoutApp.Services;

namespace WorkoutApp.ViewModels;

public partial class WorkoutDetailsViewModel : ViewModelBase<WorkoutDetailsViewModel>
{
    protected WorkoutDetailsViewModel(Workout workout, IDialogService dialogService, ISettingsService settingsService, ILogger<WorkoutDetailsViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
            _workout = workout ?? throw new ArgumentNullException(nameof(workout));
        }

    [ObservableProperty]
    private Workout _workout;

    [RelayCommand]
    private async Task EditNote(CancellationToken cancellationToken = default)
    {
            await Task.Delay(1000, cancellationToken);
        }
}