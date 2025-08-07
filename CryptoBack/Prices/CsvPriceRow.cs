using CsvHelper.Configuration.Attributes;

namespace CryptoBack.Prices;

public class CsvPriceRow
{
    [Index(0)]
    public string Name { get; set; } = "";

    [Index(1)]
    public decimal Open { get; set; }

    [Index(2)]
    public decimal High { get; set; }

    [Index(3)]
    public decimal Low { get; set; }

    [Index(4)]
    public decimal Close { get; set; }

    [Index(5)]
    public decimal Volume { get; set; }

    [Index(6)]
    public decimal MarketCap { get; set; }

    [Index(7)]
    public DateTimeOffset TimeStamp { get; set; }
}
