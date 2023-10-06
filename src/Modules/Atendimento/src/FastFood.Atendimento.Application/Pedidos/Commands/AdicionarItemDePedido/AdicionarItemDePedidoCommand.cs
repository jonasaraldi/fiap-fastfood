using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.AdicionarItemDePedido;

public record AdicionarItemDePedidoCommand(
    Ulid PedidoId,
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade) 
    : ICommand<AdicionarItemDePedidoResponse>
{
}

public record AdicionarItemDePedidoResponse(Ulid PedidoId, Ulid Id);