using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class HistoryPage
{
    public HistoryPage(HistoryPageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}