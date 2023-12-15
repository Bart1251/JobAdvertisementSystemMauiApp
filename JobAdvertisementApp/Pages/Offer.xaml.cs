namespace JobAdvertisementApp.Pages;

public partial class Offer : ContentPage
{
	public Offer()
	{
		InitializeComponent();
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavBar.OnAppearing();
    }

	private void PageSizeChanged(object sender, EventArgs e)
	{
		MainContainer.Margin = new Thickness(Math.Pow(Math.Log10((this.Width - 400) > 0 ? this.Width - 400 : 1), 5), 10);
		if (this.Width < 600)
		{
			Grid.SetColumn(WegeFrame, 0);
			Grid.SetRow(WegeFrame, 1);
			Grid.SetColumnSpan(WegeFrame, 3);

			Grid.SetColumn(Right, 0);
			Grid.SetRow(Right, 1);
			Grid.SetColumnSpan(Right, 2);
			Grid.SetColumnSpan(Left, 2);
		}
		else
		{
			Grid.SetColumn(WegeFrame, 2);
			Grid.SetRow(WegeFrame, 0);
			Grid.SetColumnSpan(WegeFrame, 1);

            Grid.SetColumn(Right, 1);
            Grid.SetRow(Right, 0);
            Grid.SetColumnSpan(Right, 1);
            Grid.SetColumnSpan(Left, 1);
        }
	}
}