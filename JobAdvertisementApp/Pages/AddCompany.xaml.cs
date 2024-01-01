using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class AddCompany : ContentPage
{
	private readonly CompanyApiService companyApiService;

    byte[] logoBytes;

	public AddCompany(CompanyApiService companyApiService)
	{
		InitializeComponent();
		this.companyApiService = companyApiService;
	}

	private async void GoBack(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//AdminPanel");
	}

	private async void AddLogo(object sender, EventArgs e)
	{
        var file = await MediaPicker.PickPhotoAsync();
        if (file != null)
        {
            using (Stream stream = await file.OpenReadAsync())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    logoBytes = ms.ToArray();
                }
            }
			((Button)sender).Text = "Zmieñ logo";
        }
    }

    private async void Submit(object sender, EventArgs e)
    {
		await companyApiService.AddAsync(new Company()
		{
			Name = Name.Text,
			Adress = Address.Text,
			Description = Description.Text
		});
		await companyApiService.UploadImage((await companyApiService.GetAllAsync()).Single(e => e.Name == Name.Text && e.Adress == Address.Text && e.Description == Description.Text).Id.ToString(), logoBytes);
		LogoButton.Text = "Dodaj logo";
		foreach(var child in MainGrid.Children)
		{
			if(child is Entry entry)
			{
				entry.Text = "";
			}
		}
		await Shell.Current.GoToAsync("//AdminPanel");
    }
}