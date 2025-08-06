using System;

namespace Crypto_back.Models;

public class CoinPrice
{
    public required DateOnly Date { get; set; }
    
    public required decimal Price { get; set; }
}
