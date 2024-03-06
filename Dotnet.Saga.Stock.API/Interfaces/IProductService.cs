using Dotnet.Saga.Stock.API.Models;

namespace Dotnet.Saga.Stock.API.Interfaces;

public interface IProductService
{
    Task<IList<ProductModel>> GetAllAsync();
}