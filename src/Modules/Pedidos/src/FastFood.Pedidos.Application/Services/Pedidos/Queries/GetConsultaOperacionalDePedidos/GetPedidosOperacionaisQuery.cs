using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Queries.GetConsultaOperacionalDePedidos;

public sealed record GetPedidosOperacionaisQuery() : ICommand<GetPedidosOperacionaisResponse>;

public sealed record GetPedidosOperacionaisResponse(
    IEnumerable<PedidoOperacionalResponse> Pedidos);

public sealed record PedidoOperacionalResponse(
    Ulid Id,
    string Codigo,
    string Status,
    DateTime DataDeCriacao,
    DateTime? DataDeAlteracao,
    decimal ValorTotal,
    bool Pago,
    IEnumerable<ItemDePedidoOperacionalResponse> Itens);

public sealed record ItemDePedidoOperacionalResponse(
    Ulid Id,
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade,
    string? Observacao);