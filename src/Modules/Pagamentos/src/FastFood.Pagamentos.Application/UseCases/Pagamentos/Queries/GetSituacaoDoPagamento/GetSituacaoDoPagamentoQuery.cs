using FastFood.Pagamentos.Application.Abstractions;

namespace FastFood.Pagamentos.Application.UseCases.Pagamentos.Queries.GetSituacaoDoPagamento;

public record GetSituacaoDoPagamentoQuery(Ulid PedidoId) : ICommand<GetPagamentosResponse>;
public record GetPagamentosResponse(Ulid PedidoId, string Situacao);