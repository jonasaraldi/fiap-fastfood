using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.CriarPedido;

public record CriarPedidoCommand : ICommand<CriarPedidoResponse>
{
}

public record CriarPedidoResponse(Ulid PedidoId);
