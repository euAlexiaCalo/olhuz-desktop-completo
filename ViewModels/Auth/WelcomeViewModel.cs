using System.Windows.Input;
using olhuz_desktop_completo.Views.Auth;

namespace olhuz_desktop_completo.ViewModels.Auth
{
    public class WelcomeViewModel
    {
        public ICommand GoToLoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public WelcomeViewModel()
        {
            GoToLoginCommand = new Command(
                async () => await Shell.Current.GoToAsync(
                    nameof(LoginView)));

            GoToRegisterCommand = new Command(
                async () => await Shell.Current.GoToAsync(
                    nameof(RegisterView)));
        }
    }
}