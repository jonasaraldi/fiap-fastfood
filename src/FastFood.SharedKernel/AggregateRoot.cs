namespace FastFood.SharedKernel;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot()
    {
    }
    
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() =>
        _domainEvents.ToList();

    public void ClearDomainEvents() => 
        _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => 
        _domainEvents.Add(domainEvent);
}