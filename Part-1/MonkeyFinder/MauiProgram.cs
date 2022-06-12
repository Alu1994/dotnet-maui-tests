using MonkeyFinder.Services;
using MonkeyFinder.View;

namespace MonkeyFinder;

public static class MauiProgram
{
	public static ObservableCollection<Monkey> MonkeysList { get; private set; }

	public static MauiApp CreateMauiApp()
	{
        MonkeysList = new ObservableCollection<Monkey>();

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DetailsPage>();
        builder.Services.AddTransient<CreateDetailsPage>();

        builder.Services.AddSingleton<MonkeyService>();
        builder.Services.AddSingleton<MonkeysViewModel>();
        builder.Services.AddTransient<MonkeyDetailsViewModel>();
        builder.Services.AddTransient<CreateMonkeyDetailsViewModel>();

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        return builder.Build();
	}
}
