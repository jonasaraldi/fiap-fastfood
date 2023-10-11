using FastFood.Atendimento.Application.Abstractions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.AtualizarCpf;

public sealed record AtualizarCpfCommand(Ulid PedidoId, string cpf) : ICommand;