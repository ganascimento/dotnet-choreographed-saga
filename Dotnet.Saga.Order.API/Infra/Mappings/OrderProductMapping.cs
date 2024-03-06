using Dotnet.Saga.Order.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Saga.Order.API.Infra.Mappings;

public class OrderProductMapping : IEntityTypeConfiguration<OrderProductEntity>
{
    public void Configure(EntityTypeBuilder<OrderProductEntity> builder)
    {
        builder
            .ToTable("tb_order_product");

        builder.HasKey(p => p.Id);
    }
}
