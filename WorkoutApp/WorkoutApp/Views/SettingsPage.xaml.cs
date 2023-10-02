using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class SettingsPage
{
    public SettingsPage(SettingsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}