using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.FinalizarPedido;

public record FinalizarPedidoCommand(Ulid PedidoId) : ICommand<FinalizarPedidoResponse>
{
}

public record FinalizarPedidoResponse(Ulid PedidoId, string Status);