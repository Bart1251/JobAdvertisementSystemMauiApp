using JobAdvertisementApp.Controls;
using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly CategoryApiService categoryApiService;
    private readonly WorkingShiftApiService workingShiftApiService;
    private readonly TypeOfContractApiService typeOfContractApiService;
    private readonly JobTypeApiService jobTypeApiService;
    private readonly JobLevelApiService jobLevelApiService;
    private readonly OfferApiService offerApiService;

    private List<Category> categories;
    private List<WorkingShift> workingShifts;
    private List<TypeOfContract> typeOfContracts;
    private List<JobType> jobTypes;
    private List<JobLevel> jobLevels;

    public MainPage(CategoryApiService categoryApiService, WorkingShiftApiService workingShiftApiService, TypeOfContractApiService typeOfContractApiService, JobTypeApiService jobTypeApiService, JobLevelApiService jobLevelApiService, OfferApiService offerApiService)
	{
		InitializeComponent();
        this.categoryApiService = categoryApiService;
        this.workingShiftApiService = workingShiftApiService;
        this.typeOfContractApiService = typeOfContractApiService;
        this.jobTypeApiService = jobTypeApiService;
        this.jobLevelApiService = jobLevelApiService;
        this.offerApiService = offerApiService;
        SetDropdowns();
        GetOffers();
    }

    private async void Search(object sender, EventArgs e)
    {
        var offers = await offerApiService.GetFiletredAsync(
            Category.SelectedIndex > 0 ? categories.Single(e => e.Name == Category.SelectedItem as string).Id.ToString() : "",
            JobLevel.SelectedIndex > 0 ? jobLevels.Single(e => e.Level == JobLevel.SelectedItem as string).Id.ToString() : "",
            TypeOfContract.SelectedIndex > 0 ? typeOfContracts.Single(e => e.Type == TypeOfContract.SelectedItem as string).Id.ToString() : "",
            JobType.SelectedIndex > 0 ? jobTypes.Single(e => e.Type == JobType.SelectedItem as string).Id.ToString() : "",
            WorkingShift.SelectedIndex > 0 ? workingShifts.Single(e => e.Shift == WorkingShift.SelectedItem as string).Id.ToString() : "",
            Position.Text);
        Offers.Children.Clear();
        foreach (Models.Offer offer in offers)
        {
            Offers.Children.Add(new OfferTile(offer));
        }
        if (Offers.Children.Count == 0)
            Offers.Children.Add(new Label() { Text = "Brak wyników", FontSize = 30, TextColor = Color.FromHex("#222222"), Margin = new Thickness(0, 40), HorizontalTextAlignment = TextAlignment.Center});
    }

    private void PageSizeChanged(object sender, EventArgs e)
	{
		Banner.MaximumHeightRequest = this.Height - NavBar.Height;
		if(this.Width < 1300)
		{
			Row1.Orientation = StackOrientation.Vertical;
			Row2.Orientation = StackOrientation.Vertical;
			Row1.Padding = 0;
			Row2.Padding = 0;
            foreach (View control in Row1.Children)
            {
                control.HeightRequest = 50;
            }
			foreach(View control in Row1.Children)
			{
				control.WidthRequest = this.Width - Math.Pow(Math.Log10((this.Width - 400) > 0 ? this.Width - 400 : 1), 5) * 2;
			}
            foreach (View control in Row2.Children)
            {
                control.WidthRequest = this.Width - Math.Pow(Math.Log10((this.Width - 400) > 0 ? this.Width - 400 : 1), 5) * 2;
            }
        }
		else
		{
            Row1.Orientation = StackOrientation.Horizontal;
            Row2.Orientation = StackOrientation.Horizontal;
            Row1.Padding = 10;
            Row2.Padding = 10;
            foreach (View control in Row1.Children)
            {
                control.HeightRequest = 60;
            }
            foreach (View control in Row2.Children)
            {
                control.WidthRequest = 240;
            }
            Position.WidthRequest = 350;
            Category.WidthRequest = 300;
        }
        ((IView)Row1).InvalidateMeasure();
        ((IView)Row2).InvalidateMeasure();

        Offers.Margin = new Thickness(Math.Pow(Math.Log10((this.Width - 400) > 0 ? this.Width - 400 : 1), 5), 10);
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavBar.OnAppearing();
        Offers.Margin = new Thickness(Math.Pow(Math.Log10((this.Width - 400) > 0 ? this.Width - 400 : 1), 5), 10);
    }

    private async void SetDropdowns()
    {
        categories = new List<Category>() { new Category() { Id = 0, Name = "Kategoria" } };
        categories.AddRange(await categoryApiService.GetAllAsync());
        Category.ItemsSource = categories;
        if (DeviceInfo.Platform != DevicePlatform.Android) Category.ItemsSource = Category.GetItemsAsArray();
        Category.SelectedIndex = 0;
        workingShifts = new List<WorkingShift>() { new WorkingShift() { Id = 0, Shift = "Wymiar pracy" } };
        workingShifts.AddRange(await workingShiftApiService.GetAllAsync());
        WorkingShift.ItemsSource = workingShifts;
        if (DeviceInfo.Platform != DevicePlatform.Android) WorkingShift.ItemsSource = WorkingShift.GetItemsAsList();
        WorkingShift.SelectedIndex = 0;
        typeOfContracts = new List<TypeOfContract>() { new TypeOfContract() { Id = 0, Type = "Rodzaj umowy" } };
        typeOfContracts.AddRange(await typeOfContractApiService.GetAllAsync());
        TypeOfContract.ItemsSource = typeOfContracts;
        if (DeviceInfo.Platform != DevicePlatform.Android) TypeOfContract.ItemsSource = TypeOfContract.GetItemsAsArray();
        TypeOfContract.SelectedIndex = 0;
        jobTypes = new List<JobType>() { new JobType() { Id = 0, Type = "Tryb pracy" } };
        jobTypes.AddRange(await jobTypeApiService.GetAllAsync());
        JobType.ItemsSource = jobTypes;
        if (DeviceInfo.Platform != DevicePlatform.Android) JobType.ItemsSource = JobType.GetItemsAsArray();
        JobType.SelectedIndex = 0;
        jobLevels = new List<JobLevel>() { new JobLevel() { Id = 0, Level = "Poziom stanowiska" } };
        jobLevels.AddRange(await jobLevelApiService.GetAllAsync());
        JobLevel.ItemsSource = jobLevels;
        if(DeviceInfo.Platform != DevicePlatform.Android) JobLevel.ItemsSource = JobLevel.GetItemsAsArray();
        JobLevel.SelectedIndex = 0;
    }

    private async void GetOffers()
    {
        Offers.Children.Clear();
        foreach(Models.Offer offer in await offerApiService.GetAllAsync())
        {
            Offers.Children.Add(new OfferTile(offer));
        }
    }
}

