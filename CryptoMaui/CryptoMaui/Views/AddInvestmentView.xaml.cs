using CommunityToolkit.Maui.Views;
using CryptoMaui.ViewModels;

namespace CryptoMaui.Views;

public partial class AddInvestmentView : Popup<bool>
{
	public AddInvestmentView(AddInvestmentViewModel addInvestmentViewModel) : base()
	{
		InitializeComponent();
		BindingContext = addInvestmentViewModel;
        this.Opened += AddInvestmentView_Opened;
	}

    private async void AddInvestmentView_Opened(object? sender, EventArgs e)
    {
        if (BindingContext is AddInvestmentViewModel vm) 
        {
            await vm.LoadData();
        }
    }

    private async void Ok_Clicked(object sender, EventArgs e)
    {
		await CloseAsync(true);
    }



}