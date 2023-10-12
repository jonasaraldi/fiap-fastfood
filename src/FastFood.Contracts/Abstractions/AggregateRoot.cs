namespace FastFood.Contracts.Abstractions;

public abstract class AggregateRoot : AuditableEntity
{
    private readonly List<DomainEvent> _domainEvents = new();

    protected AggregateRoot()
    {
    }
    
    public IReadOnlyCollection<DomainEvent> GetDomainEvents() =>
        _domainEvents.ToList();

    public void ClearDomainEvents() => 
        _domainEvents.Clear();

    protected void RaiseDomainEvent(DomainEvent domainEvent) => 
        _domainEvents.Add(domainEvent);
}