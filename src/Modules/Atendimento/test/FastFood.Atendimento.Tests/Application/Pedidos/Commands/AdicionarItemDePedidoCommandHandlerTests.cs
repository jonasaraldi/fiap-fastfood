using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork.InMemory;
using FastFood.Atendimento.Application.Pedidos.Commands.AdicionarItemDePedido;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Domain.Pedidos.Repositories.InMemory;

namespace FastFood.Atendimento.Tests.Application.Pedidos.Commands;

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
        Assert.NotEqual(Ulid.Empty, response.Id);
        Assert.NotNull(itemDePedido);
        Assert.Equal(response.Id, itemDePedido.Id);
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