namespace JobAdvertisementApp.Pages;

public partial class AddJobExperience : ContentPage
{
	public AddJobExperience()
	{
		InitializeComponent();
	}

	private async void GoBack(object sender, EventArgs e)
	{
		await Shell.Current.Navigation.PopModalAsync();
	}
}