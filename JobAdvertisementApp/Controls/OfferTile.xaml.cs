using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Controls;

public partial class OfferTile : ContentView
{
	private Offer offer;

	public OfferTile(Offer offer, ImageSource companyLogo)
	{
		InitializeComponent();
		this.offer = offer;
		this.BindingContext = offer;
		CompanyLogo.Source = companyLogo;
    }

	private async void Navigate(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"//Offer?offerId={offer.Id}");
	}
}