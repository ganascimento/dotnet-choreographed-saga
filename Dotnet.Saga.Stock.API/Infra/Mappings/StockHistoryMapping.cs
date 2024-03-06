using Dotnet.Saga.Stock.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Saga.Stock.API.Infra.Mappings;

public class StockHistoryMapping : IEntityTypeConfiguration<StockHistoryEntity>
{
    public void Configure(EntityTypeBuilder<StockHistoryEntity> builder)
    {
        builder
            .ToTable("tb_stock_history");

        builder.HasKey(p => p.Id);

        builder
            .HasOne(p => p.Stock)
            .WithMany(p => p.StockHistory);
    }
}