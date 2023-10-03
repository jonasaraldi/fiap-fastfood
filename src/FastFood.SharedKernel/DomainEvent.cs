namespace FastFood.SharedKernel;

public abstract record DomainEvent
{
    public DomainEvent()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    protected DateTime CreatedAt { get; set; }
}