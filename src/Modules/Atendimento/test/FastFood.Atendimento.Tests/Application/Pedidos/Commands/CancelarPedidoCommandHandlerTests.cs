using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.CancelarPedido;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;

namespace FastFood.Atendimento.Tests.Application.Pedidos.Commands;

public class CancelarPedidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveConfirmarCancelar()
    {
        Pedido pedido = Pedido.Criar();
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        await repository.AddAsync(pedido, CancellationToken.None);
        
        CancelarPedidoCommand command = new(pedido.Id);
        CancelarPedidoCommandHandler handler = new(repository, unitOfWork);
        
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(pedido.Id, response.PedidoId);
        Assert.Equal(pedido.Status.Descricao, response.Status);
    }
    
    [Fact]
    public async Task Handle_NaoDeveConfirmarPedido_QuandoPedidoNaoForEncontrado()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        
        var pedido = Pedido.Criar();
        await repository.AddAsync(pedido, CancellationToken.None);

        Ulid pedidoIdAleatorio = Ulid.NewUlid();
        CancelarPedidoCommand command = new(pedidoIdAleatorio);
        CancelarPedidoCommandHandler handler = new(repository, unitOfWork);
        
        await Assert.ThrowsAsync<PedidoNaoEncontradoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }
}