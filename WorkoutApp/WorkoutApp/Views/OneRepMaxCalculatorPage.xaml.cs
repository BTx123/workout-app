using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class OneRepMaxCalculatorPage : ContentPage
{
    public OneRepMaxCalculatorPage(OneRepMaxCalculatorViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}