using FastFood.Contracts.Abstractions;

namespace FastFood.Contracts.Pedidos;

public static class DomainEvents
{
    public record ItemDePedidoAdicionado(Ulid PedidoId, Ulid ItemDePedidoId) : DomainEvent;
    public record ItemDePedidoRemovido(Ulid PedidoId, Ulid ItemDePedidoId) : DomainEvent;
    public record ClienteIdentificado(Ulid PedidoId, Ulid ClienteId, string Nome, string Email) : DomainEvent;
    public record PedidoCancelado(Ulid PedidoId) : DomainEvent;
    public record PedidoConfirmado(Ulid PedidoId) : DomainEvent;
    public record PedidoPronto(Ulid PedidoId) : DomainEvent;
    public record PedidoRecebido(Ulid PedidoId) : DomainEvent;
    public record PedidoEmPreparacao(Ulid PedidoId) : DomainEvent;
    public record PedidoCriado(Ulid PedidoId) : DomainEvent;
    public record PedidoFinalizado(Ulid PedidoId) : DomainEvent;
}