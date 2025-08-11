using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoBack.Models;

public class InvestmentModel
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 

    public DateTime Date { get; set; }

    public decimal CoinAmmount { get; set; }

    public decimal InvestmentValue { get; set; }

    public string CoinName { get; set; } = "";
}
