using JobAdvertisementApp.Controls;
using JobAdvertisementApp.Models;
using JobAdvertisementApp.Services;
using System.Collections.ObjectModel;

namespace JobAdvertisementApp.Pages;

public partial class Profile : ContentPage
{
	private readonly UserApiService userApiService;
	private readonly ProfileApiService profileApiService;
    private readonly LanguageApiService languageApiService;
    private readonly JobExperienceApiService jobExperienceApiService;
    private readonly EducationApiService educationApiService;
    private readonly CourseApiService courseApiService;

    private ObservableCollection<Models.Profile> profiles = new ObservableCollection<Models.Profile>();
	private ObservableCollection<Language> languages = new ObservableCollection<Language>();
	private ObservableCollection<string> skills = new ObservableCollection<string>();

    public Profile(UserApiService userApiService, ProfileApiService profileApiService, LanguageApiService languageApiService, JobExperienceApiService jobExperienceApiService, EducationApiService educationApiService, CourseApiService courseApiService)
	{
		InitializeComponent();
		this.userApiService = userApiService;
		this.profileApiService = profileApiService;
        this.languageApiService = languageApiService;
        this.jobExperienceApiService = jobExperienceApiService;
        this.educationApiService = educationApiService;
        this.courseApiService = courseApiService;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        NavBar.OnAppearing();
        if (this.Width < 800)
        {
            Grid.SetColumn(Right, 0);
            Grid.SetRow(Right, 1);
            SecondColumn.Width = new GridLength(0, GridUnitType.Star);
            Left.MinimumHeightRequest = -1;
        }
        else
        {
            Grid.SetColumn(Right, 1);
            Grid.SetRow(Right, 0);
            SecondColumn.Width = new GridLength(2, GridUnitType.Star);
            Left.MinimumHeightRequest = ScrollView.Height;
        }
        GetData();
    }

    private void PageSizeChanged(object sender, EventArgs e)
    {
        if(this.Width < 800)
        {
            Grid.SetColumn(Right, 0);
            Grid.SetRow(Right, 1);
            SecondColumn.Width = new GridLength(0, GridUnitType.Star);
            Left.MinimumHeightRequest = -1;
        }
        else
        {
            Grid.SetColumn(Right, 1);
            Grid.SetRow(Right, 0);
            SecondColumn.Width = new GridLength(2, GridUnitType.Star);
            Left.MinimumHeightRequest = ScrollView.Height;
        }
        UpdateLayout();
    }

	private async void GetData()
	{
        byte[] imageBytes = await userApiService.GetImage(App.LoggedUser.Id.ToString());
        if(imageBytes != null)
            ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        await GetProfiles();
        BindableLayout.SetItemsSource(Links, profiles);
        await GetLanguages();
        BindableLayout.SetItemsSource(Languages, languages);
        PersonalInfo1.BindingContext = App.LoggedUser;
        PersonalInfo2.BindingContext = App.LoggedUser;
        if(App.LoggedUser.Skills != null && App.LoggedUser.Skills != string.Empty)
        {
            skills.Clear();
            string[] skillsResponse = App.LoggedUser.Skills.Split(";");
            foreach (string skill in skillsResponse)
                skills.Add(skill);
        }
        BindableLayout.SetItemsSource(Skills, skills);
        await GetJobExperiences();
        await GetEducations();
        await GetCourses();
        UpdateLayout();
    }

    private async Task GetJobExperiences()
    {
        IEnumerable<JobExperience> jobExperiences = await jobExperienceApiService.GetAllFromIdAsync(App.LoggedUser.Id.ToString());
        JobExperiences.Children.Clear();
        foreach(JobExperience jobExperience in jobExperiences)
            JobExperiences.Children.Add(new JobExperienceView(jobExperience, DeleteJobExperience));
    }

    private async Task GetEducations()
    {
        IEnumerable<Education> educations = await educationApiService.GetAllFromIdAsync(App.LoggedUser.Id.ToString());
        Educations.Children.Clear();
        foreach (Education education in educations)
            Educations.Children.Add(new EducationView(education, DeleteEducation));
    }

    private async Task GetCourses()
    {
        IEnumerable<Course> courses = await courseApiService.GetAllFromIdAsync(App.LoggedUser.Id.ToString());
        Courses.Children.Clear();
        foreach (Course course in courses)
            Courses.Children.Add(new CourseView(course, DeleteCourse));
    }

    private async Task GetProfiles()
    {
        profiles.Clear();
        IEnumerable<Models.Profile> profilesResponse = await profileApiService.GetAllFromIdAsync(App.LoggedUser.Id.ToString());
        foreach (var profile in profilesResponse)
            profiles.Add(profile);
    }

    private async Task GetLanguages()
    {
        languages.Clear();
        IEnumerable<Language> languagesResponse = await languageApiService.GetAllFromIdAsync(App.LoggedUser.Id.ToString());
        foreach (var language in languagesResponse)
            languages.Add(language);
    }

