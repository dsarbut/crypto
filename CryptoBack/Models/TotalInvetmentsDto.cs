namespace CryptoBack.Models;

public class TotalInvetmentsDto
{
    public TotalInvetmentsDto(IEnumerable<InvestmentDto> investments)
    {
        Investments = investments;
        Dcas = ComputeDcas(investments);
    }

    public IEnumerable<InvestmentDto> Investments { get; set; }

    public IEnumerable<DcaDto> Dcas { get; set; }

    public static IEnumerable<DcaDto> ComputeDcas(IEnumerable<InvestmentDto> investments)
    {

        Dictionary<string, List<InvestmentDto>> investmentsByCoin = [];
        foreach (var investment in investments)
        {
            if (!investmentsByCoin.TryGetValue(investment.CoinName, out List<InvestmentDto>? investList))
            {
                investList = [];
                investmentsByCoin[investment.CoinName] = investList;
            }

            investList.Add(investment);
        }

        List<DcaDto> dcas = [];
        foreach (var entry in investmentsByCoin)
        {
            dcas.Add(ComputeDCA(entry.Key, entry.Value));
        }

        return dcas;
    }


    private static DcaDto ComputeDCA(string coninName, IEnumerable<InvestmentDto> investments)
    {
        var orderedInvestmets = investments.OrderBy(x => x.Date);

        DcaDto dca = new()
        {
            CoinName = coninName,
        };

        decimal totalInvest = 0, totalCoin = 0;
        foreach (var investment in orderedInvestmets)
        {
            totalInvest += investment.InvestmentValue;
            totalCoin += (investment.InvestmentValue / investment.CoinPrice);
        }

        dca.TotalInvestment = totalInvest;
        dca.TotalCoin = totalCoin;
        dca.Dca = totalInvest / totalCoin;

        dca.ValueToday = totalCoin * orderedInvestmets.Last().CoinPrice;

        dca.Roi = ((dca.ValueToday - totalInvest) / totalInvest) * 100;

        return dca;
    }
}
