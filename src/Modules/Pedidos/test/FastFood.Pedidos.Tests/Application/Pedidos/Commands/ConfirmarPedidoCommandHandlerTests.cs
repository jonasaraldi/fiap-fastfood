using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.ConfirmarPedido;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Domain.Pedidos.Repositories.InMemory;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;

namespace FastFood.Pedidos.Tests.Application.Pedidos.Commands;

public class ConfirmarPedidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveConfirmarPedido()
    {
        Pedido pedido = CriarPedidoProntoParaConfirmar();
        IPedidoRespository respository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        await respository.AddAsync(pedido, CancellationToken.None);
        
        ConfirmarPedidoCommand command = new(pedido.Id);
        ConfirmarPedidoCommandHandler handler = new(respository, unitOfWork);
        
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(pedido.Id, response.PedidoId);
    }
    
    [Fact]
    public async Task Handle_NaoDeveConfirmarPedido_QuandoPedidoNaoForEncontrado()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        
        var pedido = Pedido.Criar();
        await repository.AddAsync(pedido, CancellationToken.None);

        Ulid pedidoIdAleatorio = Ulid.NewUlid();
        ConfirmarPedidoCommand command = new(pedidoIdAleatorio);
        ConfirmarPedidoCommandHandler handler = new(repository, unitOfWork);
        
        await Assert.ThrowsAsync<PedidoNaoEncontradoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }

    private Pedido CriarPedidoProntoParaConfirmar()
    {
        Pedido pedido = Pedido.Criar();
        ItemDePedido itemDePedido = ItemDePedido.Criar("Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        pedido.AdicionarItem(itemDePedido);

        return pedido;
    }
}