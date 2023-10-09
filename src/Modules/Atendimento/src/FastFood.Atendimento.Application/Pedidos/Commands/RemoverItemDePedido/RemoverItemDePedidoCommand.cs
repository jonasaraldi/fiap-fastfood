using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.RemoverItemDePedido;

public record RemoverItemDePedidoCommand(Ulid PedidoId, Ulid ItemDePedidoId) 
    : ICommand<RemoverItemDePedidoResponse>
{
}

public record RemoverItemDePedidoResponse(
    Ulid PedidoId, Ulid ItemDePedidoId, decimal ValorTotal);