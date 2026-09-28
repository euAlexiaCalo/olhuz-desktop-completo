using olhuz_desktop_completo.ViewModels.Preferences;

namespace olhuz_desktop_completo.Views.Preferences;

public partial class PreferencesView : ContentPage
{
    private readonly PreferencesViewModel _viewModel;

    public PreferencesView(PreferencesViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPreferencesAsync();
    }
}