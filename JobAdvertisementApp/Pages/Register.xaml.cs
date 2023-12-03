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
                await DisplayAlert("B³¹d", "Dodanie nie powiod³o siê", "OK");
                //TODO wyœwietl b³¹d nie uda³o siê utworzyæ u¿ytkownika
            }
		}
		else
		{
			await DisplayAlert("B³¹d", "Has³a s¹ ró¿ne", "OK");
			//TODO wyœwietl b³¹d has³a s¹ ró¿ne
		}
		
	}
}