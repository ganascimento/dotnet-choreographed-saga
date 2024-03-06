using Dotnet.Saga.Order.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Saga.Order.API.Infra.Mappings;

public class OrderMapping : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder
            .ToTable("tb_order");

        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.Products);

        builder
            .HasOne(p => p.Requester);
    }
}