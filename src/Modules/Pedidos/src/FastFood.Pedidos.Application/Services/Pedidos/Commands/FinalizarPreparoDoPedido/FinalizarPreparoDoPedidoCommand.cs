using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.FinalizarPreparoDoPedido;

public record FinalizarPreparoDoPedidoCommand(Ulid PedidoId) : ICommand<FinalizarPreparoDoPedidoResponse>;
public record FinalizarPreparoDoPedidoResponse(Ulid PedidoId, string Status);