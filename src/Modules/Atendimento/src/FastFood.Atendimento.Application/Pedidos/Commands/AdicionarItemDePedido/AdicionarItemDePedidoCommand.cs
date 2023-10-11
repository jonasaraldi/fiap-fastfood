using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.AdicionarItemDePedido;

public sealed record AdicionarItemDePedidoCommand(
    Ulid PedidoId,
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade) 
    : ICommand<AdicionarItemDePedidoResponse>
{
}

public sealed record AdicionarItemDePedidoResponse(
    Ulid PedidoId, Ulid ItemDePedidoId, decimal ValorTotal);