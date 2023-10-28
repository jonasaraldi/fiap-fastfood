using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.CriarPedido;

public record CriarPedidoCommand : ICommand<CriarPedidoResponse>
{
}

public record CriarPedidoResponse(Ulid PedidoId);
