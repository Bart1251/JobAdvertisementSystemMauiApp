﻿using JobAdvertisementApp.Controls;
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
        builder.Services.AddSingleton<PasswordHasher>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<Login>();
		builder.Services.AddSingleton<Register>();
		builder.Services.AddSingleton<Profile>();

        return builder.Build();
	}
}
