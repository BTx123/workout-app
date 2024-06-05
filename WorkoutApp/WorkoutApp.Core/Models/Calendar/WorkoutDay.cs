using XCalendar.Core.Collections;
using XCalendar.Core.Models;

namespace WorkoutApp.Core.Models.Calendar;

// public class WorkoutDay<TEvent> : CalendarDay<TEvent>
//     where TEvent : IEvent;

public class WorkoutDay : CalendarDay
{
    // [ObservableProperty]
    public ObservableRangeCollection<Workout> Workouts = new();
}