using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class AddEducation : ContentPage
{
    private readonly EducationApiService educationApiService;

    public AddEducation(EducationApiService educationApiService)
    {
        InitializeComponent();
        this.educationApiService = educationApiService;
        EducationLevel.ItemsSource = Enum.GetValues(typeof(EducationLevel));
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile");
    }

    private async void Submit(object sender, EventArgs e)
    {
        if (await educationApiService.AddToIdAsync(App.LoggedUser.Id.ToString(),
            new Education()
            {
                Institution = Institution.Text,
                Town = Town.Text,
                EducationLevel = (EducationLevel)EducationLevel.SelectedItem,
                FieldOfStudy = FieldOfStudy.Text,
                PeriodOfEducationStart = PeriodOfEducationStart.Date,
                PeriodOfEducationEnd = PeriodOfEducationEnd.Date
            }))
        {
            await Shell.Current.GoToAsync("//Profile");
        }
        else
        {
            await DisplayAlert("Dodawanie wykszta³cenia", "Wyst¹pi³ b³¹d przy zapisie", "OK");
        }

    }
}