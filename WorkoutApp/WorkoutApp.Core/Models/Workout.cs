using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkoutApp.Core.Models;

[ObservableObject]
public partial class Workout : ModelBase, IWorkout
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Duration))]
    private DateTime _startedAt;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Duration))]
    private DateTime _stoppedAt;

    public TimeSpan Duration => StoppedAt - StartedAt;

    [ObservableProperty]
    private ObservableCollection<ISetGroup> _setGroups = new();

    [ObservableProperty]
    private string _note = string.Empty;
}