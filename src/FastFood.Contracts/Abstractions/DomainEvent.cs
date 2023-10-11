namespace FastFood.Contracts.Abstractions;

public abstract record DomainEvent
{
    public DomainEvent()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    protected DateTime CreatedAt { get; set; }
}