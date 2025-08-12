
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CryptoMaui.ViewModels;
public partial class InvestmentViewModel : ObservableObject
{
    public InvestmentViewModel()
    {
        CoinDatas = [];
        Year = 2025;
        Month = 1;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Date))]
    public partial int Month { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Date))]
    public partial int Year { get; set; }

    public decimal InvestedAmmount { get; set; }

    public DateTime Date => new(Year, Month, 1);

    [ObservableProperty]
    public partial ObservableCollection<CoinDataViewModel> CoinDatas { get; set; }
}
