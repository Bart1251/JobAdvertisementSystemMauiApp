using JobAdvertisementApp.Models;

namespace JobAdvertisementApp;

public partial class App : Application
{
	public static User LoggedUser { get; set; }
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
