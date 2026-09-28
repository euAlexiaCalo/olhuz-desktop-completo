using System.Collections.ObjectModel;
using System.Windows.Input;
using olhuz_desktop_completo.Models.Responses.Readings;
using olhuz_desktop_completo.Services;

namespace olhuz_desktop_completo.ViewModels.Readings
{
    public class ReadingHistoryViewModel : BindableObject
    {
        private readonly ReadingService _readingService;

        private bool _isLoading;

        // Lista de leituras exibidas na tela
        public ObservableCollection<ReadingHistoryResponse> ReadingsList { get; set; } = new();

        // Indica se as leituras estão sendo carregadas
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        // Comando para carregar as leituras
        public ICommand LoadReadingsCommand { get; }

        // Comando executado ao clicar em uma leitura
        public ICommand SelectReadingCommand { get; }

        public ReadingHistoryViewModel(ReadingService readingService)
        {
            _readingService = readingService;

            // Inicializa os comandos
            LoadReadingsCommand = new Command(async () => await LoadReadingsAsync());

            SelectReadingCommand =
                new Command<ReadingHistoryResponse>(
                    async (reading) => await GoToDetailsAsync(reading));
        }

        // Busca as leituras na API
        public async Task LoadReadingsAsync()
        {
            // Evita fazer duas requisições ao mesmo tempo
            if (IsLoading)
                return;

            IsLoading = true;

            try
            {
                var response = await _readingService.GetReadingsAsync();

                // Verifica se a API retornou os dados corretamente
                if (!response.Error && response.Data != null)
                {
                    // Limpa a lista atual
                    ReadingsList.Clear();

                    // Adiciona cada leitura recebida
                    foreach (var item in response.Data)
                    {
                        ReadingsList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erro",
                    $"Não foi possível carregar as leituras.\n\n{ex.Message}",
                    "OK");
            }
            finally
            {
                // Finaliza o carregamento mesmo se ocorrer um erro
                IsLoading = false;
            }
        }

        // Abre os detalhes da leitura selecionada
        private async Task GoToDetailsAsync(ReadingHistoryResponse reading)
        {
            // Não continua se nenhuma leitura foi selecionada
            if (reading == null)
                return;

            // Envia a leitura selecionada para a tela de detalhes
            var navigationParameter = new Dictionary<string, object>
            {
                { "ReadingDetails", reading }
            };

            await Shell.Current.GoToAsync(
                "ReadingDetailsView",
                navigationParameter);
        }
    }
}