using olhuz_desktop_completo.ViewModels.Readings;

namespace olhuz_desktop_completo.Views.Readings;

public partial class ReadingDetailsView : ContentPage
{
    private readonly ReadingDetailsViewModel _viewModel;

    public ReadingDetailsView(ReadingDetailsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _viewModel.StopSpeaking();
    }
}