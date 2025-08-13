using CryptoBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoinsController : ControllerBase
{
    private readonly InvestmentDatabase investmentDatabase;
    private readonly CoinPrices coinPrices;

    public CoinsController(InvestmentDatabase investmentDatabase, CoinPrices coinPrices)
    {
        this.investmentDatabase = investmentDatabase;
        this.coinPrices = coinPrices;
    }


    [HttpGet("GetInstalledCoins")]
    public IEnumerable<string> Get()
    {
        return coinPrices.CoinIds;
    }

    [HttpGet("GetPriceHistory")]
    [ProducesResponseType<IEnumerable<CoinPriceDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<List<CoinPriceDto>>> GetHistory([FromQuery] string coinId)
    {
        if (coinPrices.priceHistoryByCurrencyId.TryGetValue(coinId, out IEnumerable<CoinPriceDto>? currencyHistory))
        {
            return Ok(currencyHistory);
        }
        return NotFound();
    }

    [HttpGet("GetInvestments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<TotalInvetmentsDto> GetInvestments()
    {
        investmentDatabase.Database.EnsureCreated();
        

        IEnumerable<InvestmentDto> investments = investmentDatabase.Investments.Select
            (investment => new InvestmentDto()
            {
                InvestmentValue = investment.InvestmentValue,
                CoinPrice = investment.CoinPrice,
                CoinName = investment.CoinName,
                Date = investment.Date,
            });

        TotalInvetmentsDto rv = new (investments.ToArray());
        return Ok(rv);
    }


    [HttpPost("AddInvestment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> AddInvestment([FromBody] InvestmentDto investment)
    {
        try
        {
            await investmentDatabase.Investments.AddAsync(new InvestmentModel()
            {
                CoinPrice = investment.CoinPrice,
                CoinName = investment.CoinName,
                Date = investment.Date,
                InvestmentValue = investment.InvestmentValue,
            });
            await investmentDatabase.SaveChangesAsync();
            return Ok();
        }
        catch (Exception _)
        {
            return UnprocessableEntity();
        }

    }
}
