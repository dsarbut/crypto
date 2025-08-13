using CryptoMaui.Views;

namespace CryptoMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddInvestmentPage), typeof(AddInvestmentPage));
    }
}
