using System.Windows.Input;

namespace olhuz_desktop_completo.Components;

public partial class BackButton : ContentView
{
    public static readonly BindableProperty BackCommandProperty =
        BindableProperty.Create(
            nameof(BackCommand),
            typeof(ICommand),
            typeof(BackButton),
            null);

    public ICommand? BackCommand
    {
        get => (ICommand?)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }

    public BackButton()
    {
        InitializeComponent();
    }
}