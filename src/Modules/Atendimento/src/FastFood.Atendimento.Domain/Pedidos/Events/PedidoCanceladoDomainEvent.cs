using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Events;

public record PedidoCanceladoDomainEvent(Ulid PedidoId) : DomainEvent;