using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;
using System.Text.RegularExpressions;

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
		if(await Validate())
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
            }
		}
	}

	private async Task<bool> Validate()
	{
		Dictionary<string, Entry> validationDictionary = new Dictionary<string, Entry>()
		{
			{"^[A-Za-z]{2,20}$", FirstName },
			{"^[A-Za-z'\\-]{2,30}$", SecondName},
			{"(?<!\\w)(\\(?(\\+|00)?48\\)?)?[ -]?\\d{3}[ -]?\\d{3}[ -]?\\d{3}(?!\\w)", PhoneNumber },
			{"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", Email},
			{"^((?=\\S*?[A-Z])(?=\\S*?[a-z])(?=\\S*?[0-9]).{6,})\\S$", Password}
		};

		foreach(var validationPair in validationDictionary)
		{
			if(string.IsNullOrEmpty(validationPair.Value.Text) || !new Regex(validationPair.Key).IsMatch(validationPair.Value.Text))
			{
				validationPair.Value.BackgroundColor = Colors.Red;
				validationPair.Value.Focus();
				validationPair.Value.TextChanged += DeleteBackground;
                await DisplayAlert("Wyst¹pi³ problem", "Nieprawid³owe dane", "OK");
				return false;
            }
		}

		if(Password.Text != Password2.Text)
		{
			Password2.BackgroundColor = Colors.Red;
            Password2.Focus();
            Password2.TextChanged += DeleteBackground;
            await DisplayAlert("Wyst¹pi³ problem", "Has³a s¹ ró¿ne", "OK");
			return false;
        }

		if(DateOfBirth.Date > DateTime.Now || DateTime.Now.Year - DateOfBirth.Date.Year > 100)
		{
			DateOfBirth.BackgroundColor = Colors.Red;
			DateOfBirth.Focus();
            DateOfBirth.DateSelected += DeleteBackground;
            await DisplayAlert("Wyst¹pi³ problem", "Nieprawid³owe dane", "OK");
			return false;
        }
		
		return true;
	}

	private void DeleteBackground(object sender, EventArgs e)
	{
		if (sender.GetType() == typeof(Entry))
		{
			((Entry)sender).BackgroundColor = Color.FromHex("#222222");
			((Entry)sender).TextChanged -= DeleteBackground;
		}
		if(sender.GetType() == typeof(DatePicker))
		{
			((DatePicker)sender).BackgroundColor = Color.FromHex("#222222");
            ((DatePicker)sender).DateSelected -= DeleteBackground;
		}
	}

	private async void GoBack(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//Login");
	}
}