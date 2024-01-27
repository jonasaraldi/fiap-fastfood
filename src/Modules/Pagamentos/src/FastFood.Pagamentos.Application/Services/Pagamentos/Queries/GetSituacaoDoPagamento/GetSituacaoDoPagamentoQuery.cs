using FastFood.Pagamentos.Application.Abstractions;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Queries.GetSituacaoDoPagamento;

public record GetSituacaoDoPagamentoQuery(Ulid PedidoId) : ICommand<GetPagamentosResponse>;
public record GetPagamentosResponse(Ulid PedidoId, string Situacao);