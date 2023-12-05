using System.Collections.ObjectModel;

namespace JobAdvertisementApp.Pages;

public partial class Profile : ContentPage
{
	private ObservableCollection<Models.Profile> profiles = new ObservableCollection<Models.Profile>();

	public Profile()
	{
		InitializeComponent();
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

	private async void UpdateDateOfBirth(object sender, EventArgs e)
	{
        try
		{
			string result = await DisplayPromptAsync("Zmaina wartoúci", "Wprowadü datÍ urodzenia", placeholder: "01-01-2023", initialValue: App.LoggedUser.DateOfBirth.ToShortDateString());
			if (string.IsNullOrEmpty(result))
				return;
            DateTime date = DateTime.Parse(result);
			if (date.Year > DateTime.Now.Year)
				throw new ArgumentException();
			((Label)sender).Text = date.ToString("dd-MM-yyyy");
			App.LoggedUser.DateOfBirth = date;
        }
		catch(Exception)
		{
			await DisplayAlert("Wystπpi≥ problem", "Nieprawid≥owe dane", "OK");
		}
	}

    private async void UpdateAdress(object sender, EventArgs e)
    {
		string result = await DisplayPromptAsync("Zmiana wartoúci", "Wprowadü adres", initialValue: App.LoggedUser.Adress);
		if (string.IsNullOrEmpty(result))
			return;
		((Label)sender).Text = result;
		App.LoggedUser.Adress = result;
    }

    private async void UpdatePhoneNumber(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana wartoúci", "Wprowadü numer telefonu", initialValue: App.LoggedUser.PhoneNumber);
        if (string.IsNullOrEmpty(result))
            return;
        ((Label)sender).Text = result;
        App.LoggedUser.PhoneNumber = result;
    }

    private async void UpdateEmail(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana wartoúci", "Wprowadü email", initialValue: App.LoggedUser.Email);
        if (string.IsNullOrEmpty(result))
            return;
        ((Label)sender).Text = result;
        App.LoggedUser.Email = result;
    }

	private async void AddLink(object sender, EventArgs e)
	{
		string result = await DisplayPromptAsync("Dodawanie odnoúnika do profilu", "Wprowadü odnoúnik");
		if (string.IsNullOrEmpty(result))
			return;
		profiles.Add(new Models.Profile() { Link = result });
	}

	private async void DeleteLink(object sender, EventArgs e)
	{
		if (await DisplayAlert("Usuwanie odnoúnika", "Czy chcesz usunπÊ odnoúnik?", "Tak", "Nie"))
		{
			profiles.Remove(((TapGestureRecognizer)((Label)sender).GestureRecognizers[0]).CommandParameter as Models.Profile);
		}
	}

}