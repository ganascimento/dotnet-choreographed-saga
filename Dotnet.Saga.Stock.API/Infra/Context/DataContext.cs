using Dotnet.Saga.Stock.API.Entities;
using Dotnet.Saga.Stock.API.Infra.Mappings;
using Dotnet.Saga.Stock.API.Infra.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Stock.API.Infra.Context;

public class DataContext : DbContext
{
    public DbSet<ProductEntity>? Product { get; set; }
    public DbSet<StockEntity>? Stock { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMapping());
        modelBuilder.ApplyConfiguration(new StockMapping());
        modelBuilder.ApplyConfiguration(new StockHistoryMapping());

        ProductSeed.Seed(modelBuilder);
        StockSeed.Seed(modelBuilder);
    }
}