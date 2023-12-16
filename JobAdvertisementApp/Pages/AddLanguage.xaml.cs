using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class AddLanguage : ContentPage
{
    private readonly LanguageApiService languageApiService;

    public AddLanguage(LanguageApiService languageApiService)
    {
        InitializeComponent();
        this.languageApiService = languageApiService;
        SetPickers();
    }

    private async Task SetPickers()
    {
        LanguageLevel.ItemsSource = Enum.GetValues(typeof(LanguageLevel));
        List<Language> languages = (List<Language>)await languageApiService.GetAllAsync();
        for (int i = 0; i < languages.Count;)
        {
            if (languages.Any(e => e.Name == languages[i].Name && languages[i].Id != e.Id))
            {
                languages.Remove(languages[i]);
            }
            else
            {
                i++;
            }
        }
        LanguageName.ItemsSource = languages;
        if(DeviceInfo.Platform == DevicePlatform.Android) LanguageName.ItemsSource = LanguageName.GetItemsAsArray();
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile");
    }

    private async void Submit(object sender, EventArgs e)
    {
        if (await languageApiService.UserAddLanguageAsync(App.LoggedUser.Id.ToString(), 
            ((List<Language>)await languageApiService.GetAllAsync())
            .Single(e => e.Name == LanguageName.SelectedItem.ToString() && e.Level == (LanguageLevel)LanguageLevel.SelectedItem).Id.ToString()))
        {
            await Shell.Current.GoToAsync("//Profile");
        }
        else
        {
            await DisplayAlert("Dodawanie jêzyka", "Wyst¹pi³ b³¹d przy zapisie", "OK");
        }
    }
}