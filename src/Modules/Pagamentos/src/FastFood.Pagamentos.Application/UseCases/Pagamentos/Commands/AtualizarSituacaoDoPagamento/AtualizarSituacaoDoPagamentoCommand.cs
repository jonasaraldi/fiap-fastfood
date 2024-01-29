using FastFood.Pagamentos.Application.Abstractions;

namespace FastFood.Pagamentos.Application.UseCases.Pagamentos.Commands.AtualizarSituacaoDoPagamento;

public record AtualizarSituacaoDoPagamentoCommand(Ulid PedidoId, string CodigoDaSituacao) : ICommand;