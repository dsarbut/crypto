using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoMaui.ViewModels;
public partial class CoinSelectionViewModel : ObservableObject
{
    public CoinSelectionViewModel()
    {
    }

    public IEnumerable<CoinSelectionViewModelItem> Items { get; private set; } = [];

    public void SetItems(IEnumerable<CoinSelectionViewModelItem> items)
    {
        Items = items;
        OnPropertyChanged(nameof(Items));
    }
}
