using Dotnet.Saga.Payment.Service.Entities;
using Dotnet.Saga.Payment.Service.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Payment.Service.Infra.Context;

public class DataContext : DbContext
{
    public DbSet<PaymentEntity>? Order { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PaymentMapping());
    }
}