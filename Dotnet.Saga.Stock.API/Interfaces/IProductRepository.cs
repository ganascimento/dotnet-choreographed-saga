using Dotnet.Saga.Stock.API.Entities;

namespace Dotnet.Saga.Stock.API.Interfaces;

public interface IProductRepository
{
    Task<IList<ProductEntity>> GetAllAsync();
}