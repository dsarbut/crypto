using BackendClient;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoMaui.Views;
using System.Collections.ObjectModel;

namespace CryptoMaui.ViewModels;
public partial class PortfolioViewModel : ObservableObject
{
    private readonly CryptoBackClient cryptoBack;

    public PortfolioViewModel(CryptoBackClient cryptoBack)
    {
        this.cryptoBack = cryptoBack;
        Investments = [];
        SelectedCoin = "All";
        IsCoinSelected = false;
    }

    [ObservableProperty]
    public partial string? SelectedCoin { get; set; }

    [ObservableProperty]
    public partial bool IsCoinSelected { get; set; }


    [ObservableProperty]
    public partial ObservableCollection<InvestementViewModel> Investments { get; set; }

    [ObservableProperty]
    public partial CoinSelectionViewModel? CoinSelection { get; set; }



    [RelayCommand]
    public async Task ShowDetails(InvestementViewModel? investement)
    {
        if (investement != null)
        {
            await Shell.Current.ShowPopupAsync(new InvestmentDetailsView(investement));
        }
    }

    [RelayCommand]
    public async Task AddInvestment()
    {
        AddInvestmentViewModel viewModel = new AddInvestmentViewModel(cryptoBack);
        var res = await Shell.Current.ShowPopupAsync<bool>(new AddInvestmentView(viewModel));
        if (res != null && !res.WasDismissedByTappingOutsideOfPopup && res.Result)
        {
            InvestmentDto dto = new()
            {
                CoinAmmount = viewModel.BuyingPrice,
                InvestmentValue = viewModel.InvestedAmmount,
                CoinName = viewModel.SelectedCoin,
                Date = viewModel.SelectedDate,
                RepeatMonthly = viewModel.RepeatMonthly,
            };

            await cryptoBack.AddInvestmentAsync(dto);

            await RefreshInvestments();

            return;
        }
    }




    public async Task LoadPortfolio()
    {

        IsCoinSelected = false;
        await RefreshInvestments();

        var coinViewModelItems = Investments
            .Select(item => item.CoinName).Distinct()
            .Select(coin => new CoinSelectionViewModelItem()
            {
                Label = coin,
                IsSelected = true,
            });


        CoinSelection = new CoinSelectionViewModel();
        CoinSelection.SetItems(coinViewModelItems);
    }

    private async Task RefreshInvestments()
    {
        Investments.Clear();
        var totalInvestments = await cryptoBack.GetInvestmentsAsync();
        foreach (var investment in totalInvestments.Investments)
        {
            Investments.Add(new InvestementViewModel()
            {
                CoinAmmount = investment.CoinAmmount,
                CoinName = investment.CoinName,
                Date = investment.Date.Date,
                InvestedAmmount = investment.InvestmentValue,
                ValueToday = 0,
                ReturnOfInvestment = 0
            });
        }
    }

}
