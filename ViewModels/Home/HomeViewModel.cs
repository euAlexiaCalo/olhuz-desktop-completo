using System.Windows.Input;
using olhuz_desktop_completo.Models.DTOs.Readings;
using olhuz_desktop_completo.Services;

namespace olhuz_desktop_completo.ViewModels.Home
{
    public class HomeViewModel : BindableObject
    {
        private readonly ReadingService _readingService;
        private readonly UserService _userService;

        private string _userName = "Usuário";
        private bool _isLoading;

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
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

        public ICommand PickFileCommand { get; }
        public ICommand LoadUserCommand { get; }


        public HomeViewModel(
            ReadingService readingService,
            UserService userService)
        {
            _readingService = readingService;
            _userService = userService;

            LoadUserCommand = new Command(
                async () => await LoadUserDataAsync());

            PickFileCommand = new Command(
                async () => await PickAndProcessFileAsync());
        }


        // Carrega o nome do usuário
        public async Task LoadUserDataAsync()
        {
            try
            {
                var response = await _userService.GetProfileAsync();

                if (!response.Error &&
                    response.Data != null &&
                    !string.IsNullOrWhiteSpace(response.Data.FullName))
                {
                    UserName = response.Data.FullName
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault() ?? "Usuário";
                }
            }
            catch
            {
                UserName = "Usuário";
            }
        }


        // Processa o arquivo enviado para a API
        public async Task ProcessFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) ||
                !File.Exists(filePath))
            {
                return;
            }

            IsLoading = true;

            try
            {
                string extension = Path
                    .GetExtension(filePath)
                    .ToLowerInvariant();

                if (!IsSupportedFile(extension))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Arquivo não suportado",
                        "Selecione uma imagem, PDF ou arquivo de texto.",
                        "OK");

                    return;
                }

                string typeLabel =
                    extension == ".pdf" || extension == ".txt"
                        ? "Documento"
                        : "Imagem";

                string fileName = Path.GetFileName(filePath);

                var dto = new CreateReadingDto
                {
                    Type = typeLabel,
                    Title = fileName,
                    FilePath = filePath,
                    DescriptionText = string.Empty
                };

                var response =
                    await _readingService.CreateReadingAsync(dto);

                if (response != null && !response.Error && response.Data != null)
                {
                    var navigationParameter =
                        new Dictionary<string, object>
                        {
                            { "ReadingDetails", response.Data }
                        };

                    // Navega passando a leitura gerada pela API
                    await Shell.Current.GoToAsync(
                        "ReadingDetailsView",
                        navigationParameter);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Erro",
                        response.Message ??
                        "Erro ao processar o arquivo.",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    $"Falha ao enviar o arquivo: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }


        // Abre o seletor de arquivos
        private async Task PickAndProcessFileAsync()
        {
            try
            {
                var customFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        {
                            DevicePlatform.WinUI,
                            new[]
                            {
                                ".jpg",
                                ".jpeg",
                                ".png",
                                ".pdf",
                                ".txt"
                            }
                        },
                        {
                            DevicePlatform.MacCatalyst,
                            new[]
                            {
                                "public.jpeg",
                                "public.png",
                                "com.adobe.pdf",
                                "public.plain-text"
                            }
                        },
                        {
                            DevicePlatform.Android,
                            new[]
                            {
                                "image/jpeg",
                                "image/png",
                                "application/pdf",
                                "text/plain"
                            }
                        },
                        {
                            DevicePlatform.iOS,
                            new[]
                            {
                                "public.jpeg",
                                "public.png",
                                "com.adobe.pdf",
                                "public.plain-text"
                            }
                        }
                    });

                var options = new PickOptions
                {
                    PickerTitle =
                        "Selecione uma imagem, PDF ou arquivo de texto",

                    FileTypes = customFileType
                };

                var result =
                    await FilePicker.Default.PickAsync(options);

                if (result != null)
                {
                    await ProcessFileAsync(result.FullPath);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    $"Não foi possível selecionar o arquivo: {ex.Message}",
                    "OK");
            }
        }


        // Verifica se o formato é aceito
        private static bool IsSupportedFile(string extension)
        {
            return extension is
                ".jpg" or
                ".jpeg" or
                ".png" or
                ".pdf" or
                ".txt";
        }
    }
}