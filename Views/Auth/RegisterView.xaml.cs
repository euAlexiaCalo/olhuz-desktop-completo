using olhuz_desktop_completo.ViewModels.Auth;

namespace olhuz_desktop_completo.Views.Auth;

public partial class RegisterView : ContentPage
{
    private readonly RegisterViewModel _viewModel;
    public RegisterView(RegisterViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}