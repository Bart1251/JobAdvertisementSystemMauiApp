using JobAdvertisementApp.Models;

namespace JobAdvertisementApp.Controls;

public partial class JobExperienceView : ContentView
{
	public JobExperienceView(JobExperience jobExperience, EventHandler delete)
	{
		InitializeComponent();
        this.BindingContext = jobExperience;
		BindableLayout.SetItemsSource(Responsibilities, jobExperience.Responsibilities.Split(";"));
		Delete.Clicked += delete;
	}
}