namespace CryptoBack.Models;

public class InvestmentDto
{
    public DateTime Date { get; set; }

    public decimal CoinPrice { get; set; }

    public decimal InvestmentValue { get; set; }

    public string CoinName { get; set; } = "";
}
