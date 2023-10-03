using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Events;

public record ItemDePedidoAdicionadoDomainEvent(Ulid PedidoId, Ulid ItemDePedidoId) : DomainEvent
{
}