using Microsoft.Extensions.Logging;
using olhuz_desktop_completo.Services;
using olhuz_desktop_completo.Views.Auth;
using olhuz_desktop_completo.Views.Home;
using olhuz_desktop_completo.Views.Preferences;
using olhuz_desktop_completo.Views.Readings;
using olhuz_desktop_completo.Views.User;
using olhuz_desktop_completo.ViewModels.Auth;
using olhuz_desktop_completo.ViewModels.Home;
using olhuz_desktop_completo.ViewModels.Preferences;
using olhuz_desktop_completo.ViewModels.Readings;
using olhuz_desktop_completo.ViewModels.User;

namespace olhuz_desktop_completo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // =========================================================
            // CONFIGURAÇÃO DA API
            // =========================================================
            // Registra automaticamente o JWT nas requisições
            builder.Services.AddTransient<AuthTokenHandler>();

            // Cria o HttpClient utilizando o AuthTokenHandler
            builder.Services.AddHttpClient(string.Empty, client =>
            {
                client.BaseAddress = new Uri("https://olhuz-api.onrender.com/");
            })
            .AddHttpMessageHandler<AuthTokenHandler>();

            // =========================================================
            // REGISTRO DOS SERVIÇOS
            // =========================================================
            builder.Services.AddSingleton<AuthSessionService>();
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<ReadingService>();
            builder.Services.AddTransient<UserPreferencesService>();
            builder.Services.AddTransient<UserService>();

            // =========================================================
            // VIEWMODELS
            // =========================================================
            builder.Services.AddTransient<WelcomeViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<PreferencesViewModel>();
            builder.Services.AddTransient<ReadingHistoryViewModel>();
            builder.Services.AddTransient<ReadingDetailsViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();

            // =========================================================
            // REGISTRO DAS VIEWS
            // =========================================================
            builder.Services.AddTransient<WelcomeView>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<ReadingHistoryView>();
            builder.Services.AddTransient<ReadingDetailsView>();
            builder.Services.AddTransient<PreferencesView>();
            builder.Services.AddTransient<ProfileView>();
            //builder.Services.AddTransient<EditProfileView>();

            // =========================================================
            // SHELL
            // =========================================================
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<AuthShell>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
