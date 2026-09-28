using olhuz_desktop_completo.ViewModels.Auth;

namespace olhuz_desktop_completo.Views.Auth;

public partial class LoginView : ContentPage
{
    private readonly LoginViewModel _viewModel;
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}