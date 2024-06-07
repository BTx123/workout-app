using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutApp.ViewModels;

namespace WorkoutApp.Views;

public partial class PlateCalculatorPage : ContentPage
{
    public PlateCalculatorPage(PlateCalculatorPageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}