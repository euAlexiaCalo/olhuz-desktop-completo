using System.Globalization;
using System.Windows.Input;
using olhuz_desktop_completo.Models.DTOs.Preferences;
using olhuz_desktop_completo.Models.Enums;
using olhuz_desktop_completo.Services;

namespace olhuz_desktop_completo.ViewModels.Preferences
{
    public class PreferencesViewModel : BindableObject
    {
        private readonly UserPreferencesService _preferencesService;

        // =========================================================
        // CAMPOS PRIVADOS
        // =========================================================

        private bool _screenReader;
        private decimal _speechRate = 1.0m;
        private VoiceType _voiceType = VoiceType.Feminina;
        private int _volumeLevel = 50;
        private ThemeType _theme = ThemeType.Light;
        private bool _vibrationEnabled = true;
        private bool _alertSoundEnabled = true;


        // =========================================================
        // PROPRIEDADES
        // =========================================================

        public bool ScreenReader
        {
            get => _screenReader;

            set
            {
                if (_screenReader != value)
                {
                    _screenReader = value;

                    // Informa a View que o valor mudou
                    OnPropertyChanged();

                    // Salva a alteração na API
                    _ = SavePreferencesAsync();
                }
            }
        }


        public decimal SpeechRate
        {
            get => _speechRate;

            set
            {
                if (_speechRate != value)
                {
                    _speechRate = value;

                    OnPropertyChanged();

                    _ = SavePreferencesAsync();
                }
            }
        }


        public VoiceType VoiceType
        {
            get => _voiceType;

            set
            {
                if (_voiceType != value)
                {
                    _voiceType = value;

                    OnPropertyChanged();

                    _ = SavePreferencesAsync();
                }
            }
        }


        public int VolumeLevel
        {
            get => _volumeLevel;

            set
            {
                if (_volumeLevel != value)
                {
                    _volumeLevel = value;

                    OnPropertyChanged();

                    _ = SavePreferencesAsync();
                }
            }
        }


        public ThemeType Theme
        {
            get => _theme;

            set
            {
                if (_theme != value)
                {
                    _theme = value;

                    OnPropertyChanged();

                    _ = SavePreferencesAsync();
                }
            }
        }


        public bool VibrationEnabled
        {
            get => _vibrationEnabled;

            set
            {
                if (_vibrationEnabled != value)
                {
                    _vibrationEnabled = value;

                    OnPropertyChanged();

                    _ = SavePreferencesAsync();
                }
            }
        }


        public bool AlertSoundEnabled
        {
            get => _alertSoundEnabled;

            set
            {
                if (_alertSoundEnabled != value)
                {
                    _alertSoundEnabled = value;

                    OnPropertyChanged();

                    _ = SavePreferencesAsync();
                }
            }
        }


        // =========================================================
        // COMMANDS
        // =========================================================

        public ICommand LoadPreferencesCommand { get; }

        public ICommand SetSpeechRateCommand { get; }

        public ICommand SetVoiceTypeCommand { get; }

        public ICommand SetThemeCommand { get; }

        public ICommand AdjustVolumeCommand { get; }


        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public PreferencesViewModel(
            UserPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;


            // -----------------------------------------------------
            // Carregar preferências
            // -----------------------------------------------------

            LoadPreferencesCommand =
                new Command(async () =>
                    await LoadPreferencesAsync());


            // -----------------------------------------------------
            // Velocidade da fala
            // -----------------------------------------------------

            SetSpeechRateCommand =
                new Command<string>(rateString =>
                {
                    if (decimal.TryParse(
                        rateString,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out decimal rate))
                    {
                        SpeechRate = rate;
                    }
                });


            // -----------------------------------------------------
            // Tipo de voz
            // -----------------------------------------------------

            SetVoiceTypeCommand =
                new Command<VoiceType>(voiceType =>
                {
                    VoiceType = voiceType;
                });


            // -----------------------------------------------------
            // Tema
            // -----------------------------------------------------

            SetThemeCommand =
                new Command<ThemeType>(theme =>
                {
                    Theme = theme;
                });


            // -----------------------------------------------------
            // Volume
            // -----------------------------------------------------

            AdjustVolumeCommand =
                new Command<string>(direction =>
                {
                    if (direction == "Up")
                    {
                        VolumeLevel =
                            Math.Min(100, VolumeLevel + 5);
                    }
                    else if (direction == "Down")
                    {
                        VolumeLevel =
                            Math.Max(0, VolumeLevel - 5);
                    }
                });
        }


