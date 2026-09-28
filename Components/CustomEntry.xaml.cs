namespace olhuz_desktop_completo.Components;

public partial class CustomEntry : ContentView
{
    // Texto exibido acima do campo.
    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(
            nameof(LabelText),
            typeof(string),
            typeof(CustomEntry),
            string.Empty);

    // Texto exibido como dica no campo.
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomEntry),
            string.Empty);

    // Texto digitado pelo usuário.
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomEntry),
            string.Empty,
            BindingMode.TwoWay);

    // Tipo do teclado.
    public static readonly BindableProperty KeyboardTypeProperty =
        BindableProperty.Create(
            nameof(KeyboardType),
            typeof(Keyboard),
            typeof(CustomEntry),
            Keyboard.Default);

    // Limite de caracteres.
    public static readonly BindableProperty MaxLengthProperty =
        BindableProperty.Create(
            nameof(MaxLength),
            typeof(int),
            typeof(CustomEntry),
            int.MaxValue);

    // Permite acessar o texto do rótulo.
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    // Permite acessar o placeholder.
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    // Permite acessar o texto digitado.
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    // Permite definir o tipo do teclado.
    public Keyboard KeyboardType
    {
        get => (Keyboard)GetValue(KeyboardTypeProperty);
        set => SetValue(KeyboardTypeProperty, value);
    }

    // Permite definir o limite de caracteres.
    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public CustomEntry()
    {
        InitializeComponent();
    }
}