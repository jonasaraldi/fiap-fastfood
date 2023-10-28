using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.RemoverItemDePedido;

public sealed class RemoverItemDePedidoCommandHandler : ICommandHandler<RemoverItemDePedidoCommand, RemoverItemDePedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoverItemDePedidoCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<RemoverItemDePedidoResponse> Handle(
        RemoverItemDePedidoCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);
        
        if (pedido is null)
            throw new PedidoNaoEncontradoDomainException();
        
        pedido.RemoverItem(request.ItemDePedidoId);
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new(request.PedidoId, request.ItemDePedidoId, pedido.ValorTotal);
    }
}