using FastFood.Contracts.Abstractions;

namespace FastFood.Contracts.Pedidos;

public static class DomainEvents
{
    public record ItemDePedidoAdicionadoDomainEvent(Ulid PedidoId, Ulid ItemDePedidoId) : DomainEvent;
    public record ItemDePedidoRemovidoDomainEvent(Ulid PedidoId, Ulid ItemDePedidoId) : DomainEvent;
    public record PedidoCanceladoDomainEvent(Ulid PedidoId) : DomainEvent;
    public record PedidoConfirmadoDomainEvent(Ulid PedidoId) : DomainEvent;
    public record PedidoCriadoDomainEvent(Ulid PedidoId) : DomainEvent;
    public record PedidoFinalizadoDomainEvent(Ulid PedidoId) : DomainEvent;
}