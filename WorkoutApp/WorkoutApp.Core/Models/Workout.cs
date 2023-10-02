using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using UnitsNet;

namespace WorkoutApp.Core.Models;

public partial class Workout : ObservableObject, IWorkout
{
    private readonly Database.Workout _workout;

    private Workout(Database.Workout workout)
    {
        _workout = workout ?? throw new ArgumentNullException(nameof(workout));
    }

    public static Workout From(Database.Workout workout)
    {
        return new Workout(workout);
    }

    [ObservableProperty]
    private string _name;

    partial void OnNameChanged(string value)
    {
        _workout.Name = value;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Duration))]
    private DateTime _startedAt;

    partial void OnStartedAtChanged(DateTime value)
    {
        _workout.StartedAt = value;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Duration))]
    private DateTime? _completedAt;

    partial void OnCompletedAtChanged(DateTime? value)
    {
        _workout.CompletedAt = value;
    }

    public TimeSpan? Duration => CompletedAt == null ? null : CompletedAt - StartedAt;

    [ObservableProperty]
    private ObservableCollection<ISetGroup> _setGroups;

    partial void OnSetGroupsChanged(ObservableCollection<ISetGroup> value)
    {
        _workout.SetGroups.Clear();
        foreach (var group in value)
        {
            _workout.SetGroups.Add(group.ToDatabaseObject());
        }
    }

    [ObservableProperty]
    private string _note;
}

public interface IWorkout
{
    string Name { get; set; }

    DateTime StartedAt { get; set; }

    DateTime? CompletedAt { get; set; }

    TimeSpan? Duration { get; }

    ObservableCollection<ISetGroup> SetGroups { get; set; }

    string Note { get; set; }
}