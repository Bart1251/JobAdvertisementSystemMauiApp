using JobAdvertisementApp.Controls;
using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class AdminPanel : ContentPage
{
	private readonly CompanyApiService companyApiService;
	private readonly OfferApiService offerApiService;

    public AdminPanel(CompanyApiService companyApiService, OfferApiService offerApiService)
	{
		InitializeComponent();
		this.companyApiService = companyApiService;
		this.offerApiService = offerApiService;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavBar.OnAppearing();
        GetCompanies();
    }

	private async void GetCompanies()
	{
		Companies.Children.Clear();
		List<Company> companies = (List<Company>)await companyApiService.GetAllAsync();
		foreach(Company company in companies)
		{
			Companies.Children.Add(
				new CompanyView(company, 
					(List<Models.Offer>) await offerApiService.GetAllFromCompanyAsync(company.Id.ToString()),
					DeleteCompany,
					DeleteOffer,
					AddOffer
				));
		}
	}

	private async void DeleteCompany(object sender, EventArgs e)
	{
        foreach(Models.Offer offer in await offerApiService.GetAllFromCompanyAsync(((Company)((Button)sender).CommandParameter).Id.ToString()))
			await offerApiService.DeleteAsync(offer.Id.ToString());

        await companyApiService.DeleteAsync(((Company)((Button)sender).CommandParameter).Id.ToString());
        GetCompanies();
    }

    private async void AddCompany(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("//AddCompany");
    }

    private async void AddOffer(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"//AddOffer?companyId={((Company)((Button)sender).CommandParameter).Id}");
    }
    private async void DeleteOffer(object sender, EventArgs e)
    {
		await offerApiService.DeleteAsync(((Models.Offer)((Button)sender).CommandParameter).Id.ToString());
        GetCompanies();
    }
}