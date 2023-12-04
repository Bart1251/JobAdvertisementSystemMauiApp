using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;
using System.Security.Cryptography;

namespace JobAdvertisementApp.Pages;

public partial class Login : ContentPage
{
    private readonly UserApiService userApiService;
    private readonly PasswordHasher passwordHasher;

    public Login(UserApiService userApiService, PasswordHasher passwordHasher)
	{
		InitializeComponent();
        this.userApiService = userApiService;
        this.passwordHasher = passwordHasher;
    }

    private async void RegisterNavigate(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Register");
    }

    private async void LoginClick(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(Email.Text))
        {
            await DisplayAlert("Wyst¹pi³ problem", "Dane logowania s¹ nieprawid³owe", "OK");
            return;
        }
        User user = await userApiService.GetUserByEmailAsync(Email.Text);

        if (user == null || !passwordHasher.CheckPassword(user.Password, Password.Text))
        {
            await DisplayAlert("Wyst¹pi³ problem", "Dane logowania s¹ nieprawid³owe", "OK");
            return;
        }
        App.LoggedUser = user;
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}