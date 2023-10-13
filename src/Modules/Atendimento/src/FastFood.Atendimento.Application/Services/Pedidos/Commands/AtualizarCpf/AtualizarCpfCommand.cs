using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Services.Pedidos.Commands.AtualizarCpf;

public sealed record AtualizarCpfCommand(Ulid PedidoId, string Cpf) : ICommand;