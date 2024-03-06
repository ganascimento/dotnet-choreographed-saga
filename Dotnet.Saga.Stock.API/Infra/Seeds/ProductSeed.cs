using Dotnet.Saga.Stock.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Stock.API.Infra.Seeds;

public static class ProductSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>().HasData(
            new ProductEntity { Id = Guid.Parse("b584159e-eeac-4586-bd76-557249d61daf"), CurrentValue = 4459, Name = "PS5" },
            new ProductEntity { Id = Guid.Parse("20db60a4-c53d-4afd-a1ad-42d15f470e2c"), CurrentValue = 3999, Name = "XBOX Series X" },
            new ProductEntity { Id = Guid.Parse("5f36da7e-7c77-4ea5-b65f-1bf3744fdc78"), CurrentValue = 2199, Name = "XBOX Series S" }
        );
    }
}