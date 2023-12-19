using JobAdvertisementApp.Services;
using System.Collections.ObjectModel;

namespace JobAdvertisementApp.Pages;

public partial class AddJobExperience : ContentPage
{
	private readonly JobExperienceApiService jobExperienceApiService;

	private ObservableCollection<string> responsibilities = new ObservableCollection<string>();

	public AddJobExperience(JobExperienceApiService jobExperienceApiService)
	{
		InitializeComponent();
		BindableLayout.SetItemsSource(Responsibilities, responsibilities);
		this.jobExperienceApiService = jobExperienceApiService;
	}

	private async void GoBack(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//Profile");
	}

	private void Checked(object sender, EventArgs e)
	{
		PeriodOdEmploymentEnd.IsVisible = !((CheckBox)sender).IsChecked;
	}

	private void AddResponsibility(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(Responsibility.Text) && !responsibilities.Contains(Responsibility.Text) && !Responsibility.Text.Contains(";"))
		{
			responsibilities.Add(Responsibility.Text);
			((IView)MainGrid).InvalidateMeasure();
			Responsibility.Focus();
			Responsibility.Text = "";
		}
	}

    private void DeleteResponsibility(object sender, EventArgs e)
    {
		responsibilities.Remove(((Button)sender).CommandParameter as string);
    }

    private async void Submit(object sender, EventArgs e)
	{
		if (await jobExperienceApiService.AddToIdAsync(App.LoggedUser.Id.ToString(),
			new Models.JobExperience()
			{
				Position = Position.Text,
				Company = Company.Text,
				Location = Location.Text,
				PeriodOfEmploymentStart = PeriodOfEmploymentStart.Date,
				PeriodOdEmploymentEnd = StillWorking.IsChecked ? DateTime.MaxValue : PeriodOdEmploymentEnd.Date,
				Responsibilities = String.Join(";", responsibilities)
			}))
		{
            await Shell.Current.GoToAsync("//Profile");
        }
		else
		{
			await DisplayAlert("Dodawanie doœwiadczenia zawodowego", "Wyst¹pi³ b³¹d przy zapisie", "OK");
		}
		
	}
}