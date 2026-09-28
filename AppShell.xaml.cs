namespace olhuz_desktop_completo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Rotas de telas que não ficam diretamente no menu
            Routing.RegisterRoute("ReadingDetailsView", typeof(Views.Readings.ReadingDetailsView));
            //Routing.RegisterRoute("EditProfile", typeof(Views.User.EditProfileView));
        }
    }
}
