using CryptoMaui.ViewModels;

namespace CryptoMaui.Views;

public partial class PortfolioPage : ContentPage
{
    public PortfolioPage(PortfolioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
