using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Atendimento.Application.Services.Pedidos.Commands.RemoverItemDePedido;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;

namespace FastFood.Atendimento.Tests.Application.Pedidos.Commands;

public class RemoverItemDePedidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeveRemoverItemDoPedido()
    {
        IPedidoRespository respository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
     
        Pedido pedido = GetPedidoComUmItem();
        await respository.AddAsync(pedido, CancellationToken.None);
        
        RemoverItemDePedidoCommandHandler handler = new(respository, unitOfWork);

        Ulid itemDePedidoId = pedido.Itens.First().Id;
        RemoverItemDePedidoCommand command = new(pedido.Id, itemDePedidoId);
        var response = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(pedido.Id, response.PedidoId);
        Assert.Equal(itemDePedidoId, response.ItemDePedidoId);
        Assert.Equal(0, response.ValorTotal);
    }
    
    [Fact]
    public async Task Handle_NaoDeveRemoverItemDePedido_QuandoPedidoNaoForEncontrado()
    {
        IPedidoRespository respository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
     
        Pedido pedido = GetPedidoComUmItem();
        await respository.AddAsync(pedido, CancellationToken.None);
        
        RemoverItemDePedidoCommandHandler handler = new(respository, unitOfWork);

        Ulid pedidoIdAleatorio = Ulid.NewUlid();
        Ulid itemDePedidoId = pedido.Itens.First().Id;
        RemoverItemDePedidoCommand command = new(pedidoIdAleatorio, itemDePedidoId);

        await Assert.ThrowsAsync<PedidoNaoEncontradoDomainException>(async () => 
            await handler.Handle(command, CancellationToken.None));
    }
    
    [Fact]
    public async Task Handle_DeveRemoverUmDosItensDoPedidoEAtualizarTotal()
    {
        IPedidoRespository respository = new PedidoRepositoryInMemory();
        IUnitOfWork unitOfWork = new UnitOfWorkInMemory();
     
        Pedido pedido = GetPedidoComDoisItens();
        await respository.AddAsync(pedido, CancellationToken.None);
        
        RemoverItemDePedidoCommandHandler handler = new(respository, unitOfWork);

        Ulid itemDePedidoId = pedido.Itens.First().Id;
        RemoverItemDePedidoCommand command = new(pedido.Id, itemDePedidoId);
        var response = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(pedido.Id, response.PedidoId);
        Assert.Equal(itemDePedidoId, response.ItemDePedidoId);
        Assert.Equal(pedido.ValorTotal, response.ValorTotal);
    }

    private Pedido GetPedidoComUmItem()
    {
        Pedido pedido = Pedido.Criar();
        pedido.AdicionarItem(
            ItemDePedido.Criar("Hamburguer", "Pão, carne e queijo", Dinheiro.Criar(20), 1));
        
        return pedido;
    }
    
    private Pedido GetPedidoComDoisItens()
    {
        Pedido pedido = Pedido.Criar();
        pedido.AdicionarItem(
            ItemDePedido.Criar("Hamburguer", "Pão, carne e queijo", Dinheiro.Criar(20), 1));
        
        pedido.AdicionarItem(
            ItemDePedido.Criar("Coca-cola", "Bebida gaseificada", Dinheiro.Criar(5), 2));
        
        return pedido;
    }
}