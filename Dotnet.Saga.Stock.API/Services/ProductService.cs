using Dotnet.Saga.Stock.API.Interfaces;
using Dotnet.Saga.Stock.API.Models;

namespace Dotnet.Saga.Stock.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductModel>> GetAllAsync()
    {
        return (await _productRepository.GetAllAsync())
            .Select(product => new ProductModel
            {
                Name = product.Name,
                CurrentValue = product.CurrentValue,
                Description = product.Description,
            }).ToList();
    }
}