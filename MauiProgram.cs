using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Moneybase.Pages;
using Moneybase.Services;
using Moneybase.ViewModels;

namespace Moneybase;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("faregular.otf", "faregular");
                fonts.AddFont("fasolid.otf", "fasolid");
                fonts.AddFont("fabrands.otf", "fabrands");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
		//Pages
		builder.Services.AddSingleton<HomePage>();

		//View Models
		builder.Services.AddSingleton<HomePageViewModel>();

		//Services
		builder.Services.AddTransient<IApiRepository, ApiRepository>();

		return builder.Build();
	}
}
