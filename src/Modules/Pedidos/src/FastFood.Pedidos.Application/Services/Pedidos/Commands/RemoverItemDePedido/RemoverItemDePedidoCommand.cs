using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.RemoverItemDePedido;

public record RemoverItemDePedidoCommand(Ulid PedidoId, Ulid ItemDePedidoId) 
    : ICommand<RemoverItemDePedidoResponse>
{
}

public record RemoverItemDePedidoResponse(
    Ulid PedidoId, Ulid ItemDePedidoId, decimal ValorTotal);