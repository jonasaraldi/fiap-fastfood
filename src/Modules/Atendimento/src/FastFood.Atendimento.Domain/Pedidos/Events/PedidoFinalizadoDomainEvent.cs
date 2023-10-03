using FastFood.SharedKernel;

namespace FastFood.Atendimento.Domain.Pedidos.Events;

public record PedidoFinalizadoDomainEvent(Ulid PedidoId) : DomainEvent;