namespace CryptoBack.Models;

public class CoinPriceDto
{
    public required DateTime Date { get; set; }

    public required decimal Price { get; set; }
}
