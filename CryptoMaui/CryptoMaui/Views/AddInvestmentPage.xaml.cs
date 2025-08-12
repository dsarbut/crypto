using CryptoMaui.ViewModels;

namespace CryptoMaui.Views;

public partial class AddInvestmentPage : ContentPage
{
	public AddInvestmentPage(AddInvestmentViewModel addInvestmentViewModel) : base()
	{
		InitializeComponent();
		BindingContext = addInvestmentViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AddInvestmentViewModel viewModel)
        {
            await viewModel.LoadData();
        }
    }
}