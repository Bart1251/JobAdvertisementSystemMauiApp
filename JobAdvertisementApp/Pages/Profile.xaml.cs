namespace JobAdvertisementApp.Pages;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

	private async void GoBack(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//MainPage");
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavBar.OnAppearing();
	}
}