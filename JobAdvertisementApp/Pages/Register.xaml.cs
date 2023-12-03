using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class Register : ContentPage
{
	private readonly UserApiService userApiService;
	private readonly PasswordHasher passwordHasher;

	public Register(UserApiService userApiService, PasswordHasher passwordHasher)
	{
		InitializeComponent();
		this.userApiService = userApiService;
		this.passwordHasher = passwordHasher;
	}

	private async void RegisterClick(object sender, EventArgs e)
	{
		if(Password.Text == Password2.Text)
		{
			if (await userApiService.AddAsync(new User() {
				FirstName = FirstName.Text,
				SecondName = SecondName.Text,
				DateOfBirth = DateOfBirth.Date,
				PhoneNumber = PhoneNumber.Text,
				Email = Email.Text,
				Password = passwordHasher.HashPassword(Password.Text, passwordHasher.GenerateSalt())}))
			{
				await Shell.Current.GoToAsync("//MainPage");
			}
			else
			{
                await DisplayAlert("B��d", "Dodanie nie powiod�o si�", "OK");
                //TODO wy�wietl b��d nie uda�o si� utworzy� u�ytkownika
            }
		}
		else
		{
			await DisplayAlert("B��d", "Has�a s� r�ne", "OK");
			//TODO wy�wietl b��d has�a s� r�ne
		}
		
	}
}