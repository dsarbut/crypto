using CryptoMaui.ViewModels;

namespace CryptoMaui.Views;

public partial class PortfolioPage : ContentPage
{
    public PortfolioPage(PortfolioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PortfolioViewModel viewModel)
        {
            await viewModel.LoadPortfolio();
        }
    }
}
