using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

[QueryProperty(nameof(offerId), "offerId")]
public partial class Offer : ContentPage, IQueryAttributable
{
    private readonly OfferApiService offerApiService;
    private Models.Offer offer;
    private string offerId = "";
	public Offer(OfferApiService offerApiService)
	{
		InitializeComponent();
        this.offerApiService = offerApiService;
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