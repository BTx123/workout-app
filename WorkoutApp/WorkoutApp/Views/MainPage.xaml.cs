using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class MainPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}

