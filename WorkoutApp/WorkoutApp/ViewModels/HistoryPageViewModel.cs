using System.Collections.Concurrent;
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutApp.Core.Models;
using WorkoutApp.Core.Models.Calendar;
using WorkoutApp.DAL.Context;
using WorkoutApp.Logging;
using WorkoutApp.Services;
using XCalendar.Core.Collections;
using XCalendar.Core.Enums;
using XCalendar.Core.Extensions;
using XCalendar.Core.Models;

namespace WorkoutApp.ViewModels;

public partial class HistoryPageViewModel : ViewModelBase<HistoryPageViewModel>
{
    private readonly IDbContextFactory<WorkoutAppContext> _dbContextFactory;
    private readonly ISettingsService _settingsService;
    private static readonly Random Random = new();

    public HistoryPageViewModel(IDbContextFactory<WorkoutAppContext> dbContextFactory, IDialogService dialogService, ISettingsService settingsService, ILogger<HistoryPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        if (Application.Current == null) throw new Exception("Failed to determine current application");

        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        _settingsService.FirstDayOfWeekChanged += SettingsServiceOnFirstDayOfWeekChanged;

        RefreshTimer = Application.Current.Dispatcher.CreateTimer();
        RefreshTimer.Interval = TimeSpan.FromSeconds(60);
        RefreshTimer.IsRepeating = true;
        RefreshTimer.Tick += RefreshTimerOnTick;

        WorkoutCalendar = new Calendar<WorkoutDay>
        {
            StartOfWeek = _settingsService.FirstDayOfTheWeek,
            SelectionType = SelectionType.Single,
            SelectionAction = SelectionAction.Replace,
            SelectedDates = new ObservableRangeCollection<DateTime> { DateTime.Today },
            NavigationLoopMode = NavigationLoopMode.DontLoop,
            //NavigationUpperBound = DateTime.Today,
            AutoRows = true,
            NavigatedDate = DateTime.Today,
            TodayDate = DateTime.Today
        };
        WorkoutCalendar.DaysUpdated += WorkoutCalendarOnDaysUpdated;
        WorkoutCalendar.DateSelectionChanged += WorkoutCalendarOnDateSelectionChanged;
    }

    #region Properties

    protected IDispatcherTimer RefreshTimer { get; }

    [ObservableProperty]
    private Calendar<WorkoutDay> _workoutCalendar;

    [ObservableProperty]
    private bool _isCalendarBusy;

    [ObservableProperty]
    private bool _isListBusy;

    [ObservableProperty]
    private bool _isRefreshing;

    public ObservableRangeCollection<Workout> Workouts { get; } = new();

    public ObservableRangeCollection<Workout> SelectedWorkouts { get; } = new();

    #endregion

    #region Commands

    [RelayCommand(AllowConcurrentExecutions = false, IncludeCancelCommand = true)]
    private async Task Appearing(CancellationToken cancellationToken = default)
    {
        await Refresh(cancellationToken);
        RefreshTimer.Start();
    }

    [RelayCommand(AllowConcurrentExecutions = false, IncludeCancelCommand = true)]
    private async Task Disappearing(CancellationToken cancellationToken = default)
    {
        RefreshTimer.Stop();
    }

