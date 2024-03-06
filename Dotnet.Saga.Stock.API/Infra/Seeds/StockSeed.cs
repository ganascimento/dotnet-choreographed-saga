using Dotnet.Saga.Stock.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Stock.API.Infra.Seeds;

public static class StockSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockEntity>().HasData(
            new { Id = Guid.Parse("0e45f9f6-738b-433c-a9b2-2350b38b5322"), ProductId = Guid.Parse("b584159e-eeac-4586-bd76-557249d61daf"), Amount = 30, CreatedAt = DateTime.UtcNow, IsDeleted = false },
            new { Id = Guid.Parse("33c33659-0b4b-4667-9051-62382c64ceab"), ProductId = Guid.Parse("20db60a4-c53d-4afd-a1ad-42d15f470e2c"), Amount = 30, CreatedAt = DateTime.UtcNow, IsDeleted = false },
            new { Id = Guid.Parse("4fa4615d-40d1-42f7-b539-4d2f74adfa0d"), ProductId = Guid.Parse("5f36da7e-7c77-4ea5-b65f-1bf3744fdc78"), Amount = 30, CreatedAt = DateTime.UtcNow, IsDeleted = false }
        );
    }
}