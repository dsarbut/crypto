using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoMaui.ViewModels;

public partial class CoinSelectionViewModelItem : ObservableObject
{

    [ObservableProperty]
    public partial bool IsSelected { get; set; }

    [ObservableProperty]
    public partial string Label { get; set; }

    [ObservableProperty]
    public partial decimal Price{ get; set; }

    [ObservableProperty]
    public partial DateTime Date { get; set; }
}
