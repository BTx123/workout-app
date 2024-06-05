using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using UnitsNet;

namespace WorkoutApp.Core.Models;

[ObservableObject]
public partial class SetGroup : ModelBase, ISetGroup
{
    [ObservableProperty]
    private Exercise _exercise = new();

    [ObservableProperty]
    private ObservableCollection<Set> _sets = new();

    [ObservableProperty]
    private string _note = string.Empty;
}

public static class ExerciseExtensions
{
    public static Exercise ToModel(this DAL.Entities.Exercise entity)
    {
        return new Exercise
        {
            Id = entity.Id,
            Name = entity.Name,
            Barbell = entity.Barbell == null
                ? null
                : new Barbell
                {
                    Id = entity.Barbell.Id,
                    Name = entity.Barbell.Name,
                    Weight = Mass.FromKilograms(entity.Barbell.MassKg)
                }
        };
    }
}

public static class SetExtensions
{
    public static Set ToModel(this DAL.Entities.Set entity)
    {
        return new Set
        {
            Id = entity.Id,
            StartedAt = entity.StartedAt,
            StoppedAt = entity.StoppedAt,
            IsAmrap = entity.IsAmrap,
            Weight = Mass.FromKilograms(entity.MassKg),
            Repetitions = entity.Repetitions
        };
    }
}

public static class SetGroupExtensions
{
    public static SetGroup ToModel(this DAL.Entities.SetGroup entity)
    {
        return new SetGroup
        {
            Id = entity.Id,
            Exercise = entity.Exercise.ToModel(),
            Sets = new ObservableCollection<Set>(entity.Sets.Select(s => s.ToModel())),
            Note = entity.Note
        };
    }
}