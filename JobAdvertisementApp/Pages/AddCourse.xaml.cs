using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class AddCourse : ContentPage
{
    private readonly CourseApiService courseApiService;

    public AddCourse(CourseApiService courseApiService)
    {
        InitializeComponent();
        this.courseApiService = courseApiService;
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Profile");
    }

    private async void Submit(object sender, EventArgs e)
    {
        if (await courseApiService.AddToIdAsync(App.LoggedUser.Id.ToString(),
            new Course()
            {
                Name = Name.Text,
                Organizer = Organizer.Text,
                CourseStart = CourseStart.Date,
                CourseEnd = CourseEnd.Date
            }))
        {
            foreach (var child in MainGrid.Children)
            {
                if (child is Entry entry)
                    entry.Text = "";
                if (child is DatePicker datePicker)
                    datePicker.Date = DateTime.Now;
            }
            await Shell.Current.GoToAsync("//Profile");
        }
        else
        {
            await DisplayAlert("Dodawanie kursu/szkolenia", "Wyst¹pi³ b³¹d przy zapisie", "OK");
        }
    }
}