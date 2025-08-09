using BackendClient;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CryptoMaui.ViewModels;

public partial class AddInvestmentViewModel : ObservableObject
{
    private readonly CryptoBackClient cryptoBack;
    private readonly Dictionary<string, IEnumerable<CoinPrice>> cache = [];

    public AddInvestmentViewModel(CryptoBackClient cryptoBack)
    {
        this.cryptoBack = cryptoBack;
        Coins = [];
        MinDate = new DateTime(1970, 1, 1);
        MaxDate = DateTime.Now;
    }

    [ObservableProperty]
    public partial ObservableCollection<string> Coins { get; set; }

    [ObservableProperty]
    public partial string? SelectedCoin { get; set; }

    [ObservableProperty]
    public partial DateTime MinDate { get; set; }

    [ObservableProperty]
    public partial DateTime MaxDate { get; set; }

    [ObservableProperty]
    public partial DateTime SelectedDate { get; set; }

    [ObservableProperty]
    public partial bool RepeatMonthly { get; set; }

    [ObservableProperty]
    public partial decimal InvestedAmmount { get; set; }

    [ObservableProperty]
    public partial decimal BuyingPrice { get; set; }


    public async Task LoadData()
    {
        var installedCoins = await cryptoBack.InstalledCoinsAsync();
        Coins = new ObservableCollection<string>(installedCoins);
        SelectedCoin = Coins.FirstOrDefault();
    }

    async partial void OnSelectedCoinChanged(string? oldValue, string? newValue)
    {
        if (!string.IsNullOrEmpty(newValue) && oldValue != newValue)
        {
            IEnumerable<CoinPrice> history = await GetPriceHistoryFor(newValue);
            var minDate = history.Min(price => price.Date);
            var maxDate = history.Max(price => price.Date);
            MinDate = minDate.Date;
            MaxDate = maxDate.Date;
            SelectedDate = MinDate;
            await UpdateBuyingPrice();
        }
    }

    async partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue)
    {
        if (oldValue != newValue)
           await UpdateBuyingPrice();
    }

    private async Task UpdateBuyingPrice()
    {
        if (SelectedCoin != null)
        {
            IEnumerable<CoinPrice> history = await GetPriceHistoryFor(SelectedCoin);

            CoinPrice? price = await FindPrice(SelectedDate, history);
            if (price != null)
            {
                BuyingPrice = price.Price;
            }
        }
    }


    private async Task<IEnumerable<CoinPrice>> GetPriceHistoryFor(string coin)
    {
        if (!cache.TryGetValue(coin, out IEnumerable<CoinPrice>? history))
        {
            history = await cryptoBack.CoinPriceAsync(coin);
            cache[coin] = history;
        }

        return history;
    }

    private async static Task<CoinPrice?> FindPrice(DateTime date, IEnumerable<CoinPrice> history)
    {
        CoinPrice? selectedPrice = history.
            OrderByDescending(price => price.Date.UtcDateTime)
            .FirstOrDefault(price => price.Date.UtcDateTime <= date);

        if (selectedPrice == null)
        {
            await Shell.Current.DisplayAlert("Error", "Could not find a price for the selected date", "Ok");
            return null;
        }

        return selectedPrice;
    }
}
