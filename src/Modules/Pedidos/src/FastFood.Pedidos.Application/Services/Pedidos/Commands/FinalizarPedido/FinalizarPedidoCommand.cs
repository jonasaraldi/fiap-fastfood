using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.FinalizarPedido;

public record FinalizarPedidoCommand(Ulid PedidoId) : ICommand<FinalizarPedidoResponse>
{
}

public record FinalizarPedidoResponse(Ulid PedidoId, string Status);