using olhuz_desktop_completo.ViewModels.Auth;

namespace olhuz_desktop_completo.Views.Auth;

public partial class WelcomeView : ContentPage
{
    private readonly WelcomeViewModel _viewModel;
    public WelcomeView(WelcomeViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}