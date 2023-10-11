using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Atendimento.Application.Pedidos.Commands.FinalizarPedido;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;

namespace FastFood.Atendimento.Tests.Application.Pedidos.Commands;

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