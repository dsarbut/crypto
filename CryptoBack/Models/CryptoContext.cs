using Microsoft.EntityFrameworkCore;

namespace Crypto_back.Models;

public class CryptoContext : DbContext
{
    public CryptoContext()
    {
            
    }

    public DbSet<CurrencyId> CurrencyIds { get; set; }
}
