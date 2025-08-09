using CryptoBack.Prices;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CryptoBack.Models;

public class CoinPrices
{
    public Dictionary<string, IEnumerable<CoinPrice>> priceHistoryByCurrencyId = [];

    public IEnumerable<string> CoinIds => priceHistoryByCurrencyId.Keys;


    public async Task LoadAsync(string csvDataDir)
    {
        var dataFiles = Directory.GetFiles(csvDataDir, "*.csv");
        List<Task<(string, IEnumerable<CoinPrice>)>> loadTasks = [];
        foreach (var file in dataFiles)
            loadTasks.Add(LoadFileAsync(file));

        await Task.WhenAll(loadTasks);

        foreach (var task in loadTasks)
        {
            priceHistoryByCurrencyId[task.Result.Item1] = task.Result.Item2;
        }
    }

    public static async Task<(string, IEnumerable<CoinPrice>)> LoadFileAsync(string csvFileName)
    {
        List<CoinPrice> priceList = [];

        string coinSymbol = Path.GetFileNameWithoutExtension(csvFileName);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = "\r\n",
            HasHeaderRecord = true,
            Delimiter = ";",
            BadDataFound = new BadDataFound(args => { })
        };

        using (var reader = new StreamReader(csvFileName))
        using (var csv = new CsvReader(reader, config))
        {
            try
            {
                var rows = csv.GetRecordsAsync<CsvPriceRow>();

                await foreach (var row in rows)
                {
                    priceList.Add(
                        new CoinPrice()
                        {
                            Date = row.TimeStamp,
                            Price = row.Open
                        });
                }
            }
            catch (Exception)
            {

            }
        }

        return (coinSymbol, priceList.OrderBy(price => price.Date));
    }

}
