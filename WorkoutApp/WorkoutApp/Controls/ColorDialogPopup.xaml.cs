using CommunityToolkit.Maui.Views;

namespace WorkoutApp.Controls;

public partial class ColorDialogPopup : Popup
{
    #region Constructors

    public ColorDialogPopup() : this(DefaultColor)
    {
    }

    public ColorDialogPopup(Color initialSelectedColor)
    {
        InitialSelectedColor = initialSelectedColor ?? throw new ArgumentNullException(nameof(initialSelectedColor));
        ResultWhenUserTapsOutsideOfPopup = initialSelectedColor ?? throw new ArgumentNullException(nameof(initialSelectedColor));
        SelectedColor = initialSelectedColor ?? throw new ArgumentNullException(nameof(initialSelectedColor));

#if DEBUG
        CanBeDismissedByTappingOutsideOfPopup = false;
#endif

        InitializeComponent();

        SetPointerRingPositionFromColor(SelectedColor);
    }

    #endregion

    #region Properties

    public static readonly Color DefaultColor = new();

    public Color InitialSelectedColor { get; }

    private Color _selectedColor = DefaultColor;
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (Equals(_selectedColor, value)) return;
            _selectedColor = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    [RelayCommand]
    public void PickedColorChanged(Color color)
    {
        SelectedColor = color;
    }

    [RelayCommand]
    public void CancelDialog()
    {
        Close(InitialSelectedColor);
    }

    [RelayCommand]
    public void DismissDialog()
    {
        Close(SelectedColor);
    }

    [RelayCommand]
    public void ResetReturnValue()
    {
        SetPointerRingPositionFromColor(InitialSelectedColor);
    }

    #endregion

    #region Helper Method

    private void SetPointerRingPositionFromColor(Color color)
    {
        ColorPicker.PointerRingPositionXUnits = color.GetHue();
        ColorPicker.PointerRingPositionYUnits = 1 - color.GetLuminosity();
    }

    #endregion
}