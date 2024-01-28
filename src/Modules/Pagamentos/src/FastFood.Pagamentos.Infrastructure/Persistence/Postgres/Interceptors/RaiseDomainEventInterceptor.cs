using FastFood.Contracts.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Interceptors;

public sealed class RaiseDomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public RaiseDomainEventInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if(dbContext is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var publishTasks = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(aggregateRoot =>
            { 
                var domainEvents = aggregateRoot.GetDomainEvents();
                aggregateRoot.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => 
                _publisher.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(publishTasks);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}