using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.AdicionarItemDePedido;

public sealed class AdicionarItemDePedidoCommandHandler : ICommandHandler<AdicionarItemDePedidoCommand, AdicionarItemDePedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarItemDePedidoCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AdicionarItemDePedidoResponse> Handle(
        AdicionarItemDePedidoCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);

        if (pedido is null) 
            throw new PedidoNaoEncontradoDomainException();

        ItemDePedido item = ItemDePedido.Criar(
            request.Nome, 
            request.Descricao, 
            Dinheiro.Criar(request.Preco),
            request.Quantidade,
            request.Observacao);
        
        pedido.AdicionarItem(item);

        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new(pedido.Id, item.Id, pedido.ValorTotal);
    }
}