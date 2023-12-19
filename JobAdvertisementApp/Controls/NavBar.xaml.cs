using JobAdvertisementApp.Pages;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Controls;

public partial class NavBar : ContentView
{
	public NavBar()
	{
		InitializeComponent();
	}

	public void OnAppearing()
	{
        if (App.LoggedUser == null)
        {
            UserButton.Text = "Zaloguj siê";
        }
        else if(Shell.Current.CurrentPage.GetType() == typeof(Profile) || Shell.Current.CurrentPage.GetType() == typeof(AdminPanel))
		{
			UserButton.Text = "Wyloguj siê";
		}
		else if(App.LoggedUser.IsAdmin == true)
		{
			UserButton.Text = "Admin panel";
		}
		else
        {
            UserButton.Text = "Mój profil";
        }
		this.InvalidateMeasure();
    }

	private async void UserButtonNavigate(object sender, EventArgs e)
	{
		if (App.LoggedUser == null)
		{
			await Shell.Current.GoToAsync("//Login");
		}
		else if (Shell.Current.CurrentPage.GetType() == typeof(Profile) || Shell.Current.CurrentPage.GetType() == typeof(AdminPanel))
		{
			App.LoggedUser = null;
			await Shell.Current.GoToAsync("//MainPage");
		}
        else if (App.LoggedUser.IsAdmin == true)
		{
            await Shell.Current.GoToAsync("//AdminPanel");
        }
		else
        {
			await Shell.Current.GoToAsync("//Profile");
		}
	}

	private async void MainNavigate(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//MainPage");
	}
}