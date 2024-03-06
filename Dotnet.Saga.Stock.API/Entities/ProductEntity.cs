using Dotnet.Saga.Stock.API.Entities.Base;

namespace Dotnet.Saga.Stock.API.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }
    public decimal CurrentValue { get; set; }
    public string? Description { get; set; }

    public ProductEntity()
    {

    }

    public ProductEntity(Guid id)
    {
        this.Id = id;
    }
}