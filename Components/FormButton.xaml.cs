using System.Windows.Input;

namespace olhuz_desktop_completo.Components;

public partial class FormButton : ContentView
{
    // Texto exibido no botão.
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(FormButton),
            string.Empty);

    // Cor de fundo do botão.
    public static readonly BindableProperty ButtonColorProperty =
        BindableProperty.Create(
            nameof(ButtonColor),
            typeof(Color),
            typeof(FormButton),
            Colors.Blue);

    // Indica se o botão está carregando.
    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(FormButton),
            false);

    // Comando executado ao clicar no botão.
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(FormButton),
            null);

    // Permite acessar o texto.
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    // Permite acessar a cor do botão.
    public Color ButtonColor
    {
        get => (Color)GetValue(ButtonColorProperty);
        set => SetValue(ButtonColorProperty, value);
    }

    // Permite controlar o estado de carregamento.
    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    // Permite receber um comando do ViewModel.
    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public FormButton()
    {
        InitializeComponent();
    }
}