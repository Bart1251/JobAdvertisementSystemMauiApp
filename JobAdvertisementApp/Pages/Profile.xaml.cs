using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;
using System.Collections.ObjectModel;

namespace JobAdvertisementApp.Pages;

public partial class Profile : ContentPage
{
	private readonly UserApiService userApiService;
	private readonly ProfileApiService profileApiService;

	private ObservableCollection<Models.Profile> profiles = new ObservableCollection<Models.Profile>();

	public Profile(UserApiService userApiService, ProfileApiService profileApiService)
	{
		InitializeComponent();
		this.userApiService = userApiService;
		this.profileApiService = profileApiService;
		GetData();
	}

	private async void GetData()
	{
		IEnumerable<Models.Profile> response = await profileApiService.GetAllFromIdAsync(App.LoggedUser.Id.ToString());
        foreach(var profile in response)
			profiles.Add(profile);
        Links.ItemsSource = profiles;
        PersonalInfo1.BindingContext = App.LoggedUser;
        PersonalInfo2.BindingContext = App.LoggedUser;
    }

	private async void GoBack(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//MainPage");
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavBar.OnAppearing();
    }

	private async Task<bool> Save()
	{
		return await userApiService.UpdateAsync(App.LoggedUser.Id.ToString(), App.LoggedUser);
	}

	private async void UpdateDateOfBirth(object sender, EventArgs e)
	{
        try
		{
			string result = await DisplayPromptAsync("Zmaina warto�ci", "Wprowad� dat� urodzenia", placeholder: "01-01-2023", initialValue: App.LoggedUser.DateOfBirth.ToShortDateString());
			if (string.IsNullOrEmpty(result))
				return;
            DateTime date = DateTime.Parse(result);
			if (date.Year > DateTime.Now.Year)
				throw new ArgumentException();
            DateTime previous = App.LoggedUser.DateOfBirth;
            App.LoggedUser.DateOfBirth = date;
            if (!await Save())
            {
                App.LoggedUser.DateOfBirth = previous;
                await DisplayAlert("Zmiana warto�ci", "Wyst�pi� b��d przy zapisie", "OK");
                return;
            }
        ((Label)sender).Text = result;
        }
		catch(Exception)
		{
			await DisplayAlert("Wyst�pi� problem", "Nieprawid�owe dane", "OK");
		}
	}

    private async void UpdateAdress(object sender, EventArgs e)
    {
		string result = await DisplayPromptAsync("Zmiana warto�ci", "Wprowad� adres", initialValue: App.LoggedUser.Adress);
		if (string.IsNullOrEmpty(result))
			return;
        string previous = App.LoggedUser.Adress;
        App.LoggedUser.Adress = result;
        if (!await Save())
        {
            App.LoggedUser.Adress = previous;
            await DisplayAlert("Zmiana warto�ci", "Wyst�pi� b��d przy zapisie", "OK");
            return;
        }
        ((Label)sender).Text = result;
    }

    private async void UpdatePhoneNumber(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana warto�ci", "Wprowad� numer telefonu", initialValue: App.LoggedUser.PhoneNumber);
        if (string.IsNullOrEmpty(result))
            return;
        string previous = App.LoggedUser.PhoneNumber;
        App.LoggedUser.PhoneNumber = result;
        if (!await Save())
        {
            App.LoggedUser.PhoneNumber = previous;
            await DisplayAlert("Zmiana warto�ci", "Wyst�pi� b��d przy zapisie", "OK");
            return;
        }
        ((Label)sender).Text = result;
    }

    private async void UpdateEmail(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana warto�ci", "Wprowad� email", initialValue: App.LoggedUser.Email);
        if (string.IsNullOrEmpty(result))
            return;
        string previous = App.LoggedUser.Email;
        App.LoggedUser.Email = result;
        if (!await Save())
        {
            App.LoggedUser.Email = previous;
            await DisplayAlert("Zmiana warto�ci", "Wyst�pi� b��d przy zapisie", "OK");
            return;
        }
        ((Label)sender).Text = result;
    }

    private async void UpdateName(object sender, EventArgs e)
    {
		string action = await DisplayActionSheet("Kt�r� warto�� chcesz zmieni�", "Anuluj", null, "Imi�", "Nazwisko");
		if (action == null)
			return;

		string result = "";
		if(action == "Imi�")
		{
			result = await DisplayPromptAsync("Zmiana warto�ci", "Wprowad� imi�", initialValue: App.LoggedUser.FirstName);
            if (string.IsNullOrEmpty(result))
                return;
            string previous = App.LoggedUser.FirstName;
            App.LoggedUser.FirstName = result;
            if (!await Save())
            {
                await DisplayAlert("Zmiana warto�ci", "Wyst�pi� b��d przy zapisie", "OK");
                App.LoggedUser.FirstName = previous;
                return;
            }
        }
		else
		{
            result = await DisplayPromptAsync("Zmiana warto�ci", "Wprowad� nazwisko", initialValue: App.LoggedUser.SecondName);
            if (string.IsNullOrEmpty(result))
                return;
			string previous = App.LoggedUser.SecondName;
			App.LoggedUser.SecondName = result;
            if (!await Save())
			{
                await DisplayAlert("Zmiana warto�ci", "Wyst�pi� b��d przy zapisie", "OK");
				App.LoggedUser.SecondName = previous;
				return;
            }
        }

        ((Label)sender).Text = App.LoggedUser.FirstName + " " + App.LoggedUser.SecondName;
    }

    private async void UpdateOccupation(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana warto�ci", "Wprowad� zaw�d", initialValue: App.LoggedUser.Occupation);
		if (string.IsNullOrEmpty(result))
		{
			if (!await DisplayAlert("Zmiana warto�ci", "Czy chcesz usun�� zaw�d", "Tak", "Nie"))
				return;
		}
		string previous = App.LoggedUser.Occupation;
		App.LoggedUser.Occupation = result;
        if (!await Save())
		{
			App.LoggedUser.Occupation = previous;
            await DisplayAlert("Zmiana warto�ci", "Wyst�pi� b��d przy zapisie", "OK");
			return;
		}
		((Label)sender).Text = result;
    }

    private async void AddLink(object sender, EventArgs e)
	{
		string result = await DisplayPromptAsync("Dodawanie odno�nika do profilu", "Wprowad� odno�nik");
		if (string.IsNullOrEmpty(result))
			return;
		Models.Profile newProfile = new Models.Profile() { Link = result };
		if(await profileApiService.AddToIdAsync(App.LoggedUser.Id.ToString(), newProfile))
			profiles.Add(newProfile);
		else
			await DisplayAlert("Dodawanie odno�nika do profilu", "Wyst�pi� b��d przy dodawaniu", "OK");
	}

	private async void DeleteLink(object sender, EventArgs e)
	{
		if (await DisplayAlert("Usuwanie odno�nika", "Czy chcesz usun�� odno�nik?", "Tak", "Nie"))
		{
			Models.Profile profileToDelete = ((TapGestureRecognizer)((Label)sender).GestureRecognizers[0]).CommandParameter as Models.Profile;
			if(await profileApiService.DeleteAsync(profileToDelete.Id.ToString()))
				profiles.Remove(profileToDelete);
			else
                await DisplayAlert("Usuwanie odno�nika do profilu", "Wyst�pi� b��d przy usuwaniu", "OK");
        }
	}

}