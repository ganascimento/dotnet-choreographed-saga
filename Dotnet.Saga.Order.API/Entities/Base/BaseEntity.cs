namespace Dotnet.Saga.Order.API.Entities.Base;

public class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted { get; private set; } = false;

    public BaseEntity()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.UtcNow;
    }

    public void SetDeleted()
    {
        this.IsDeleted = true;
        this.DeletedAt = DateTime.UtcNow;
    }
}