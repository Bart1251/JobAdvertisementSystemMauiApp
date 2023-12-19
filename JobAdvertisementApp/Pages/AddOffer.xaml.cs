using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;
using System.Collections.ObjectModel;

namespace JobAdvertisementApp.Pages;

[QueryProperty(nameof(companyId), "companyId")]
public partial class AddOffer : ContentPage, IQueryAttributable
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

    private ObservableCollection<string> responsibilities = new ObservableCollection<string>();
    private ObservableCollection<string> benefits = new ObservableCollection<string>();
    private ObservableCollection<string> requirements = new ObservableCollection<string>();

    private string companyId = "";

    public AddOffer(CategoryApiService categoryApiService, WorkingShiftApiService workingShiftApiService, TypeOfContractApiService typeOfContractApiService, JobTypeApiService jobTypeApiService, JobLevelApiService jobLevelApiService, OfferApiService offerApiService)
	{
		InitializeComponent();
        this.categoryApiService = categoryApiService;
        this.workingShiftApiService = workingShiftApiService;
        this.typeOfContractApiService = typeOfContractApiService;
        this.jobTypeApiService = jobTypeApiService;
        this.jobLevelApiService = jobLevelApiService;
        this.offerApiService = offerApiService;
        SetDropdowns();
        BindableLayout.SetItemsSource(Responsibilities, responsibilities);
        BindableLayout.SetItemsSource(Benefits, benefits);
        BindableLayout.SetItemsSource(Requirements, requirements);
    }

    private async void Submit(object sender, EventArgs e)
    {
        if(DeviceInfo.Platform == DevicePlatform.Android)
            await offerApiService.AddAsync(new Models.Offer()
            {
                Position = Position.Text,
                MinimumWege = decimal.Parse(MinimumWege.Text),
                MaximumWege = decimal.Parse(MaximumWege.Text),
                Expires = Expires.Date,
                Description = Description.Text,
                Benefits = String.Join(";", benefits),
                Requirements = String.Join(";", requirements),
                Responsibilities = String.Join(";", responsibilities)},
            companyId,
            ((JobLevel)JobLevel.SelectedItem).Id.ToString(),
            ((TypeOfContract)TypeOfContract.SelectedItem).Id.ToString(),
            ((JobType)JobType.SelectedItem).Id.ToString(),
            ((WorkingShift)WorkingShift.SelectedItem).Id.ToString(),
            ((Category)Category.SelectedItem).Id.ToString());
        else
            await offerApiService.AddAsync(new Models.Offer()
            {
                Position = Position.Text,
                MinimumWege = decimal.Parse(MinimumWege.Text),
                MaximumWege = decimal.Parse(MaximumWege.Text),
                Expires = Expires.Date,
                Description = Description.Text,
                Benefits = String.Join(";", benefits),
                Requirements = String.Join(";", requirements),
                Responsibilities = String.Join(";", responsibilities)
            },
            companyId,
            jobLevels.Single(e => e.Level == JobLevel.SelectedItem as string).Id.ToString(),
            typeOfContracts.Single(e => e.Type == TypeOfContract.SelectedItem).Id.ToString(),
            jobTypes.Single(e => e.Type == JobType.SelectedItem).Id.ToString(),
            workingShifts.Single(e => e.Shift == WorkingShift.SelectedItem).Id.ToString(),
            categories.Single(e => e.Name == Category.SelectedItem).Id.ToString());
        await Shell.Current.GoToAsync("//AdminPanel");
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

    private void AddBenefit(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Benefit.Text) && !benefits.Contains(Benefit.Text) && !Benefit.Text.Contains(";"))
        {
            benefits.Add(Benefit.Text);
            ((IView)MainGrid).InvalidateMeasure();
            Benefit.Focus();
            Benefit.Text = "";
        }
    }

    private void DeleteBenefit(object sender, EventArgs e)
    {
        benefits.Remove(((Button)sender).CommandParameter as string);
    }

    private void AddRequirement(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Requirement.Text) && !requirements.Contains(Requirement.Text) && !Requirement.Text.Contains(";"))
        {
            requirements.Add(Requirement.Text);
            ((IView)MainGrid).InvalidateMeasure();
            Requirement.Focus();
            Requirement.Text = "";
        }
    }

    private void DeleteRequirement(object sender, EventArgs e)
    {
        requirements.Remove(((Button)sender).CommandParameter as string);
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AdminPanel");
    }

    private async void SetDropdowns()
    {
        categories = (List<Category>)await categoryApiService.GetAllAsync();
        Category.ItemsSource = categories;
        if (DeviceInfo.Platform != DevicePlatform.Android) Category.ItemsSource = Category.GetItemsAsArray();
        workingShifts = (List<WorkingShift>)await workingShiftApiService.GetAllAsync();
        WorkingShift.ItemsSource = workingShifts;
        if (DeviceInfo.Platform != DevicePlatform.Android) WorkingShift.ItemsSource = WorkingShift.GetItemsAsArray();
        typeOfContracts = (List<TypeOfContract>)await typeOfContractApiService.GetAllAsync();
        TypeOfContract.ItemsSource = typeOfContracts;
        if (DeviceInfo.Platform != DevicePlatform.Android) TypeOfContract.ItemsSource = TypeOfContract.GetItemsAsArray();
        jobTypes = (List<JobType>)await jobTypeApiService.GetAllAsync();
        JobType.ItemsSource = jobTypes;
        if (DeviceInfo.Platform != DevicePlatform.Android) JobType.ItemsSource = JobType.GetItemsAsArray();
        jobLevels = (List<JobLevel>)await jobLevelApiService.GetAllAsync();
        JobLevel.ItemsSource = jobLevels;
        if (DeviceInfo.Platform != DevicePlatform.Android) JobLevel.ItemsSource = JobLevel.GetItemsAsArray();
        Category.SelectedIndex = 0;
        WorkingShift.SelectedIndex = 0;
        TypeOfContract.SelectedIndex = 0;
        JobType.SelectedIndex = 0;
        JobLevel.SelectedIndex = 0;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        companyId = query["companyId"] as string;
    }
}