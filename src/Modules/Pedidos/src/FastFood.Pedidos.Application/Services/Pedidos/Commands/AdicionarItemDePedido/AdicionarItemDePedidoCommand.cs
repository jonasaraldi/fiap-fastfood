using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.AdicionarItemDePedido;

public sealed record AdicionarItemDePedidoCommand(
    Ulid PedidoId,
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade,
    string? Observacao = null)
    : ICommand<AdicionarItemDePedidoResponse>;

public sealed record AdicionarItemDePedidoResponse(
    Ulid PedidoId, Ulid ItemDePedidoId, decimal ValorTotal);