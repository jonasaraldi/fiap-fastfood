using FastFood.Pagamentos.Application.Abstractions;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Commands.RealizarPagamento;

public sealed record RealizarPagamentoCommand(Ulid PedidoId) : ICommand;