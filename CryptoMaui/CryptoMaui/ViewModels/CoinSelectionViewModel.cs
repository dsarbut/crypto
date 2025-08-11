using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CryptoMaui.ViewModels;
public partial class CoinSelectionViewModel : ObservableObject
{

    public CoinSelectionViewModel()
    {
    }


    public bool AllItemsSelected =>  Items.All(item => item.IsSelected);

    public IEnumerable<CoinSelectionViewModelItem> Items { get; private set; } = [];


    public void SetItems(IEnumerable<CoinSelectionViewModelItem> items)
    {
        List<CoinSelectionViewModelItem> itms = [];

        foreach (var item in items)
        {
            item.IsSelected = true;
            itms.Add(item);
            itms.Add(item);
            itms.Add(item);
            itms.Add(item);
        }

        Items = itms;
        OnPropertyChanged(nameof(Items));
        OnPropertyChanged(nameof(AllItemsSelected));
    }

    [RelayCommand]
    public void SelectItem(CoinSelectionViewModelItem? item)
    {
        item?.IsSelected = !item.IsSelected;
        OnPropertyChanged(nameof(AllItemsSelected));
    }

    [RelayCommand]
    public void SelectAllItems()
    {
        foreach (var item in Items)
        {
            item.IsSelected = true;
        }
        OnPropertyChanged(nameof(AllItemsSelected));
    }
}
