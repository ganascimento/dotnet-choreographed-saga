using Dotnet.Saga.Order.API.Entities;
using Dotnet.Saga.Order.API.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Order.API.Infra.Context;

public class DataContext : DbContext
{
    public DbSet<OrderEntity>? Order { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderMapping());
        modelBuilder.ApplyConfiguration(new OrderProductMapping());
        modelBuilder.ApplyConfiguration(new RequesterMapping());
    }
}