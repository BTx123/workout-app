using CommunityToolkit.Mvvm.ComponentModel;
using UnitsNet;

namespace WorkoutApp.Core.Models;

[ObservableObject]
public partial class Barbell : ModelBase, IBarbell
{
    public static readonly Barbell StandardBarbell = new() { Name = "Standard Barbell", Weight = Mass.FromPounds(45) };

    public static readonly Barbell OlympicBarbell = new() { Name = "Olympic Barbell", Weight = Mass.FromKilograms(20) };

    public static readonly Barbell AlternativeOlympicBarbell = new() { Name = "Light Olympic Barbell", Weight = Mass.FromKilograms(15) };

    public static readonly Barbell SafetySquatBar = new() { Name = "Safety Squat Bar", Weight = Mass.FromPounds(70) };

    public static readonly Barbell EzCurlBar = new() { Name = "EZ Curl Bar", Weight = Mass.FromPounds(25) };

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private Mass _weight;
}
