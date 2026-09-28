using System.Windows.Input;
using olhuz_desktop_completo.Models.Responses.Readings;
using olhuz_desktop_completo.Services;

namespace olhuz_desktop_completo.ViewModels.Readings
{
    public class ReadingDetailsViewModel
        : BindableObject, IQueryAttributable
    {
        private readonly ReadingService _readingService;

        private ReadingHistoryResponse _reading = new();

        private CancellationTokenSource? _speechCancellation;

        private bool _isLoading;


        // =========================================================
        // LEITURA
        // =========================================================

        public ReadingHistoryResponse Reading
        {
            get => _reading;

            set
            {
                if (_reading != value)
                {
                    _reading = value;

                    OnPropertyChanged();
                }
            }
        }

        // =========================================================
        // CARREGAMENTO
        // =========================================================

        public bool IsLoading
        {
            get => _isLoading;

            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;

                    OnPropertyChanged();
                }
            }
        }


        // =========================================================
        // COMANDOS
        // =========================================================

        public ICommand BackCommand { get; }

        public ICommand SpeakDescriptionCommand { get; }

        public ICommand CopyTextCommand { get; }

        public ICommand DeleteReadingCommand { get; }


        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public ReadingDetailsViewModel(
            ReadingService readingService)
        {
            _readingService = readingService;

            BackCommand = new Command(
                async () => await GoBackAsync());

            SpeakDescriptionCommand = new Command(
                async () => await SpeakDescriptionAsync());

            CopyTextCommand = new Command(
                async () => await CopyTextAsync());

            DeleteReadingCommand = new Command(
                async () => await DeleteReadingAsync());
        }


        // =========================================================
        // RECEBER LEITURA PELA NAVEGAÇÃO
        // =========================================================

        public void ApplyQueryAttributes(
            IDictionary<string, object> query)
        {
            if (query.TryGetValue(
                "ReadingDetails",
                out var value))
            {
                if (value is ReadingHistoryResponse reading)
                {
                    Reading = reading;
                }
            }
        }

        // =========================================================
        // PARAR FALA
        // =========================================================
        public void StopSpeaking()
        {
            _speechCancellation?.Cancel();
        }


        // =========================================================
        // VOLTAR
        // =========================================================

        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }


        // =========================================================
        // OUVIR DESCRIÇÃO
        // =========================================================

        private async Task SpeakDescriptionAsync()
        {
            if (string.IsNullOrWhiteSpace(Reading.DescriptionText))
                return;

            // Cancela uma fala anterior, caso exista
            _speechCancellation?.Cancel();
            _speechCancellation?.Dispose();

            // Cria um novo controle de cancelamento
            _speechCancellation = new CancellationTokenSource();

            try
            {
                await TextToSpeech.Default.SpeakAsync(
                    Reading.DescriptionText,
                    null,
                    _speechCancellation.Token);
            }
            catch (OperationCanceledException)
            {
                // A fala é cancelada ao sair da tela.
            }
        }


        // =========================================================
        // COPIAR DESCRIÇÃO
        // =========================================================

        private async Task CopyTextAsync()
        {
            if (string.IsNullOrWhiteSpace(
                Reading.DescriptionText))
            {
                return;
            }

            await Clipboard.Default.SetTextAsync(
                Reading.DescriptionText);

            await Application.Current!.MainPage!
                .DisplayAlert(
                    "Sucesso",
                    "Descrição copiada para a área de transferência!",
                    "OK");
        }


        // =========================================================
        // EXCLUIR LEITURA
        // =========================================================

        private async Task DeleteReadingAsync()
        {
            bool confirm =
                await Application.Current!.MainPage!
                    .DisplayAlert(
                        "Excluir Leitura",
                        "Tem certeza que deseja excluir esta leitura permanentemente?",
                        "Sim",
                        "Não");

            if (!confirm)
                return;

            IsLoading = true;

            try
            {
                // Depois colocaremos a exclusão real aqui.

                await Application.Current!.MainPage!
                    .DisplayAlert(
                        "Sucesso",
                        "Leitura excluída com sucesso.",
                        "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!
                    .DisplayAlert(
                        "Erro",
                        $"Não foi possível excluir: {ex.Message}",
                        "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}