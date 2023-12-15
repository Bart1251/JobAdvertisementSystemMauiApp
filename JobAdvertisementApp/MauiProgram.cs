using JobAdvertisementApp.Controls;
using JobAdvertisementApp.Pages;
using JobAdvertisementApp.Services;

namespace JobAdvertisementApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<UserApiService>();
		builder.Services.AddSingleton<ProfileApiService>();
		builder.Services.AddSingleton<LanguageApiService>();
		builder.Services.AddSingleton<JobExperienceApiService>();
		builder.Services.AddSingleton<EducationApiService>();
		builder.Services.AddSingleton<CourseApiService>();
		builder.Services.AddSingleton<CategoryApiService>();
		builder.Services.AddSingleton<JobTypeApiService>();
		builder.Services.AddSingleton<WorkingShiftApiService>();
		builder.Services.AddSingleton<TypeOfContractApiService>();
		builder.Services.AddSingleton<JobLevelApiService>();
        builder.Services.AddSingleton<PasswordHasher>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<Login>();
		builder.Services.AddSingleton<Register>();
		builder.Services.AddTransient<Profile>();
		builder.Services.AddTransient<AddJobExperience>();
		builder.Services.AddTransient<AddEducation>();
		builder.Services.AddTransient<AddCourse>();
		builder.Services.AddTransient<AddLanguage>();
		builder.Services.AddTransient<Offer>();

        return builder.Build();
	}
}
