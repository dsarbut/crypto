using BackendClient;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CryptoMaui.ViewModels;

public partial class AddInvestmentViewModel : ObservableObject
{
    private readonly CryptoBackClient cryptoBack;
    private readonly Dictionary<string, IEnumerable<CoinPriceDto>> cache = [];

    public AddInvestmentViewModel(CryptoBackClient cryptoBack)
    {
        this.cryptoBack = cryptoBack;
        MinDate = new DateTime(2024, 1, 1).Date;
        MaxDate = DateTime.Now.Date;
        CoinSelection = new CoinSelectionViewModel();
    }


    [ObservableProperty]
    public partial DateTime MinDate { get; set; }

    [ObservableProperty]
    public partial DateTime MaxDate { get; set; }

    [ObservableProperty]
    public partial DateTime SelectedDate { get; set; }


    [ObservableProperty]
    public partial CoinSelectionViewModel CoinSelection { get; set; }



    [ObservableProperty]
    public partial decimal InvestedAmmount { get; set; }

    [ObservableProperty]
    public partial decimal BuyingPrice { get; set; }


    public async Task LoadData()
    {
        var installedCoins = await cryptoBack.GetInstalledCoinsAsync();

        List<CoinSelectionViewModelItem> coinSelectionViewModelsItems = [];
        foreach (var coin in installedCoins.Distinct())
        {
            var history = await GetPriceHistoryFor(coin);

            var coinPrice = await FindPrice(SelectedDate, history);
            if (coinPrice != null)
            {

                var viewModel = new CoinSelectionViewModelItem()
                {
                    IsSelected = false,
                    Label = coin,
                    Price = coinPrice.Price,
                    Date = coinPrice.Date.UtcDateTime
                };
                coinSelectionViewModelsItems.Add(viewModel);
            }

        }

        CoinSelection.SetItems(coinSelectionViewModelsItems);
    }


    private async Task<IEnumerable<CoinPriceDto>> GetPriceHistoryFor(string coin)
    {
        if (!cache.TryGetValue(coin, out IEnumerable<CoinPriceDto>? history))
        {
            history = await cryptoBack.GetPriceHistoryAsync(coin);
            cache[coin] = history;
        }

        return history;
    }

    private async static Task<CoinPriceDto?> FindPrice(DateTime date, IEnumerable<CoinPriceDto> history)
    {
        CoinPriceDto? selectedPrice = history.
            OrderByDescending(price => price.Date)
            .LastOrDefault(price => price.Date >= date);

        if (selectedPrice == null)
        {
            await Shell.Current.DisplayAlert("Error", "Could not find a price for the selected date", "Ok");
            return null;
        }

        return selectedPrice;
    }
}
