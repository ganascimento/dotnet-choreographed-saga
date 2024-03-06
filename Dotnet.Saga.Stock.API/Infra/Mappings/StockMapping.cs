using Dotnet.Saga.Stock.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Saga.Stock.API.Infra.Mappings;

public class StockMapping : IEntityTypeConfiguration<StockEntity>
{
    public void Configure(EntityTypeBuilder<StockEntity> builder)
    {
        builder
            .ToTable("tb_stock");

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Product);

        builder
            .HasMany(p => p.StockHistory)
            .WithOne(p => p.Stock);
    }
}