using Dotnet.Saga.Stock.API.Entities.Base;
using Dotnet.Saga.Stock.API.Enums;

namespace Dotnet.Saga.Stock.API.Entities;

public class StockEntity : BaseEntity
{
    public required int Amount { get; set; }
    public virtual ProductEntity? Product { get; set; }
    public virtual ICollection<StockHistoryEntity>? StockHistory { get; set; }

    public void GetAmount(int amount)
    {
        if (this.Amount < amount)
            throw new InvalidOperationException("Amount is invalid!");

        this.Amount -= amount;
    }

    public void AddAmount(int amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Amount is invalid!");

        this.Amount += amount;
    }

    public void AddHistory(int amount, Guid orderId, StockStatusEnum status)
    {
        if (this.StockHistory == null)
            this.StockHistory = new List<StockHistoryEntity>();

        this.StockHistory.Add(new StockHistoryEntity
        {
            Amount = amount,
            OrderId = orderId,
            Status = status,
        });
    }

    public void CancelHistories()
    {
        this.StockHistory?.ToList().ForEach(history =>
        {
            history.Status = StockStatusEnum.Canceled;
        });
    }

    public void PaymentErrorHistories()
    {
        this.StockHistory?.ToList().ForEach(history =>
        {
            history.Status = StockStatusEnum.PaymentError;
        });
    }
}