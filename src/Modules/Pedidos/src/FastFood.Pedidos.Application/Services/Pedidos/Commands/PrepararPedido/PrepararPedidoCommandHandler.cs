using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.PrepararPedido;

public sealed class PrepararPedidoCommandHandler : ICommandHandler<PrepararPedidoCommand, PrepararPedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public PrepararPedidoCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PrepararPedidoResponse> Handle(PrepararPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);

        if (pedido is null)
            throw new PedidoNaoEncontradoDomainException();

        pedido.Preparar();
        
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new(pedido.Id, pedido.Status.Descricao);
    }
}