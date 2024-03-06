using Dotnet.Saga.Payment.Service.Entities.Base;
using Dotnet.Saga.Payment.Service.Enums;

namespace Dotnet.Saga.Payment.Service.Entities;

public class PaymentEntity : BaseEntity
{
    public required Guid OrderId { get; set; }
    public required Guid PaymentIdent { get; set; }
    public PaymentStatusEnum Status { get; private set; }
    public required decimal Value { get; set; }

    public void SetStatus(PaymentStatusEnum status)
    {
        if (this.Status == PaymentStatusEnum.Canceled)
            throw new InvalidOperationException("Payment is canceled!");

        this.Status = status;
    }
}