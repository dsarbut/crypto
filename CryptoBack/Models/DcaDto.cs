namespace CryptoBack.Models;

public class DcaDto
{
    public string CoinName { get; set; } = "";

    public decimal TotalInvestment { get; set; }

    public decimal TotalCoin { get; set; }

    public decimal Dca {  get; set; }

    public decimal ValueToday { get; set; }
    
    public decimal Roi { get; set; }
}
