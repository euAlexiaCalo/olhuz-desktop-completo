namespace olhuz_desktop_completo.Components;

public partial class PasswordEntry : ContentView
{
    // Rótulo exibido acima do campo.
    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(
            nameof(LabelText),
            typeof(string),
            typeof(PasswordEntry),
            string.Empty);

    // Texto exibido como dica no campo.
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(PasswordEntry),
            string.Empty);

    // Texto digitado pelo usuário.
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(PasswordEntry),
            string.Empty,
            BindingMode.TwoWay);

    // Define se a senha fica escondida.
    public static readonly BindableProperty IsPasswordHiddenProperty =
        BindableProperty.Create(
            nameof(IsPasswordHidden),
            typeof(bool),
            typeof(PasswordEntry),
            true);

    // Imagem do ícone do olho.
    public static readonly BindableProperty IconSourceProperty =
        BindableProperty.Create(
            nameof(IconSource),
            typeof(string),
            typeof(PasswordEntry),
            "eye_closed.png");

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsPasswordHidden
    {
        get => (bool)GetValue(IsPasswordHiddenProperty);
        set => SetValue(IsPasswordHiddenProperty, value);
    }

    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public PasswordEntry()
    {
        InitializeComponent();
    }

    // Alterna entre senha escondida e visível.
    private void OnTogglePasswordTapped(object sender, TappedEventArgs e)
    {
        IsPasswordHidden = !IsPasswordHidden;

        IconSource = IsPasswordHidden
            ? "eye_closed.png"
            : "eye_open.png";
    }
}