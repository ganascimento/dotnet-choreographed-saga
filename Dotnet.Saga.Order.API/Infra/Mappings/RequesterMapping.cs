using Dotnet.Saga.Order.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Saga.Order.API.Infra.Mappings;

public class RequesterMapping : IEntityTypeConfiguration<RequesterEntity>
{
    public void Configure(EntityTypeBuilder<RequesterEntity> builder)
    {
        builder
            .ToTable("tb_requester");

        builder.HasKey(p => p.Id);
    }
}