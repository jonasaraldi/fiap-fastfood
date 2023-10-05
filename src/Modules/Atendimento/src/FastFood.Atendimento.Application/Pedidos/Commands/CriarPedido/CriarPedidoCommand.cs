using MediatR;

namespace FastFood.Atendimento.Application.Pedidos.Commands.CriarPedido;

public record CriarPedidoCommand : IRequest<CriarPedidoCommandResponse>
{
}

public record CriarPedidoCommandResponse(Ulid PedidoId);
