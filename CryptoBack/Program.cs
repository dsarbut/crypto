using CryptoBack.Models;
using Microsoft.OpenApi.Models;

namespace CryptoBack;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<CoinPrices>();

        builder.Services.AddControllers();

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
            c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date" });
        });


        var connectionString = "Data Source=Investments.db";
        builder.Services.AddSqlite<InvestmentDatabase>(connectionString);



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        await app.Services.GetRequiredService<CoinPrices>().LoadAsync("Prices");

        app.Run();
    }
}
