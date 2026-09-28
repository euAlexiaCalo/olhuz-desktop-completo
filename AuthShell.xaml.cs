namespace olhuz_desktop_completo;

public partial class AuthShell : Shell
{
	public AuthShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("LoginView", typeof(Views.Auth.LoginView));
        Routing.RegisterRoute("RegisterView", typeof(Views.Auth.RegisterView));
    }
}