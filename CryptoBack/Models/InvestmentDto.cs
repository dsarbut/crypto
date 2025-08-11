namespace CryptoBack.Models;

public class InvestmentDto
{
    public DateTime Date { get; set; }

    public decimal CoinAmmount { get; set; }

    public decimal InvestmentValue { get; set; }

    public string CoinName { get; set; } = "";

    public bool RepeatMonthly { get; set; }
}
