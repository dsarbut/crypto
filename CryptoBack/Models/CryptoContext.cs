using Microsoft.EntityFrameworkCore;

namespace CryptoBack.Models;

public class CryptoContext : DbContext
{
    public CryptoContext()
    {

    }

    public DbSet<CurrencyId> CurrencyIds { get; set; }
}
