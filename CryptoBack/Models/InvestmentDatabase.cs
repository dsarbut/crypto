using Microsoft.EntityFrameworkCore;

namespace CryptoBack.Models;

public class InvestmentDatabase : DbContext
{
    public InvestmentDatabase(DbContextOptions options) : base(options)
    {

    }


    public DbSet<InvestmentModel> Investments { get; set; }
}
