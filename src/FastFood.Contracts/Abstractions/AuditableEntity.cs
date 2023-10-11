namespace FastFood.SharedKernel;

public abstract class AuditableEntity : Entity
{
    protected AuditableEntity(Ulid id) : base(id)
    {
    }

    protected AuditableEntity() : base()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    protected void RegisterUpdate() => 
        UpdatedAt = DateTime.UtcNow;
}