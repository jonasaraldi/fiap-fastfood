using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.CancelarPedido;

public class CancelarPedidoCommandHandler : ICommandHandler<CancelarPedidoCommand, CancelarPedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelarPedidoCommandHandler(
        IPedidoRespository pedidoRespository, 
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CancelarPedidoResponse> Handle(
        CancelarPedidoCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);

        if (pedido is null)
            throw new PedidoNaoEncontradoDomainException();
        
        pedido.Cancelar();
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new(pedido.Id, pedido.Status.Descricao);
    }
}