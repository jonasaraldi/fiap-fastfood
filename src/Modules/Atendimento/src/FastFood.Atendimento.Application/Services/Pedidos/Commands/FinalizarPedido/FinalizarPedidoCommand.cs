using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.FinalizarPedido;

public record FinalizarPedidoCommand(Ulid PedidoId) : ICommand<FinalizarPedidoResponse>
{
}

public record FinalizarPedidoResponse(Ulid PedidoId, string Status);