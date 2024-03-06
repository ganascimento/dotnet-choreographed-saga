namespace Dotnet.Saga.Order.API.Enums;

public enum OrderStatusEnum
{
    Created,
    Canceled,
    PaymentError,
    PaymentSuccess
}