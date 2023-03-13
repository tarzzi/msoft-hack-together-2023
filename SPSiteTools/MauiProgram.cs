using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace SPPageTools;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		// Get appSettings.json file and add that to the configuration
		var configuration = new ConfigurationBuilder()
      .AddJsonStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("SPPageTools.appSettings.json"))
      .Build();

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
      .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			;

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
