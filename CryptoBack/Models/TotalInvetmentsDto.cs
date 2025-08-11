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
        DcaDto dca = new()
        {
            CoinName = coninName,
        };

        decimal totalInvest = 0, totalCoin = 0;
        foreach (var investment in investments)
        {
            totalInvest += investment.InvestmentValue;
            totalCoin += investment.CoinAmmount;
        }

        dca.Dca = totalInvest / totalCoin;


        return dca;
    }
}