        // =========================================================
        // CARREGAR PREFERÊNCIAS DA API
        // =========================================================

        public async Task LoadPreferencesAsync()
        {
            try
            {
                // Faz GET para:
                // api/user/preferences
                var response =
                    await _preferencesService.GetPreferencesAsync();


                // -------------------------------------------------
                // Verifica se a API respondeu
                // -------------------------------------------------

                if (response == null)
                {
                    await Application.Current!.MainPage!
                        .DisplayAlert(
                            "Erro",
                            "O servidor não retornou uma resposta.",
                            "OK");

                    return;
                }


                // -------------------------------------------------
                // Verifica se a API retornou erro
                // -------------------------------------------------

                if (response.Error)
                {
                    await Application.Current!.MainPage!
                        .DisplayAlert(
                            "Erro",
                            response.Message,
                            "OK");

                    return;
                }


                // -------------------------------------------------
                // Verifica se existem dados
                // -------------------------------------------------

                if (response.Data == null)
                {
                    await Application.Current!.MainPage!
                        .DisplayAlert(
                            "Erro",
                            "Não foi possível carregar suas configurações.",
                            "OK");

                    return;
                }


                // -------------------------------------------------
                // Atualiza os campos privados
                // -------------------------------------------------
                //
                // IMPORTANTE:
                // usamos os campos privados diretamente.
                //
                // Assim os setters não executam
                // SavePreferencesAsync() enquanto estamos
                // apenas carregando os dados da API.
                // -------------------------------------------------

                _screenReader =
                    response.Data.ScreenReader;

                _speechRate =
                    response.Data.SpeechRate;

                _voiceType =
                    response.Data.VoiceType;

                _volumeLevel =
                    response.Data.VolumeLevel;

                _theme =
                    response.Data.Theme;

                _vibrationEnabled =
                    response.Data.VibrationEnabled;

                _alertSoundEnabled =
                    response.Data.AlertSoundEnabled;


                // -------------------------------------------------
                // Atualiza a interface
                // -------------------------------------------------

                OnPropertyChanged(nameof(ScreenReader));

                OnPropertyChanged(nameof(SpeechRate));

                OnPropertyChanged(nameof(VoiceType));

                OnPropertyChanged(nameof(VolumeLevel));

                OnPropertyChanged(nameof(Theme));

                OnPropertyChanged(nameof(VibrationEnabled));

                OnPropertyChanged(nameof(AlertSoundEnabled));
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!
                    .DisplayAlert(
                        "Erro",
                        $"Não foi possível carregar as configurações: {ex.Message}",
                        "OK");
            }
        }


        // =========================================================
        // SALVAR PREFERÊNCIAS
        // =========================================================

        private async Task SavePreferencesAsync()
        {
            try
            {
                var dto = new UpdateUserPreferencesDto
                {
                    ScreenReader = ScreenReader,

                    SpeechRate = SpeechRate,

                    VoiceType = VoiceType,

                    VolumeLevel = VolumeLevel,

                    Theme = Theme,

                    VibrationEnabled = VibrationEnabled,

                    AlertSoundEnabled = AlertSoundEnabled
                };


                // Faz PUT para:
                // api/user/preferences

                var response =
                    await _preferencesService
                        .UpdatePreferencesAsync(dto);


                if (response == null)
                {
                    return;
                }


                if (response.Error)
                {
                    await Application.Current!.MainPage!
                        .DisplayAlert(
                            "Erro",
                            response.Message,
                            "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!
                    .DisplayAlert(
                        "Erro",
                        $"Não foi possível salvar as configurações: {ex.Message}",
                        "OK");
            }
        }
    }
}