using BackendClient;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


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

    [RelayCommand]
    public async Task Invest()
    {

        CoinSelectionViewModelItem[] selectedCoins = [.. CoinSelection.Items.Where(item => item.IsSelected)];

        List<Task> investTasks = [];
        foreach (var coin in selectedCoins)
        {
            var investmentValue = InvestedAmmount / selectedCoins.Length;

            InvestmentDto dto = new()
            {
                CoinPrice = coin.Price,
                InvestmentValue = investmentValue,
                CoinName = coin.Label,
                Date = coin.Date,
            };

            List<InvestmentDto> monthlyInvestDtos = await GetMonthlyInvestDtos(dto);
            foreach (var  invDto in monthlyInvestDtos)
            {
                investTasks.Add(cryptoBack.AddInvestmentAsync(invDto));
            }
        }

        await Task.WhenAll(investTasks);
    }

    public async Task LoadData()
    {
        var installedCoins = await cryptoBack.GetInstalledCoinsAsync();

        List<CoinSelectionViewModelItem> coinSelectionViewModelsItems = [];
        foreach (var coin in installedCoins.Distinct())
        {
            var history = await GetPriceHistoryFor(coin);

            var coinPrice = FindPrice(SelectedDate, history);
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


    async partial void OnSelectedDateChanging(DateTime oldValue, DateTime newValue)
    {
        foreach (var coinVm in CoinSelection.Items)
        {
            var history = await GetPriceHistoryFor(coinVm.Label);
            var coinPrice = FindPrice(newValue, history);
            if (coinPrice != null)
            {
                coinVm.Price = coinPrice.Price;
                coinVm.Date = coinPrice.Date.UtcDateTime;
            }
        }
    }


    private async Task<List<InvestmentDto>> GetMonthlyInvestDtos (InvestmentDto initialInvestment)
    {
        List<InvestmentDto> rv = [initialInvestment];

        var history = await GetPriceHistoryFor(initialInvestment.CoinName);

        var lastPriceData = history
            .OrderByDescending (history => history.Date)
            .FirstOrDefault();

        if (lastPriceData == null)
        {
            return rv;
        }

        DateTime crtDate = initialInvestment.Date.Date;

        do
        {
            var newDate = crtDate.AddMonths(1);
            var buyPriceData = FindPrice(newDate, history);

            if (buyPriceData == null)
                break;

            InvestmentDto dto = new()
            {
                CoinName = initialInvestment.CoinName,
                CoinPrice = buyPriceData.Price,
                Date = buyPriceData.Date,
                InvestmentValue = initialInvestment.InvestmentValue
            };
            rv.Add(dto);

            crtDate = newDate;

        } while (crtDate <= lastPriceData.Date.Date);
        return rv;
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

    private static CoinPriceDto? FindPrice(DateTime date, IEnumerable<CoinPriceDto> history)
    {
        CoinPriceDto? selectedPrice = history.
            OrderByDescending(price => price.Date)
            .LastOrDefault(price => price.Date >= date);

        if (selectedPrice == null)
        {
            return null;
        }

        return selectedPrice;
    }
}