    [RelayCommand(CanExecute = nameof(CanExecuteRefresh), AllowConcurrentExecutions = false, IncludeCancelCommand = true)]
    private async Task Refresh(CancellationToken cancellationToken = default)
    {
        using var _ = Logger.WithCallerScope();

        try
        {
            IsRefreshing = true;

            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);

            // Refresh workouts collection
            var allWorkouts = await GetWorkoutsAsync(cancellationToken: cancellationToken);
            Workouts.ReplaceRange(allWorkouts);

            var workoutsByDay = await GetWorkoutsByDay(WorkoutCalendar.Days, cancellationToken);
            foreach (var day in WorkoutCalendar.Days)
            {
                if (workoutsByDay.TryGetValue(day, out var workouts))
                    day.Workouts.ReplaceRange(workouts);
            }

            var selectedWorkouts = await GetWorkoutsForSelectedDates(WorkoutCalendar.SelectedDates, cancellationToken);

            SelectedWorkouts.Clear();
            SelectedWorkouts.AddRange(selectedWorkouts);

            Logger.LogDebug("Refreshed workout calendar");
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to refresh calendar");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private bool CanExecuteRefresh()
    {
        return !IsBusy && !IsRefreshing && !IsCalendarBusy && !IsListBusy;
    }

    [RelayCommand(CanExecute = nameof(CanExecuteChangeDateSelection), AllowConcurrentExecutions = false, IncludeCancelCommand = true)]
    private async Task ChangeDateSelection(DateTime dateTime, CancellationToken cancellationToken = default)
    {
        using var _ = Logger.WithCallerScope();

        try
        {
            await Task.Delay(1, cancellationToken);

            if (!WorkoutCalendar.IsDateTimeCurrentMonth(dateTime))
            {
                var months = dateTime.Month - WorkoutCalendar.NavigatedDate.Month;
                await Navigate(months, cancellationToken);
            }

            WorkoutCalendar.ChangeDateSelection(dateTime);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to change selected date");
        }
    }

    private bool CanExecuteChangeDateSelection()
    {
        return !IsBusy && !IsListBusy;
    }

    [RelayCommand(CanExecute = nameof(CanExecuteNavigateToToday), AllowConcurrentExecutions = false, IncludeCancelCommand = true)]
    private async Task NavigateToToday(CancellationToken cancellationToken = default)
    {
        using var _ = Logger.WithCallerScope();

        try
        {
            await Task.Delay(1, cancellationToken);

            GoToToday();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to navigate to today");
        }
    }

    private bool CanExecuteNavigateToToday()
    {
        return !IsBusy && !IsListBusy;
    }

    [RelayCommand(CanExecute = nameof(CanExecuteNavigate), AllowConcurrentExecutions = false, IncludeCancelCommand = true)]
    private async Task Navigate(int months, CancellationToken cancellationToken = default)
    {
        using var _ = Logger.WithCallerScope();

        try
        {
            await Task.Delay(1, cancellationToken);

            if (WorkoutCalendar.NavigatedDate.TryAddMonths(months, out var targetDate))
            {
                WorkoutCalendar.NavigatedDate = targetDate;
            }
            else
            {
                WorkoutCalendar.Navigate(months > 0 ? TimeSpan.MaxValue : TimeSpan.MinValue);
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to go backwards {MonthCount} month(s)", months);
        }
    }

    private bool CanExecuteNavigate()
    {
        return !IsBusy && !IsCalendarBusy;
    }

    #endregion

    #region Event Handlers

    private void SettingsServiceOnFirstDayOfWeekChanged(object? sender, DayOfWeek e)
    {
        using var _ = Logger.WithCallerScope();

        WorkoutCalendar.StartOfWeek = e;
        Logger.LogDebug("Updated workout calendar first day of week to {FirstDayOfWeek}", e);
    }

    private async void RefreshTimerOnTick(object? sender, EventArgs e)
    {
        using var _ = Logger.WithCallerScope();

        try
        {
            await Refresh();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to refresh at interval");
        }
    }

    private async void WorkoutCalendarOnDaysUpdated(object? sender, EventArgs args)
    {
        using var _ = Logger.WithCallerScope();

        if (IsBusy || IsCalendarBusy) return;

        try
        {
            IsCalendarBusy = true;

            var workoutsByDay = await GetWorkoutsByDay(WorkoutCalendar.Days);

            foreach (var day in WorkoutCalendar.Days)
            {
                if (!workoutsByDay.TryGetValue(day, out var workouts)) continue;

                day.Workouts.Clear();
                day.Workouts.AddRange(workouts);
            }

            Logger.LogDebug("Updated workout calender for days {@Days}", WorkoutCalendar.Days.Select(d => d.DateTime));
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to update workouts per day");
        }
        finally
        {
            IsCalendarBusy = false;
        }
    }

    private async void WorkoutCalendarOnDateSelectionChanged(object? sender, DateSelectionChangedEventArgs e)
    {
        using var _ = Logger.WithCallerScope();

        if (IsBusy || IsListBusy) return;

        try
        {
            Logger.LogDebug("Updating workout calender for selected dates {SelectedDates}", e.CurrentSelection);

            IsListBusy = true;

            var workouts = await GetWorkoutsForSelectedDates(e.CurrentSelection);

            SelectedWorkouts.Clear();
            SelectedWorkouts.AddRange(workouts);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to update selected date workouts");
        }
        finally
        {
            IsListBusy = false;
        }
    }

    #endregion

    #region Helper Methods

    private async Task<IEnumerable<Workout>> GetWorkoutsAsync(DateTime? start = null, DateTime? end = null, CancellationToken cancellationToken = default)
    {
        start ??= DateTime.MinValue;
        end ??= DateTime.MaxValue;

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var workouts = await db.Workouts
            .Where(w => w.StartedAt > start && w.StartedAt < end)
            .Select(w => new Workout
            {
                Id = w.Id,
                Name = w.Name,
                StartedAt = w.StartedAt,
                StoppedAt = w.StoppedAt,
                SetGroups = w.SetGroups.Select(g => g.ToModel()).Cast<ISetGroup>().ToObservableCollection(),
                Note = w.Note
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return workouts;
    }

    private void GoToToday()
    {
        var today = DateTime.Today;
        WorkoutCalendar.NavigatedDate = today;
        if (!WorkoutCalendar.SelectedDates.Contains(today))
        {
            WorkoutCalendar.SelectedDates.ReplaceRange(new[] { today });
        }
    }

    private async Task<IDictionary<WorkoutDay, IEnumerable<Workout>>> GetWorkoutsByDay(IEnumerable<WorkoutDay> days, CancellationToken cancellationToken = default)
    {
        var workoutsByDay = new ConcurrentDictionary<WorkoutDay, IEnumerable<Workout>>();

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = -1,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(days, parallelOptions, async (day, t) =>
        {
            var workouts = await Task.Run(() => Workouts.Where(w => w.StartedAt.Date == day.DateTime.Date), t);
            if (!workoutsByDay.TryAdd(day, workouts))
            {
                Logger.LogError("Failed to add workouts to day {Day} while getting workouts by day", day);
            }
        });

        return workoutsByDay;
    }

    private async Task<IEnumerable<Workout>> GetWorkoutsForSelectedDates(IEnumerable<DateTime> dates, CancellationToken token = default)
    {
        var workouts = new List<Workout>();

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = -1,
            CancellationToken = token
        };

        await Parallel.ForEachAsync(Workouts, parallelOptions, async (workout, t) =>
        {
            var anyWorkouts = await Task.Run(() =>
                dates.Any(y => workout.StartedAt.Date == y.Date), t);
            if (anyWorkouts) workouts.Add(workout);
        });

        var sortedWorkouts = workouts.OrderByDescending(x => x.StartedAt);

        return sortedWorkouts;
    }

    #endregion
}