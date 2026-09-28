using olhuz_desktop_completo.Services;
using olhuz_desktop_completo.Views.Auth;

namespace olhuz_desktop_completo
{
    public partial class App : Application
    {
        private readonly AppShell _appShell;
        private readonly AuthShell _authShell;
        private readonly AuthSessionService _authSessionService;

        public App(AppShell appShell, AuthShell authShell, AuthSessionService authSessionService)
        {
            InitializeComponent();

            _appShell = appShell;
            _authShell = authShell;
            _authSessionService = authSessionService;
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {

            // A janela começa vazia
            var window = new Window();

            // Inicia a verificação da sessão para saber que tela exibir
            _ = InitializeApplicationAsync(window);

            return window;
        }

        // Verifica a sessão e decide qual tela deve ser aberta
        private async Task InitializeApplicationAsync(Window window)
        {
            // Verifica se existe uma sessão válida
            bool isAuthenticated =
                await _authSessionService
                    .IsAuthenticatedAsync();

            if (isAuthenticated)
            {
                window.Page = _appShell;

                await _appShell.GoToAsync(
                    $"//{nameof(Views.Home.HomeView)}");
            }
            else
            {
                window.Page = _authShell;
            }
        }
    }
}