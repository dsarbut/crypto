using BackendClient;
using CommunityToolkit.Maui;
using CryptoMaui.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;

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

        var handler = new HttpClientHandler();

#if DEBUG
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert != null && cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
#endif


        builder.Services
            .AddSingleton(handler)
            .AddSingleton<IBackendAddressResolver, BackendAddressResolver>()
            .UseBackendClient();


        builder.Services
            .AddTransient<LoginViewModel>()
            .AddTransient<PortfolioViewModel>()
            .AddTransient<AddInvestmentViewModel>();


        return builder.Build();
    }
}
