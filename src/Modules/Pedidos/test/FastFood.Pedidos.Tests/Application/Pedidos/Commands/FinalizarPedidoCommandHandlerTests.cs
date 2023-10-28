using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.FinalizarPedido;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Domain.Pedidos.Repositories.InMemory;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;

namespace FastFood.Pedidos.Tests.Application.Pedidos.Commands;

public class FinalizarPedidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveFinalizarPedido()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();

        Pedido pedido = CriarPedidoParaFinalizar();
        await repository.AddAsync(pedido, CancellationToken.None);
        
        FinalizarPedidoCommandHandler handler = new(repository, unitOfWork);
        FinalizarPedidoCommand command = new(pedido.Id);
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(pedido.Id, response.PedidoId);
    }
    
    [Fact]
    public async Task Handle_NaoDeveConfirmarPedido_QuandoPedidoNaoForEncontrado()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        
        var pedido = CriarPedidoParaFinalizar();
        await repository.AddAsync(pedido, CancellationToken.None);

        Ulid pedidoIdAleatorio = Ulid.NewUlid();
        FinalizarPedidoCommand command = new(pedidoIdAleatorio);
        FinalizarPedidoCommandHandler handler = new(repository, unitOfWork);
        
        await Assert.ThrowsAsync<PedidoNaoEncontradoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }

    private Pedido CriarPedidoParaFinalizar()
    {
        ItemDePedido itemDePedido = ItemDePedido.Criar(
            "Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        
        return Pedido.Criar()
            .AdicionarItem(itemDePedido)
            .Confirmar()
            .Receber()
            .Preparar()
            .MarcarComoPronto();
    }
}