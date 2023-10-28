using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.CriarPedido;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Domain.Pedidos.Repositories.InMemory;

namespace FastFood.Pedidos.Tests.Application.Pedidos.Commands;

public class CriarPedidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveCriarPedido()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        CriarPedidoCommandHandler handler = new(repository, unitOfWork);

        CriarPedidoCommand command = new();
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotEqual(Ulid.Empty, response.PedidoId);
    }
}