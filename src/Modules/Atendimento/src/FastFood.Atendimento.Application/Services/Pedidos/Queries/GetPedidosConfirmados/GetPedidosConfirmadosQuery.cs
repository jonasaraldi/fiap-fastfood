using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Services.Pedidos.Queries.GetPedidosConfirmados;

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
    int Quantidade,
    string? Observacao);