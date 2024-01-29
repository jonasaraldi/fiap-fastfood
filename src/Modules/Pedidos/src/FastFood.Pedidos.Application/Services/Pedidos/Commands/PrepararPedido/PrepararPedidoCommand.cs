using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.PrepararPedido;

public record PrepararPedidoCommand(Ulid PedidoId) : ICommand<PrepararPedidoResponse>;
public record PrepararPedidoResponse(Ulid PedidoId, string Status);