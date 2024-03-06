using Dotnet.Saga.Order.API.Entities;
using Dotnet.Saga.Order.API.Infra.Context;
using Dotnet.Saga.Order.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Order.API.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private readonly DbSet<OrderEntity> _dbSet;

    public OrderRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<OrderEntity>();
    }

    public async Task<OrderEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Products)
            .Include(x => x.Requester)
            .Where(order => order.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<OrderEntity?> CreateAsync(OrderEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task UpdateAsync(OrderEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}