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
            UserButton.Text = "Zaloguj si�";
        }
        else
        {
            UserButton.Text = "M�j profil";
        }
    }

	private async void UserButtonNavigate(object sender, EventArgs e)
	{
		if(App.LoggedUser == null)
		{
			await Shell.Current.GoToAsync("//Login");
		}
		else
		{
			//TODO doa� przekierowanie do profilu
		}
	}
}