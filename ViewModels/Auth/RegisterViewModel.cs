using System.Windows.Input;
using olhuz_desktop_completo.Models.DTOs.Auth;
using olhuz_desktop_completo.Services;
using olhuz_desktop_completo.Views.Auth;

namespace olhuz_desktop_completo.ViewModels.Auth
{
    public class RegisterViewModel : BindableObject
    {
        private readonly AuthService _authService;

        // Dados do cadastro
        private string _fullName = string.Empty;
        private string _cpf = string.Empty;
        private DateTime _birthDate = DateTime.Today;
        private string _phoneNumber = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;

        // Estado da tela
        private bool _isTermsAccepted;
        private bool _isLoading;

        // Feedback visual da senha
        private Color _bar1Color = Colors.LightGray;
        private Color _bar2Color = Colors.LightGray;
        private Color _bar3Color = Colors.LightGray;
        private Color _bar4Color = Colors.LightGray;
        private string _strengthText = "Senha muito fraca";


        // Propriedades dos dados
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        public string Cpf
        {
            get => _cpf;
            set
            {
                _cpf = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();

                EvaluatePasswordStrength(value);
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }


        // Propriedades de estado
        public bool IsTermsAccepted
        {
            get => _isTermsAccepted;
            set
            {
                if (_isTermsAccepted == value)
                    return;

                _isTermsAccepted = value;
                OnPropertyChanged();

                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }


        // Propriedades da força da senha
        public Color Bar1Color
        {
            get => _bar1Color;
            set
            {
                _bar1Color = value;
                OnPropertyChanged();
            }
        }

        public Color Bar2Color
        {
            get => _bar2Color;
            set
            {
                _bar2Color = value;
                OnPropertyChanged();
            }
        }

        public Color Bar3Color
        {
            get => _bar3Color;
            set
            {
                _bar3Color = value;
                OnPropertyChanged();
            }
        }

        public Color Bar4Color
        {
            get => _bar4Color;
            set
            {
                _bar4Color = value;
                OnPropertyChanged();
            }
        }

        public string StrengthText
        {
            get => _strengthText;
            set
            {
                _strengthText = value;
                OnPropertyChanged();
            }
        }


        // Comandos
        public ICommand RegisterCommand { get; }
        public ICommand OpenTermsCommand { get; }
        public ICommand OpenPrivacyCommand { get; }
        public ICommand BackCommand { get; }


        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;

            BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));

            RegisterCommand = new Command(
                async () => await RegisterAsync(),
                () => IsTermsAccepted);

            OpenTermsCommand = new Command(
                async () => await LoadTextFileAsync(
                    "TermosUso.txt",
                    "Termos de Uso"));

            OpenPrivacyCommand = new Command(
                async () => await LoadTextFileAsync(
                    "privacidade.txt",
                    "Política de Privacidade"));
        }

        // Avalia a força da senha
        private void EvaluatePasswordStrength(string password)
        {
            password ??= string.Empty;

            int strength = 0;

            if (password.Length >= 8)
                strength++;

            if (password.Any(char.IsUpper))
                strength++;

            if (password.Any(char.IsDigit))
                strength++;

            if (password.Any(c => !char.IsLetterOrDigit(c)))
                strength++;


            Color barColor = strength switch
            {
                1 => Color.FromArgb("#FF5C5C"),
                2 => Color.FromArgb("#FFA500"),
                3 => Color.FromArgb("#ADFF2F"),
                4 => Color.FromArgb("#3CB371"),
                _ => Colors.LightGray
            };


            StrengthText = strength switch
            {
                1 => "Senha fraca",
                2 => "Senha média",
                3 => "Senha forte",
                4 => "Senha muito forte",
                _ => "Senha muito fraca"
            };


            Bar1Color = strength >= 1 ? barColor : Colors.LightGray;
            Bar2Color = strength >= 2 ? barColor : Colors.LightGray;
            Bar3Color = strength >= 3 ? barColor : Colors.LightGray;
            Bar4Color = strength >= 4 ? barColor : Colors.LightGray;
        }


        // Realiza o cadastro
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(Cpf) ||
                string.IsNullOrWhiteSpace(PhoneNumber) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    "Preencha todos os campos obrigatórios.",
                    "OK");

                return;
            }

            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    "As senhas não coincidem.",
                    "OK");

                return;
            }

            if (!IsTermsAccepted)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    "Você precisa aceitar os Termos de Uso e a Política de Privacidade.",
                    "OK");

                return;
            }

            IsLoading = true;

            try
            {
                var dto = new RegisterDto
                {
                    FullName = FullName.Trim(),
                    CPF = Cpf.Trim(),
                    BirthDate = BirthDate,
                    PhoneNumber = PhoneNumber.Trim(),
                    Email = Email.Trim(),
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
                };

                var response = await _authService.RegisterAsync(dto);

                if (response.Error)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Erro",
                        response.Message,
                        "OK");

                    return;
                }

                await Application.Current.MainPage.DisplayAlert(
                    "Sucesso",
                    "Cadastro realizado com sucesso!",
                    "OK");

                // Depois do cadastro, vai para a tela de Login
                await Shell.Current.GoToAsync(nameof(LoginView));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    $"Não foi possível realizar o cadastro: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }


        // Carrega os arquivos de termos e privacidade
        private async Task LoadTextFileAsync(string fileName, string title)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                using var reader = new StreamReader(stream);

                var content = await reader.ReadToEndAsync();

                await Application.Current.MainPage.DisplayAlert(
                    title,
                    content,
                    "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    $"Não foi possível carregar o arquivo: {ex.Message}",
                    "OK");
            }
        }
    }
}