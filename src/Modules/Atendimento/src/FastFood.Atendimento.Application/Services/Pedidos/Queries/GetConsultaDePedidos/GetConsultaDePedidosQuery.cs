using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Services.Pedidos.Queries.GetConsultaDePedidos;

public sealed record GetConsultaDePedidosQuery(
    DateTime? DataInicial, DateTime? DataFinal) 
    : ICommand<GetConsultaDePedidosResponse>
{
}

public sealed record GetConsultaDePedidosResponse(
    DateTime? DataInicial,
    DateTime? DataFinal,
    IEnumerable<PedidoResponse> Pedidos);

public sealed record PedidoResponse(
    Ulid Id,
    string Codigo,
    string Status,
    DateTime DataDeCriacao,
    DateTime? DataDeAlteracao,
    decimal ValorTotal,
    IEnumerable<ItemDePedidoResponse> Itens,
    IEnumerable<HistoricoDePedidoResponse> Historicos);

public sealed record ItemDePedidoResponse(
    Ulid Id,
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade,
    string? Observacao);
    
public sealed record HistoricoDePedidoResponse(
    string Status,
    DateTime Data);