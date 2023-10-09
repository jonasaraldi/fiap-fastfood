using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Atendimento.Application.Pedidos.Commands.ConfirmarPedido;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;

namespace FastFood.Atendimento.Tests.Application.Pedidos.Commands;

public class ConfirmarPedidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveConfirmarPedido()
    {
        Pedido pedido = CriarPedidoProntoParaConfirmar();
        IPedidoRespository pedidoRespository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        await pedidoRespository.AddAsync(pedido, CancellationToken.None);
        
        ConfirmarPedidoCommand command = new(pedido.Id);
        ConfirmarPedidoCommandHandler handler = new(pedidoRespository, unitOfWork);
        
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