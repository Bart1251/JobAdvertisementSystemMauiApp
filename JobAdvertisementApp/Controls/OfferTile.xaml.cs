namespace JobAdvertisementApp.Controls;

public partial class OfferTile : ContentView
{
	public OfferTile()
	{
		InitializeComponent();
	}

	private async void Navigate(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//Offer");
	}
}