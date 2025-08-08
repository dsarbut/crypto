using CryptoMaui.ViewModels;

namespace CryptoMaui.Views;

public partial class InvestmentDetailsView : ContentView
{
    public InvestmentDetailsView(InvestementViewModel investementViewModel)
    {
        InitializeComponent();
        BindingContext = investementViewModel;
    }
}