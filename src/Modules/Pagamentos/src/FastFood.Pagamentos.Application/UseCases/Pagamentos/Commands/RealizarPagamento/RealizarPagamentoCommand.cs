using FastFood.Pagamentos.Application.Abstractions;

namespace FastFood.Pagamentos.Application.UseCases.Pagamentos.Commands.RealizarPagamento;

public sealed record RealizarPagamentoCommand(Ulid PedidoId) : ICommand;