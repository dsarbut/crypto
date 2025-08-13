using CommunityToolkit.Mvvm.ComponentModel;


namespace CryptoMaui.ViewModels
{
    public partial class DcaViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string CoinName { get; set; } = "";


        [ObservableProperty]
        public partial decimal Dca {  get; set; }


        [ObservableProperty]
        public partial decimal TotalInvestment { get; set; }


        [ObservableProperty]
        public partial decimal TotalCoin { get; set; }


        [ObservableProperty]
        public partial decimal ValueToday { get; set; }


        [ObservableProperty]
        public partial decimal RoiRate { get; set; }
    }
}
