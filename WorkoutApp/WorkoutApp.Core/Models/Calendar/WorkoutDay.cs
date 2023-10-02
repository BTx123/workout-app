using CommunityToolkit.Mvvm.ComponentModel;
using XCalendar.Core.Collections;
using XCalendar.Core.Interfaces;

namespace WorkoutApp.Core.Models.Calendar;

public partial class WorkoutDay : ObservableObject, ICalendarDay
{
    [ObservableProperty]
    private DateTime _dateTime;

    [ObservableProperty]
    private ObservableRangeCollection<Workout> _workouts = new();

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private bool _isCurrentMonth;

    [ObservableProperty]
    private bool _isToday;

    [ObservableProperty]
    private bool _isInvalid;
}