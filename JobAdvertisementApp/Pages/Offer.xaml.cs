using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;
using Microsoft.Maui.Maps;

namespace JobAdvertisementApp.Pages;

[QueryProperty(nameof(offerId), "offerId")]
public partial class Offer : ContentPage, IQueryAttributable
{
    private readonly OfferApiService offerApiService;
    private readonly MapService mapService;
    private readonly CompanyApiService companyApiService;

    private Models.Offer offer;
    private string offerId = "";
	public Offer(OfferApiService offerApiService, MapService mapService, CompanyApiService companyApiService)
	{
		InitializeComponent();
        this.offerApiService = offerApiService;
        this.mapService = mapService;
        this.companyApiService = companyApiService;
    }

    protected override void OnAppearing()
	{
		base.OnAppearing();
		NavBar.OnAppearing();
        Resize();
    }

    private async void GetOffer()
    {
        offer = await offerApiService.GetAsync(offerId);
        this.BindingContext = offer;
        byte[] imageBytes = await companyApiService.GetImage(offer.Company.Id.ToString());
        if (imageBytes != null)
            CompanyLogo.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        BindableLayout.SetItemsSource(Responsibilities, offer.Responsibilities.Split(";"));
        BindableLayout.SetItemsSource(Requirements, offer.Requirements.Split(";"));
        BindableLayout.SetItemsSource(Benefits, offer.Benefits.Split(";"));
        Location.Source = await mapService.GetMapImageAsync(offer.Company.Location.Split(";").First() + "," + offer.Company.Location.Split(";").Last());
    }

    private void PageSizeChanged(object sender, EventArgs e)
	{
        Resize();
    }

    private void Resize()
    {
        if (this.Width < 600)
        {
            Grid.SetColumn(WegeFrame, 0);
            Grid.SetRow(WegeFrame, 1);
            Grid.SetColumnSpan(WegeFrame, 3);

            Grid.SetColumn(Right, 0);
            Grid.SetRow(Right, 1);
            Grid.SetColumnSpan(Right, 2);
            Grid.SetColumnSpan(Left, 2);
        }
        else
        {
            Grid.SetColumn(WegeFrame, 2);
            Grid.SetRow(WegeFrame, 0);
            Grid.SetColumnSpan(WegeFrame, 1);

            Grid.SetColumn(Right, 1);
            Grid.SetRow(Right, 0);
            Grid.SetColumnSpan(Right, 1);
            Grid.SetColumnSpan(Left, 1);
        }
        MainContainer.Margin = new Thickness(Math.Pow(Math.Log10((this.Width - 400) > 0 ? this.Width - 400 : 1), 5), 10);
        ((IView)MainContainer).InvalidateMeasure();
        ((IView)PageGrid).InvalidateMeasure();
        ((IView)ScrollView).InvalidateMeasure();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        offerId = query["offerId"] as string;
        GetOffer();
    }
}