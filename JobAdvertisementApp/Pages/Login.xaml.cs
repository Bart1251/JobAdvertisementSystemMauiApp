namespace JobAdvertisementApp.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void RegisterNavigate(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Register));
    }
}