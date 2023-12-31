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
    private readonly UserApiService userApiService;

    private Models.Offer offer;
    private string offerId = "";
    private int mapZoomLevel = 13;
    public Offer(OfferApiService offerApiService, MapService mapService, CompanyApiService companyApiService, UserApiService userApiService)
	{
		InitializeComponent();
        this.offerApiService = offerApiService;
        this.mapService = mapService;
        this.companyApiService = companyApiService;
        this.userApiService = userApiService;
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
        if(App.LoggedUser != null)
        {
            if ((await offerApiService.GetAllFromUserAsync(App.LoggedUser.Id.ToString())).Any(e => e.Id == offer.Id))
            {
                ApplyButton.Text = "Zrezygnuj";
            }
            else
            {
                ApplyButton.Text = "Aplikuj";
            }
            string path = Path.Combine(FileSystem.AppDataDirectory, "savedOffers/" + App.LoggedUser.Id.ToString() + ".txt");
            string directory = Path.Combine(FileSystem.AppDataDirectory, "savedOffers");
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            if (!File.Exists(path))
                using (var stream = File.Create(path)) { }
            string[] content = File.ReadAllLines(path);
            if (content.Any(line => line == offer.Id.ToString()))
            {
                SaveButton.Text = "Usuñ";
            }
            else
            {
                SaveButton.Text = "Zapisz";
            }
        }
        else
        {
            SaveButton.Text = "Zapisz";
            ApplyButton.Text = "Aplikuj";
        }
        this.BindingContext = offer;
        byte[] imageBytes = await companyApiService.GetImage(offer.Company.Id.ToString());
        if (imageBytes != null)
            CompanyLogo.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        BindableLayout.SetItemsSource(Responsibilities, offer.Responsibilities.Split(";"));
        BindableLayout.SetItemsSource(Requirements, offer.Requirements.Split(";"));
        BindableLayout.SetItemsSource(Benefits, offer.Benefits.Split(";"));
        Location.Source = await mapService.GetMapImageAsync(offer.Company.Location.Split(";").FirstOrDefault().Replace(",", ".") + "," + offer.Company.Location.Split(";").Last().Replace(",", "."));
    }

    private async void Apply(object sender, EventArgs e)
    {
        if (App.LoggedUser == null)
        {
            await DisplayAlert("Aplikowanie", "Zaloguj siê by móc aplikowaæ", "OK");
            return;
        }
        if (!(await offerApiService.GetAllFromUserAsync(App.LoggedUser.Id.ToString())).Any(e => e.Id == offer.Id))
        {
            if (await userApiService.UserAddOfferAsync(App.LoggedUser.Id.ToString(), offer.Id.ToString()))
            {
                ((Button)sender).Text = "Zrezygnuj";
            }
            else
            {
                await DisplayAlert("Aplikowanie", "Wyst¹pi³ b³¹d przy zapisie", "OK");
            }
        }
        else
        {
            if (await userApiService.UserDeleteOfferAsync(App.LoggedUser.Id.ToString(), offer.Id.ToString()))
            {
                ((Button)sender).Text = "Aplikuj";
            }
            else
            {
                await DisplayAlert("Rezygnacja", "Wyst¹pi³ b³¹d przy zapisie", "OK");
            }
        }
    }

    private async void Save(object sender, EventArgs e)
    {
        if (App.LoggedUser == null)
        {
            await DisplayAlert("Zapisywanie", "Zaloguj siê by móc zapisywaæ", "OK");
            return;
        }
        string path = Path.Combine(FileSystem.AppDataDirectory, "savedOffers/" + App.LoggedUser.Id.ToString() + ".txt");
        string directory = Path.Combine(FileSystem.AppDataDirectory, "savedOffers");
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        if (!File.Exists(path))
            using (var stream = File.Create(path)) { }
        string[] content = File.ReadAllLines(path);
        if(content.Any(line => line == offer.Id.ToString()))
        {
            string[] newContent = content.Where(line => line != offer.Id.ToString()).ToArray();
            File.WriteAllLines(path, newContent);
            ((Button)sender).Text = "Zapisz";
        }
        else
        {
            string[] newContent = new string[content.Length + 1];
            for (int i = 0; i < content.Length; i++)
            {
                newContent[i] = content[i];
            }
            newContent[newContent.Length - 1] = offer.Id.ToString();
            File.WriteAllLines(path, newContent);
            ((Button)sender).Text = "Usuñ";
        }
    }

    private async void ResizeMapBigger(object sender, EventArgs e)
    {
        if (mapZoomLevel < 19)
            mapZoomLevel++;
        Location.Source = null;
        var mapImageStream = await mapService.GetMapImageAsync(offer.Company.Location.Split(";").FirstOrDefault().Replace(",", ".") + "," + offer.Company.Location.Split(";").Last().Replace(",", "."), mapZoomLevel);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (mapImageStream != null)
            {
                Location.Source = mapImageStream;
            }
        });
    }

    private async void ResizeMapSmaller(object sender, EventArgs e)
    {
        if (mapZoomLevel > 1)
            mapZoomLevel--;
        Location.Source = null;
        var mapImageStream = await mapService.GetMapImageAsync(offer.Company.Location.Split(";").FirstOrDefault().Replace(",", ".") + "," + offer.Company.Location.Split(";").Last().Replace(",", "."), mapZoomLevel);
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (mapImageStream != null)
            {
                Location.Source = mapImageStream;
            }
        });
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