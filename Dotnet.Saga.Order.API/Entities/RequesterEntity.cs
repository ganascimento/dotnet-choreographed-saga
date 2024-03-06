using Dotnet.Saga.Order.API.Entities.Base;

namespace Dotnet.Saga.Order.API.Entities;

public class RequesterEntity : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}