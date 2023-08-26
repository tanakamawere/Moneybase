using Camera.MAUI;
using CommunityToolkit.Maui;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using Moneybase.Pages.SendMoneyPages;
using Moneybase.Services;
using Mopups.Hosting;
using Mopups.Services;
using Syncfusion.Maui.Core.Hosting;
using The49.Maui.BottomSheet;

namespace Moneybase;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.UseMicrocharts()
			.ConfigureMopups()
			.UseMauiCameraView()
			.UseBottomSheet()
			.ConfigureSyncfusionCore()
            .ConfigureViewModels()
			.ConfigurePages()
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
		builder.Services.AddTransient<IApiRepository, ApiRepository>();		
		builder.Services.AddTransient<SendCurrencyBottomSheet>();
		builder.Services.AddSingleton(MopupService.Instance);

		return builder.Build();
	}
}
