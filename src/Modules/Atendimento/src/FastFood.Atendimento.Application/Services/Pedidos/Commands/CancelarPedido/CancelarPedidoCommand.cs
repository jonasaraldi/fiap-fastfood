using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.CancelarPedido;

public record CancelarPedidoCommand(Ulid PedidoId) 
    : ICommand<CancelarPedidoResponse>
{
}

public record CancelarPedidoResponse(
    Ulid PedidoId, string Status);