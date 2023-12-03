using JobAdvertisementApp.Pages;

namespace JobAdvertisementApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("MainPage/Login", typeof(Login));
		Routing.RegisterRoute("MainPage/Login/Register", typeof(Register));
    }
}
