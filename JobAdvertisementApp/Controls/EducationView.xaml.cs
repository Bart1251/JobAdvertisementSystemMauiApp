using JobAdvertisementApp.Models;

namespace JobAdvertisementApp.Controls;

public partial class EducationView : ContentView
{
	public EducationView(Education education, EventHandler delete)
	{
		InitializeComponent();
		this.BindingContext = education;
		Delete.Clicked += delete;
	}
}