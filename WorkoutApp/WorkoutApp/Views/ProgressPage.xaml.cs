using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class ProgressPage
{
    public ProgressPage(ProgressPageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}