using JobAdvertisementApp.Pages;

namespace JobAdvertisementApp.Controls;

public partial class NavBar : ContentView
{
	public NavBar()
	{
		InitializeComponent();
	}

	private async void LoginNavigate(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//Login");
	}
}