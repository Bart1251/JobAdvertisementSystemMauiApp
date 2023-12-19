using JobAdvertisementApp.Models;

namespace JobAdvertisementApp.Controls;

public partial class CompanyView : ContentView
{
	private EventHandler deleteOfferHandler;

    public CompanyView(Company company, List<Offer> offers, EventHandler deleteHandler, EventHandler deleteOfferHandler, EventHandler addOfferHandler)
	{
		InitializeComponent();
		this.BindingContext = company;
		((IView)MainInfoGrid).InvalidateMeasure();
		BindableLayout.SetItemsSource(Offers, offers);
		((IView)Offers.Parent).InvalidateMeasure();
		Delete.Clicked += deleteHandler;
		AddOffer.Clicked += addOfferHandler;
		this.deleteOfferHandler = deleteOfferHandler;
    }

	private void DeleteOffer(object sender, EventArgs e)
	{
		deleteOfferHandler.Invoke(sender, e);
	}
}