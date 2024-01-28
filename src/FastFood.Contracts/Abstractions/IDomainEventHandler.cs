using FastFood.Contracts.Pagamentos;
using MediatR;

namespace FastFood.Contracts.Abstractions;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
}