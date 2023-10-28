using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.AtualizarCpf;

public sealed record AtualizarCpfCommand(Ulid PedidoId, string Cpf) : ICommand;