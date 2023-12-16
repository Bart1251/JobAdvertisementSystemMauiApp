using JobAdvertisementApp.Models;

namespace JobAdvertisementApp.Controls;

public partial class OfferTile : ContentView
{
	private Offer offer;
	public OfferTile(Offer offer)
	{
		InitializeComponent();
		this.offer = offer;
		this.BindingContext = offer;
	}

	private async void Navigate(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"//Offer?offerId={offer.Id}");
	}
}