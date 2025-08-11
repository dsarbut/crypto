
namespace CryptoMaui.ViewModels;
public class InvestementViewModel
{
    public required DateTime Date { get; set; }

    public required string CoinName { get; set; }

    public required decimal CoinAmmount { get; set; }

    public required decimal InvestedAmmount { get; set; }

    public required decimal ValueToday {  get; set; } 

    public required decimal ReturnOfInvestment {  get; set; }

}
