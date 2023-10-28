using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.CancelarPedido;

public record CancelarPedidoCommand(Ulid PedidoId) 
    : ICommand<CancelarPedidoResponse>
{
}

public record CancelarPedidoResponse(
    Ulid PedidoId, string Status);