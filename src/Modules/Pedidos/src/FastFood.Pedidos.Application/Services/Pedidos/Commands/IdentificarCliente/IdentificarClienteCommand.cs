using FastFood.Pedidos.Application.Abstractions;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.IdentificarCliente;

public sealed record IdentificarClienteCommand(
    Ulid PedidoId, 
    string Nome, 
    string Email) : ICommand;