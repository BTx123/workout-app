using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}