using BackendClient;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CryptoMaui.ViewModels;
public partial class PortfolioViewModel : ObservableObject
{
    private readonly CryptoBackClient cryptoBack;

    public PortfolioViewModel(CryptoBackClient cryptoBack)
    {
        this.cryptoBack = cryptoBack;
        Investments = [];
        Dcas = [];
    }

    [ObservableProperty]
    public partial bool IsLoading { get; set; } = true;

    public bool HasItems => Investments.Count > 0;

    [NotifyPropertyChangedFor(nameof(HasItems))]
    [ObservableProperty]
    public partial ObservableCollection<InvestmentViewModel> Investments { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<DcaViewModel> Dcas { get; set; }

    [RelayCommand]
    public async Task AddInvestment()
    {
        await Shell.Current.GoToAsync("AddInvestmentPage");
    }

    public async Task LoadPortfolio()
    {
        IsLoading = true;
        try
        {
            await RefreshInvestments();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task RefreshInvestments()
    {
        Investments.Clear();
        var totalInvestments = await cryptoBack.GetInvestmentsAsync();

        Dcas = [.. totalInvestments.Dcas.Select(dca => new DcaViewModel()
        {
            CoinName = dca.CoinName,
            Dca = dca.Dca,
            TotalCoin = dca.TotalCoin,
            TotalInvestment = dca.TotalInvestment,
            ValueToday = dca.ValueToday,
            RoiRate = dca.Roi
        })];

        var invGroups = totalInvestments.Investments
            .GroupBy(inv => new { inv.Date.Month, inv.Date.Year })
            .ToDictionary(group => group.Key, group => group.ToList());


        List<InvestmentViewModel> viewModels = [];
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
                    BuyingPrice = inv.CoinPrice,
                    InvestmentValue = inv.InvestmentValue
                };
                viewModel.CoinDatas.Add(coinVm);
            }

            viewModels.Add(viewModel);
        }

        var ordered = viewModels
            .Select(vm => new { Item = vm, Date = new DateOnly(vm.Year, vm.Month, 1) })
            .OrderBy(x => x.Date)
            .Select(x => x.Item);

        Investments = new ObservableCollection<InvestmentViewModel>(ordered);


    }

}
