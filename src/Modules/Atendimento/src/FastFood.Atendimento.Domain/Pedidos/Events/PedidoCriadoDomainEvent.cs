using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Events;

public record PedidoCriadoDomainEvent(Ulid PedidoId) : DomainEvent;