using Dotnet.Saga.Payment.Service.Enums;

namespace Dotnet.Saga.Payment.Service.Models;

public class PaymentResultModel
{
    public required PaymentStatusEnum Status { get; set; }
    public required Guid Ident { get; set; }
}