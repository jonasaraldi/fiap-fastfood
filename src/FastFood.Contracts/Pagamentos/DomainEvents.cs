using FastFood.Contracts.Abstractions;

namespace FastFood.Contracts.Pagamentos;

public static class DomainEvents
{
    public record PagamentoCriado(Ulid PedidoId) : DomainEvent;
    public record PagamentoAprovado(Ulid PedidoId) : DomainEvent;
    public record PagamentoRecusado(Ulid PedidoId) : DomainEvent;
}