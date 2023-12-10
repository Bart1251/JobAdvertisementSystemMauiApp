using JobAdvertisementApp.Models;

namespace JobAdvertisementApp.Controls;

public partial class CourseView : ContentView
{
	public CourseView(Course course, EventHandler delete)
	{
		InitializeComponent();
		this.BindingContext = course;
		Delete.Clicked += delete;
	}
}