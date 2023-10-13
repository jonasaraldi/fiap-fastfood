using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.CriarPedido;

public record CriarPedidoCommand : ICommand<CriarPedidoResponse>
{
}

public record CriarPedidoResponse(Ulid PedidoId);
