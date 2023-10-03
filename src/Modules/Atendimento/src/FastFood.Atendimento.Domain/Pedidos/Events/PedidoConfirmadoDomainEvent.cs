using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Events;

public record PedidoConfirmadoDomainEvent(Ulid PedidoId) : DomainEvent;