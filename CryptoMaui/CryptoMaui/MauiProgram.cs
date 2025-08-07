using BackendClient;
using CommunityToolkit.Maui;
using CryptoMaui.ViewModels;
using Microsoft.Extensions.Logging;

namespace CryptoMaui;
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
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.UseBackendClient();


        builder.Services
            .AddTransient<LoginViewModel>();



        return builder.Build();
    }
}