	private async void GoBack(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//MainPage");
	}

	private async Task<bool> Save()
	{
        App.LoggedUser.Skills = String.Join(";", skills);
		return await userApiService.UpdateAsync(App.LoggedUser.Id.ToString(), App.LoggedUser);
	}

    private void UpdateLayout()
    {
        ((IView)ScrollView).InvalidateMeasure();
        ((IView)MainGrid).InvalidateMeasure();
    }

    private async void UpdateImage(object sender, EventArgs e)
    {
        var file = await MediaPicker.PickPhotoAsync();
        if (file != null)
        {
            byte[] imageBytes = null;
            using (Stream stream = await file.OpenReadAsync())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    imageBytes = ms.ToArray();
                }
            }

            if(await userApiService.UploadImage(App.LoggedUser.Id.ToString(), imageBytes))
                ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            else
                await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
        }
    }


    private async void UpdateDateOfBirth(object sender, EventArgs e)
	{
        try
		{
			string result = await DisplayPromptAsync("Zmaina wartoœci", "WprowadŸ datê urodzenia", initialValue: App.LoggedUser.DateOfBirth.ToShortDateString());
			if (string.IsNullOrEmpty(result))
				return;
            DateTime date = DateTime.Parse(result);
			if (date.Year > DateTime.Now.Year)
				throw new ArgumentException();
            DateTime previous = App.LoggedUser.DateOfBirth;
            App.LoggedUser.DateOfBirth = date;
            if (!await Save())
            {
                App.LoggedUser.DateOfBirth = previous;
                await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
                return;
            }
        ((Label)sender).Text = result;
        }
		catch(Exception)
		{
			await DisplayAlert("Wyst¹pi³ problem", "Nieprawid³owe dane", "OK");
		}
	}

    private async void UpdateAdress(object sender, EventArgs e)
    {
		string result = await DisplayPromptAsync("Zmiana wartoœci", "WprowadŸ adres", initialValue: App.LoggedUser.Adress);
		if (string.IsNullOrEmpty(result))
			return;
        string previous = App.LoggedUser.Adress;
        App.LoggedUser.Adress = result;
        if (!await Save())
        {
            App.LoggedUser.Adress = previous;
            await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
            return;
        }
        ((Label)sender).Text = result;
    }

    private async void UpdatePhoneNumber(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana wartoœci", "WprowadŸ numer telefonu", initialValue: App.LoggedUser.PhoneNumber);
        if (string.IsNullOrEmpty(result))
            return;
        string previous = App.LoggedUser.PhoneNumber;
        App.LoggedUser.PhoneNumber = result;
        if (!await Save())
        {
            App.LoggedUser.PhoneNumber = previous;
            await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
            return;
        }
        ((Label)sender).Text = result;
    }

    private async void UpdateEmail(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana wartoœci", "WprowadŸ email", initialValue: App.LoggedUser.Email);
        if (string.IsNullOrEmpty(result))
            return;
        string previous = App.LoggedUser.Email;
        App.LoggedUser.Email = result;
        if (!await Save())
        {
            App.LoggedUser.Email = previous;
            await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
            return;
        }
        ((Label)sender).Text = result;
    }

    private async void UpdateName(object sender, EventArgs e)
    {
		string action = await DisplayActionSheet("Któr¹ wartoœæ chcesz zmieniæ", "Anuluj", null, "Imiê", "Nazwisko");
		if (action == null)
			return;

		string result = "";
		if(action == "Imiê")
		{
			result = await DisplayPromptAsync("Zmiana wartoœci", "WprowadŸ imiê", initialValue: App.LoggedUser.FirstName);
            if (string.IsNullOrEmpty(result))
                return;
            string previous = App.LoggedUser.FirstName;
            App.LoggedUser.FirstName = result;
            if (!await Save())
            {
                await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
                App.LoggedUser.FirstName = previous;
                return;
            }
        }
		else
		{
            result = await DisplayPromptAsync("Zmiana wartoœci", "WprowadŸ nazwisko", initialValue: App.LoggedUser.SecondName);
            if (string.IsNullOrEmpty(result))
                return;
			string previous = App.LoggedUser.SecondName;
			App.LoggedUser.SecondName = result;
            if (!await Save())
			{
                await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
				App.LoggedUser.SecondName = previous;
				return;
            }
        }

        ((Label)sender).Text = App.LoggedUser.FirstName + " " + App.LoggedUser.SecondName;
    }

    private async void UpdateOccupation(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Zmiana wartoœci", "WprowadŸ zawód", initialValue: App.LoggedUser.Occupation);
		if (string.IsNullOrEmpty(result))
		{
			if (!await DisplayAlert("Zmiana wartoœci", "Czy chcesz usun¹æ zawód", "Tak", "Nie"))
				return;
		}
		string previous = App.LoggedUser.Occupation;
		App.LoggedUser.Occupation = result;
        if (!await Save())
		{
			App.LoggedUser.Occupation = previous;
            await DisplayAlert("Zmiana wartoœci", "Wyst¹pi³ b³¹d przy zapisie", "OK");
			return;
		}
		((Label)sender).Text = result;
    }

    private async void AddLink(object sender, EventArgs e)
	{
		string result = await DisplayPromptAsync("Dodawanie odnoœnika do profilu", "WprowadŸ odnoœnik");
		if (string.IsNullOrEmpty(result))
			return;
		Models.Profile newProfile = new Models.Profile() { Link = result };
		if(await profileApiService.AddToIdAsync(App.LoggedUser.Id.ToString(), newProfile))
        {
            await GetProfiles();
            UpdateLayout();
        }
		else
			await DisplayAlert("Dodawanie odnoœnika do profilu", "Wyst¹pi³ b³¹d przy dodawaniu", "OK");
	}

	private async void DeleteLink(object sender, EventArgs e)
	{
		if (await DisplayAlert("Usuwanie odnoœnika", "Czy chcesz usun¹æ odnoœnik?", "Tak", "Nie"))
		{
			Models.Profile profileToDelete = ((TapGestureRecognizer)((Label)sender).GestureRecognizers[0]).CommandParameter as Models.Profile;
			if(await profileApiService.DeleteAsync(profileToDelete.Id.ToString()))
            {
				profiles.Remove(profileToDelete);
                UpdateLayout();
            }
			else
                await DisplayAlert("Usuwanie odnoœnika do profilu", "Wyst¹pi³ b³¹d przy usuwaniu", "OK");
        }
	}

    private async void AddLanguage(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("//AddLanguage");
    }

    private async void DeleteLanguage(object sender, EventArgs e)
    {
        if (await DisplayAlert("Usuwanie jêzyka", "Czy chcesz usun¹æ jêzyk?", "Tak", "Nie"))
        {
            Language languageToDelete = ((TapGestureRecognizer)((Label)sender).GestureRecognizers[0]).CommandParameter as Language;
            if (await languageApiService.UserDeleteLanguageAsync(App.LoggedUser.Id.ToString(), languageToDelete.Id.ToString()))
            {
                languages.Remove(languageToDelete);
                UpdateLayout();
            }
            else
                await DisplayAlert("Usuwanie jêzyka", "Wyst¹pi³ b³¹d przy usuwaniu", "OK");
        }
    }

    private async void AddSkill(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Dodawanie umiejêtnoœci", "WprowadŸ umiejêtnoœæ");
        if (string.IsNullOrEmpty(result))
            return;
        skills.Add(result);
        if (!await Save())
        {
            await DisplayAlert("Dodawanie odnoœnika do profilu", "Wyst¹pi³ b³¹d przy dodawaniu", "OK");
            skills.Remove(result);
        }
        UpdateLayout();
    }

    private async void DeleteSkill(object sender, EventArgs e)
    {
        if (await DisplayAlert("Usuwanie umiejêtnoœci", "Czy chcesz usun¹æ umiejêtnoœæ?", "Tak", "Nie"))
        {
            string skillToDelete = ((TapGestureRecognizer)((Label)sender).GestureRecognizers[0]).CommandParameter as string;
            skills.Remove(skillToDelete);
            if (!await Save())
            {
                await DisplayAlert("Usuwanie odnoœnika do profilu", "Wyst¹pi³ b³¹d przy usuwaniu", "OK");
                skills.Add(skillToDelete);
            }
        }
        UpdateLayout();
    }

    private async void AddJobExperience(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AddJobExperience");
    }

    private async void AddEducation(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AddEducation");
    }

    private async void AddCourse(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AddCourse");
    }

    private async void AddOffer(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AddOffer");
    }

    private async void AddCompany(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AddCompany");
    }

    private async void DeleteJobExperience(object sender, EventArgs e)
    {
        if (await jobExperienceApiService.DeleteAsync(((JobExperience)((Button)sender).BindingContext).Id.ToString()))
        {
            await GetJobExperiences();
            UpdateLayout();
        }
        else
        {
            await DisplayAlert("Usuwanie doœwiadczenia zawodowego", "Wyst¹pi³ b³¹d przy usuwaniu", "OK");
        }
    }

    private async void DeleteEducation(object sender, EventArgs e)
    {
        if (await educationApiService.DeleteAsync(((Education)((Button)sender).BindingContext).Id.ToString()))
        {
            await GetEducations();
            UpdateLayout();
        }
        else
        {
            await DisplayAlert("Usuwanie wykszta³cenia", "Wyst¹pi³ b³¹d przy usuwaniu", "OK");
        }
    }

    private async void DeleteCourse(object sender, EventArgs e)
    {
        if (await courseApiService.DeleteAsync(((Course)((Button)sender).BindingContext).Id.ToString()))
        {
            await GetCourses();
            UpdateLayout();
        }
        else
        {
            await DisplayAlert("Usuwanie kursu/szkolenia", "Wyst¹pi³ b³¹d przy usuwaniu", "OK");
        }
    }

}