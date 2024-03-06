using Dotnet.Saga.Payment.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Saga.Payment.Service.Infra.Mappings;

public class PaymentMapping : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        builder
            .ToTable("tb_payment");

        builder.HasKey(p => p.Id);
    }
}