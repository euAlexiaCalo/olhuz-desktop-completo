using System.Windows.Input;
using olhuz_desktop_completo.Models.DTOs.User;
using olhuz_desktop_completo.Services;

namespace olhuz_desktop_completo.ViewModels.User
{
    public class ProfileViewModel : BindableObject
    {
        private readonly UserService _userService;
        private readonly AuthSessionService _authSessionService;
        private readonly AuthShell _authShell;

        private bool _isLoading = true;
        private string _fullName = string.Empty;
        private string _birthDate = string.Empty;
        private string _email = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _cpf = string.Empty;

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public string BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        public string Cpf
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(); }
        }

        public ICommand LoadProfileCommand { get; }
        public ICommand EditDataCommand { get; }
        public ICommand ChangePasswordCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand DeleteAccountCommand { get; }

        public ProfileViewModel(UserService userService, AuthSessionService authSessionService, AuthShell authShell)
        {
            _userService = userService;
            _authSessionService = authSessionService;
            _authShell = authShell;

            LoadProfileCommand = new Command(async () => await LoadProfileAsync());
            EditDataCommand = new Command(async () => await EditDataAsync());
            ChangePasswordCommand = new Command(async () => await ChangePasswordAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
            DeleteAccountCommand = new Command(async () => await DeleteAccountAsync());
        }

        public async Task LoadProfileAsync()
        {
            IsLoading = true;

            try
            {
                var response = await _userService.GetProfileAsync();

                if (!response.Error && response.Data != null)
                {
                    FullName = response.Data.FullName;
                    BirthDate = response.Data.BirthDate.ToString("dd/MM/yyyy");
                    Email = response.Data.Email;
                    PhoneNumber = response.Data.PhoneNumber;
                    Cpf = response.Data.CPF;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Erro",
                        response.Message,
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    $"Não foi possível carregar o perfil: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task EditDataAsync()
        {
            await Application.Current.MainPage.DisplayAlert("Editar Dados", "Navegando para a edição de perfil...", "OK");
        }

        private async Task ChangePasswordAsync()
        {
            await Application.Current.MainPage.DisplayAlert("Alterar Senha", "Navegando para a alteração de senha...", "OK");
        }

        private async Task LogoutAsync()
        {
            bool confirm =
                await Application.Current.MainPage.DisplayAlert(
                    "Sair",
                    "Deseja realmente desconectar da sua conta?",
                    "Sim",
                    "Não");

            if (!confirm)
                return;

            // Remove o token e a data de expiração salvos
            _authSessionService.ClearToken();

            // Troca o Shell principal pelo Shell de autenticação
            Application.Current!.Windows[0].Page = _authShell;

            // Vai para a tela inicial do AuthShell
            await _authShell.GoToAsync("//WelcomeView");
        }

        private async Task DeleteAccountAsync()
        {
            bool confirm =
                await Application.Current.MainPage.DisplayAlert(
                    "Excluir Conta",
                    "Tem certeza que deseja desativar sua conta? Você será desconectado e não poderá acessar sua conta enquanto ela estiver inativa.",
                    "Sim",
                    "Cancelar");

            if (!confirm)
                return;

            try
            {
                IsLoading = true;

                var response = await _userService.DeactivateAccountAsync();

                if (response.Error)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Erro",
                        response.Message,
                        "OK");

                    return;
                }

                // Remove o token salvo no computador
                _authSessionService.ClearToken();

                await Application.Current.MainPage.DisplayAlert(
                    "Conta desativada",
                    response.Message,
                    "OK");

                // Troca o AppShell pelo AuthShell
                Application.Current!.Windows[0].Page = _authShell;

                // Volta para a tela inicial
                await _authShell.GoToAsync("//WelcomeView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    $"Não foi possível desativar sua conta: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}