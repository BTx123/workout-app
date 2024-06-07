using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class OneRepMaxCalculatorPage : ContentPage
{
    public OneRepMaxCalculatorPage(OneRepMaxCalculatorPageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}