namespace JobAdvertisementApp.Pages;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private void PageSizeChanged(object sender, EventArgs e)
	{
		Banner.MaximumHeightRequest = this.Height - NavBar.Height;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavBar.OnAppearing();
	}
}

