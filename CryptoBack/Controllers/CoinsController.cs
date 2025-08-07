using CryptoBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoinsController : ControllerBase
{
    private readonly CoinPrices coinPrices;
    private readonly ILogger<CoinsController> _logger;

    public CoinsController(CoinPrices coinPrices, ILogger<CoinsController> logger)
    {
        this.coinPrices = coinPrices;
        _logger = logger;
    }


    [HttpGet("InstalledCoins")]
    public IEnumerable<string> Get()
    {
        return coinPrices.CoinIds;
    }

    [HttpGet("CoinPrice")]
    [ProducesResponseType<IEnumerable<CoinPrice>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<List<CoinPrice>>> Get([FromQuery] string coinId)
    {
        if (coinPrices.priceHistoryByCurrencyId.TryGetValue(coinId, out IEnumerable<CoinPrice>? currencyHistory))
        {
            return Ok(currencyHistory);
        }
        return NotFound();
    }
}
