using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class WorkoutDetailsPage : ContentPage
{
    public WorkoutDetailsPage(WorkoutDetailsViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
;    }
}