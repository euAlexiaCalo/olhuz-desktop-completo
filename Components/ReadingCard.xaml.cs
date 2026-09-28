using System.Windows.Input;

namespace olhuz_desktop_completo.Components;

public partial class ReadingCard : ContentView
{
    // =========================================================
    // IMAGEM
    // =========================================================

    public static readonly BindableProperty ImageUrlProperty =
        BindableProperty.Create(
            nameof(ImageUrl),
            typeof(string),
            typeof(ReadingCard),
            string.Empty);

    public string ImageUrl
    {
        get => (string)GetValue(ImageUrlProperty);
        set => SetValue(ImageUrlProperty, value);
    }


    // =========================================================
    // TIPO
    // =========================================================

    public static readonly BindableProperty TypeTextProperty =
        BindableProperty.Create(
            nameof(TypeText),
            typeof(string),
            typeof(ReadingCard),
            string.Empty);

    public string TypeText
    {
        get => (string)GetValue(TypeTextProperty);
        set => SetValue(TypeTextProperty, value);
    }


    // =========================================================
    // TÍTULO
    // =========================================================

    public static readonly BindableProperty TitleTextProperty =
        BindableProperty.Create(
            nameof(TitleText),
            typeof(string),
            typeof(ReadingCard),
            string.Empty);

    public string TitleText
    {
        get => (string)GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }


    // =========================================================
    // DATA
    // =========================================================

    public static readonly BindableProperty DateTextProperty =
        BindableProperty.Create(
            nameof(DateText),
            typeof(string),
            typeof(ReadingCard),
            string.Empty);

    public string DateText
    {
        get => (string)GetValue(DateTextProperty);
        set => SetValue(DateTextProperty, value);
    }


    // =========================================================
    // COMANDO DO CLIQUE
    // =========================================================

    public static readonly BindableProperty CardTappedCommandProperty =
        BindableProperty.Create(
            nameof(CardTappedCommand),
            typeof(ICommand),
            typeof(ReadingCard));

    public ICommand CardTappedCommand
    {
        get => (ICommand)GetValue(CardTappedCommandProperty);
        set => SetValue(CardTappedCommandProperty, value);
    }


    // =========================================================
    // PARÂMETRO DO COMANDO
    // =========================================================

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ReadingCard));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }


    // =========================================================
    // CONSTRUTOR
    // =========================================================

    public ReadingCard()
    {
        InitializeComponent();
    }
}