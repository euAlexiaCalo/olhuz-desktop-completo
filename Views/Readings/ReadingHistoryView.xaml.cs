using olhuz_desktop_completo.ViewModels.Readings;

namespace olhuz_desktop_completo.Views.Readings;

public partial class ReadingHistoryView : ContentPage
{
    private readonly ReadingHistoryViewModel _viewModel;

    public ReadingHistoryView(ReadingHistoryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Carrega a lista automaticamente toda vez que a tela é aberta
        await _viewModel.LoadReadingsAsync();
    }
}