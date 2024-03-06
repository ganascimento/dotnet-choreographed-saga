using Dotnet.Saga.Stock.API.Entities;
using Dotnet.Saga.Stock.API.Enums;
using Dotnet.Saga.Stock.API.Infra.Context;
using Dotnet.Saga.Stock.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Stock.API.Infra.Repositories;

public class StockRepository : IStockRepository
{
    private readonly DataContext _context;
    private readonly DbSet<StockEntity> _dbSet;

    public StockRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<StockEntity>();
    }

    public async Task<StockEntity?> GetByProductIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Product)
            .Where(stock => stock.Product!.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<StockEntity>?> GetByOrderIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.StockHistory!.Where(p => p.OrderId == id && p.Status == StockStatusEnum.Created))
            .Include(x => x.Product)
            .Where(stock => stock.StockHistory!.First(x => x.OrderId == id).Stock!.Id == stock.Id)
            .ToListAsync();
    }

    public async Task UpdateAsync(List<StockEntity> entities, bool added = true)
    {

        entities.ForEach(entity =>
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            entity.StockHistory?.ToList().ForEach(history =>
            {
                if (added)
                {
                    if (_context.Entry(history).State == EntityState.Unchanged)
                        _context.Entry(history).State = EntityState.Modified;
                    else
                        _context.Entry(history).State = EntityState.Added;
                }
                else
                    _context.Entry(history).State = EntityState.Modified;

            });
        });

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
    }
}