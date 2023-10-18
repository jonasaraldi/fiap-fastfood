using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.IdentificarCliente;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;

namespace FastFood.Atendimento.Tests.Application.Pedidos.Commands;

public class IdentificarClienteCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveIdentificarCliente()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();

        Pedido pedido = Pedido.Criar();
        await repository.AddAsync(pedido, CancellationToken.None);
        
        IdentificarClienteCommandHandler handler = new(repository, unitOfWork);
        IdentificarClienteCommand command = new(pedido.Id, "John Doe", "john.doe@gmail.com");
        await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(pedido.Id, command.PedidoId);
        Assert.Equal(pedido.Cliente.Nome, command.Nome);
        Assert.Equal(pedido.Cliente.Email, command.Email);
    }
    
    [Fact]
    public async Task Handle_NaoDeveIdentificarCliente_QuandoPedidoNaoForEncontrado()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        
        Pedido pedido = Pedido.Criar();
        await repository.AddAsync(pedido, CancellationToken.None);

        Ulid pedidoIdAleatorio = Ulid.NewUlid();
        IdentificarClienteCommandHandler handler = new(repository, unitOfWork);
        IdentificarClienteCommand command = new(pedidoIdAleatorio, "John Doe", "john.doe@gmail.com");
        
        await Assert.ThrowsAsync<PedidoNaoEncontradoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }
}