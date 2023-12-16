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
            Location.WidthRequest = 350;
            Category.WidthRequest = 300;
            Distance.WidthRequest = 200;
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
        List<Category> categories = new List<Category>() { new Category() { Name = "Kategoria" } };
        categories.AddRange(await categoryApiService.GetAllAsync());
        Category.ItemsSource = categories;
        if (DeviceInfo.Platform != DevicePlatform.Android) Category.ItemsSource = Category.GetItemsAsArray();
        Category.SelectedIndex = 0;
        List<WorkingShift> workingShifts = new List<WorkingShift>() { new WorkingShift() { Shift = "Wymiar pracy" } };
        workingShifts.AddRange(await workingShiftApiService.GetAllAsync());
        WorkingShift.ItemsSource = workingShifts;
        if (DeviceInfo.Platform != DevicePlatform.Android) WorkingShift.ItemsSource = WorkingShift.GetItemsAsArray();
        WorkingShift.SelectedIndex = 0;
        List<TypeOfContract> typeOfContracts = new List<TypeOfContract>() { new TypeOfContract() { Type = "Rodzaj umowy" } };
        typeOfContracts.AddRange(await typeOfContractApiService.GetAllAsync());
        TypeOfContract.ItemsSource = typeOfContracts;
        if (DeviceInfo.Platform != DevicePlatform.Android) TypeOfContract.ItemsSource = TypeOfContract.GetItemsAsArray();
        TypeOfContract.SelectedIndex = 0;
        List<JobType> jobTypes = new List<JobType>() { new JobType() { Type = "Tryb pracy" } };
        jobTypes.AddRange(await jobTypeApiService.GetAllAsync());
        JobType.ItemsSource = jobTypes;
        if (DeviceInfo.Platform != DevicePlatform.Android) JobType.ItemsSource = JobType.GetItemsAsArray();
        JobType.SelectedIndex = 0;
        List<JobLevel> jobLevels = new List<JobLevel>() { new JobLevel() { Level = "Poziom stanowiska" } };
        jobLevels.AddRange(await jobLevelApiService.GetAllAsync());
        JobLevel.ItemsSource = jobLevels;
        if(DeviceInfo.Platform != DevicePlatform.Android) JobLevel.ItemsSource = JobLevel.GetItemsAsArray();
        JobLevel.SelectedIndex = 0;
        Distance.SelectedIndex = 3;
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

