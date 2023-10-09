using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.ConfirmarPedido;

public record ConfirmarPedidoCommand(Ulid PedidoId) 
    : ICommand<ConfirmarPedidoResponse>
{
}

public record ConfirmarPedidoResponse(
    Ulid PedidoId, string Status, decimal ValorTotal);