using Dotnet.Saga.Order.API.Entities.Base;
using Dotnet.Saga.Order.API.Enums;

namespace Dotnet.Saga.Order.API.Entities;

public class OrderEntity : BaseEntity
{
    public OrderStatusEnum Status { get; private set; } = OrderStatusEnum.Created;
    public decimal TotalValue
    {
        get
        {
            if (this.Products == null) throw new InvalidOperationException("Invalid products!!!");

            return this.Products.Sum(x => x.Amount * x.Value) - this.DiscountValue;
        }
    }
    public required decimal DiscountValue { get; set; }
    public virtual ICollection<OrderProductEntity>? Products { get; private set; }
    public virtual RequesterEntity? Requester { get; set; }

    public void SetStatus(OrderStatusEnum status)
    {
        if ((this.Status != OrderStatusEnum.Created && status != OrderStatusEnum.Canceled) || this.Status == OrderStatusEnum.Canceled)
            throw new InvalidOperationException("Invalid operation!!!");

        this.Status = status;
    }

    public void AddProduct(OrderProductEntity product)
    {
        if (this.Products == null)
            this.Products = new List<OrderProductEntity>();

        if (product.Value <= 0)
            throw new InvalidOperationException("Product need value!!!");

        if (product.Amount <= 0)
            throw new InvalidOperationException("Product need amount!!!");

        this.Products.Add(product);
    }

    public OrderEntity()
    {
        this.Status = OrderStatusEnum.Created;
    }
}