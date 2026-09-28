using System.Windows.Input;
using olhuz_desktop_completo.Models.DTOs.Auth;
using olhuz_desktop_completo.Services;
using olhuz_desktop_completo.Views.Home;

namespace olhuz_desktop_completo.ViewModels.Auth
{
    public class LoginViewModel : BindableObject
    {
        private readonly AuthService _authService;
        private readonly AuthSessionService _authSessionService;
        private readonly AppShell _appShell;

        private string _email = string.Empty;
        private string _password = string.Empty;
        private bool _isLoading;

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand BackCommand { get; }

        public LoginViewModel(AuthService authService, AuthSessionService authSessionService, AppShell appShell)
        {
            _authService = authService;
            _authSessionService = authSessionService;
            _appShell = appShell;

            BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            LoginCommand = new Command(async () => await LoginAsync());
            ForgotPasswordCommand = new Command(async () => await ForgotPasswordAsync());
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
                return;
            }

            IsLoading = true;

            try
            {
                var dto = new LoginDto
                {
                    Email = Email,
                    Password = Password
                };

                var response = await _authService.LoginAsync(dto);

                if (response.Error)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Erro",
                        response.Message,
                        "OK");

                    return;
                }

                // Verifica se a API realmente retornou os dados do login
                if (response.Data == null ||
                    string.IsNullOrWhiteSpace(response.Data.Token))
                {
                    await Application.Current!.MainPage!
                        .DisplayAlert(
                            "Erro",
                            "O servidor não retornou o token de autenticação.",
                            "OK");

                    return;
                }

                // Salva o JWT e a data de expiração
                await _authSessionService.SaveTokenAsync(
                    response.Data.Token,
                    response.Data.ExpiresAt);


                // Login concluído
                await Application.Current.MainPage.DisplayAlert(
                    "Sucesso",
                    "Login realizado com sucesso!",
                    "OK");

                // Troca a tela de Login pelo Shell
                Application.Current!.Windows[0].Page = _appShell;

                // Dentro do Shell, abre a Home
                await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Erro",
                    $"Não foi possível realizar o login: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ForgotPasswordAsync()
        {
            await Application.Current!.MainPage!.DisplayAlert("Redefinir Senha", "Navegando para a página de recuperação de senha...", "OK");
        }
    }
}