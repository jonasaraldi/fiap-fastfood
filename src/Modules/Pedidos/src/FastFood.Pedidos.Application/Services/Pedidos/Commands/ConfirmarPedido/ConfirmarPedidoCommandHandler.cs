using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.ConfirmarPedido;

public sealed class ConfirmarPedidoCommandHandler : ICommandHandler<ConfirmarPedidoCommand, ConfirmarPedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmarPedidoCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ConfirmarPedidoResponse> Handle(
        ConfirmarPedidoCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);

        if (pedido is null)
            throw new PedidoNaoEncontradoDomainException();
        
        pedido.Confirmar();
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new(pedido.Id, pedido.Status.Descricao, pedido.ValorTotal);
    }
}