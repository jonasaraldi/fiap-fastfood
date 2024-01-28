using MediatR;

namespace FastFood.Contracts.Abstractions;

public abstract record DomainEvent : INotification
{
    public DomainEvent()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    protected DateTime CreatedAt { get; set; }
}