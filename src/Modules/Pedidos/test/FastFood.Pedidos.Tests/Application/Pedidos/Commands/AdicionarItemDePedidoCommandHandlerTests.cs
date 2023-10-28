using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Pedidos.Application.Services.Pedidos.Commands.AdicionarItemDePedido;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Domain.Pedidos.Repositories.InMemory;

namespace FastFood.Pedidos.Tests.Application.Pedidos.Commands;

public class AdicionarItemDePedidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveAdicionarItemDePedido()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        
        var pedido = Pedido.Criar();
        await repository.AddAsync(pedido, CancellationToken.None);
        AdicionarItemDePedidoCommandHandler handler = new(repository, unitOfWork);

        AdicionarItemDePedidoCommand command = new(
            pedido.Id, "Hamburguer", "Pão, carne e queijo", 20m, 1);
        
        var response = await handler.Handle(command, CancellationToken.None);

        Pedido pedidoSalvo = (await repository.GetByIdAsync(response.PedidoId, CancellationToken.None))!;
        ItemDePedido itemDePedido = pedidoSalvo.Itens.FirstOrDefault()!;
        
        Assert.Equal(pedido.Id, response.PedidoId);
        Assert.Equal(pedido.ValorTotal, response.ValorTotal);
        Assert.NotNull(itemDePedido);
        Assert.Equal(response.ItemDePedidoId, itemDePedido.Id);
        Assert.Equal(command.Nome, itemDePedido.Nome);
        Assert.Equal(command.Descricao, itemDePedido.Descricao);
        Assert.Equal(command.Preco, itemDePedido.Preco.Valor);
        Assert.Equal(command.Quantidade, itemDePedido.Quantidade);
        Assert.Equal(command.PedidoId, itemDePedido.PedidoId);
    }

    [Fact]
    public async Task Handle_NaoDeveAdicionarItemDePedido_QuandoPedidoNaoForEncontrado()
    {
        IPedidoRespository repository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
        
        var pedido = Pedido.Criar();
        await repository.AddAsync(pedido, CancellationToken.None);
        AdicionarItemDePedidoCommandHandler handler = new(repository, unitOfWork);

        Ulid pedidoIdAleatorio = Ulid.NewUlid();
        AdicionarItemDePedidoCommand command = new(
            pedidoIdAleatorio, "Hamburguer", "Pão, carne e queijo", 20m, 1);
        
        await Assert.ThrowsAsync<PedidoNaoEncontradoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }
}