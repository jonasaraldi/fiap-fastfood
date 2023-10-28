using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.ConfirmarPedido;

public record ConfirmarPedidoCommand(Ulid PedidoId) 
    : ICommand<ConfirmarPedidoResponse>
{
}

public record ConfirmarPedidoResponse(
    Ulid PedidoId, string Status, decimal ValorTotal);