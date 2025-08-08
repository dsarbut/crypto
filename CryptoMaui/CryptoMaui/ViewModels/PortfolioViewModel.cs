using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoMaui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMaui.ViewModels;
public partial class PortfolioViewModel : ObservableObject
{

    public PortfolioViewModel()
    {
        Investments = [];

        DateOnly start = new DateOnly(2024, 01, 01);
        for (int i = 0; i < 12; i++)
        {
            Investments.Add(new InvestementViewModel()
            {
                Date = start.AddMonths(i),
                CoinAmmount = 0.0045M,
                CoinName = "BTC",
                InvestedAmmount = 400,
                ValueToday = 420,
                ReturnOfInvestment = 20
            });
        }
    }

    [ObservableProperty]
    public partial ObservableCollection<InvestementViewModel> Investments { get; set; }


    [RelayCommand]
    public async Task ShowDetails(InvestementViewModel? investement)
    {
        if (investement != null)
        {
            await Shell.Current.ShowPopupAsync(new InvestmentDetailsView(investement));
        }
    }
}
