using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Queries.GetPedidosConfirmados;

public sealed record GetPedidosConfirmadosQuery() : ICommand<GetPedidosConfirmadosResponse>
{
}

public sealed record GetPedidosConfirmadosResponse(
    IEnumerable<PedidoConfirmadoResponse> Pedidos);
    
public sealed record PedidoConfirmadoResponse(
    Ulid Id,
    string Codigo,
    DateTime Data,
    IEnumerable<ItemDePedidoConfirmadoResponse> Itens);

public sealed record ItemDePedidoConfirmadoResponse(
    Ulid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    string? Observacao);