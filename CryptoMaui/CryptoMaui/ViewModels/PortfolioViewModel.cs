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
    }


    [ObservableProperty]
    public partial ObservableCollection<InvestmentViewModel> Investments { get; set; }

    [RelayCommand]
    public async Task AddInvestment()
    {
        await Shell.Current.GoToAsync("//AddInvestment");
        //if (res != null && !res.WasDismissedByTappingOutsideOfPopup && res.Result)
        //{
        //    InvestmentDto dto = new()
        //    {
        //        CoinAmmount = viewModel.BuyingPrice,
        //        InvestmentValue = viewModel.InvestedAmmount,
        //        CoinName = viewModel.SelectedCoin,
        //        Date = viewModel.SelectedDate,
        //        RepeatMonthly = viewModel.RepeatMonthly,
        //    };

        //    await cryptoBack.AddInvestmentAsync(dto);

        //    await RefreshInvestments();

        //    return;
        //}
    }

    public async Task LoadPortfolio()
    {

        await RefreshInvestments();
    }

    private async Task RefreshInvestments()
    {
        Investments.Clear();
        var totalInvestments = await cryptoBack.GetInvestmentsAsync();

       

        var invGroups = totalInvestments.Investments
            .GroupBy(inv => new { inv.Date.Month, inv.Date.Year })
            .ToDictionary(group => group.Key, group=>group.ToList());


        foreach (var invGroup in invGroups)
        {
            InvestmentViewModel viewModel = new()
            {
                Month = invGroup.Key.Month,
                Year = invGroup.Key.Year,

            };

            foreach (var inv in invGroup.Value)
            {
                viewModel.InvestedAmmount += inv.InvestmentValue;

                CoinDataViewModel coinVm = new()
                {
                    CoinName = inv.CoinName,
                    CoinAmmount = inv.CoinAmmount,
                    Value = inv.InvestmentValue
                };
                viewModel.CoinDatas.Add(coinVm);
            }

            Investments.Add(viewModel);
        }
    }

}
