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
            UserButton.Clicked -= ProfileNavigate;
            UserButton.Clicked += LoginNavigate;
        }
        else
        {
            UserButton.Text = "Mój profil";
            UserButton.Clicked -= LoginNavigate;
            UserButton.Clicked += ProfileNavigate;
        }
    }

	private async void LoginNavigate(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//Login");
	}

    private async void ProfileNavigate(object sender, EventArgs e)
    {
        //TODO przekierowanie na profil
        throw new NotImplementedException();
    }
}