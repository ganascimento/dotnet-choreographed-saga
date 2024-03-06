using Dotnet.Saga.Stock.API.Entities;
using Dotnet.Saga.Stock.API.Infra.Context;
using Dotnet.Saga.Stock.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Stock.API.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbSet<ProductEntity> _dbSet;

    public ProductRepository(DataContext context)
    {
        _dbSet = context.Set<ProductEntity>();
    }

    public async Task<IList<ProductEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}