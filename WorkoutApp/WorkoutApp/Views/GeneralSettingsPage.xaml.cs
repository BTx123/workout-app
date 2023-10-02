using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class GeneralSettingsPage
{
    public GeneralSettingsPage(GeneralSettingsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}