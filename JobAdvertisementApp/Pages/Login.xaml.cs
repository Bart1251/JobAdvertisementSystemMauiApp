using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class Login : ContentPage
{
    private readonly UserApiService userApiService;

    public Login(UserApiService userApiService)
	{
		InitializeComponent();
        this.userApiService = userApiService;
    }

    private async void RegisterNavigate(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Register");
    }

    private void LoginClick(object sender, EventArgs e)
    {
        
    }
}