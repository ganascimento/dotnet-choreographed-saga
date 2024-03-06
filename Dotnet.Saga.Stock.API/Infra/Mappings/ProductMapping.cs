using Dotnet.Saga.Stock.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Saga.Stock.API.Infra.Mappings;

public class ProductMapping : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
            .ToTable("tb_product");

        builder.HasKey(p => p.Id);
    }
}