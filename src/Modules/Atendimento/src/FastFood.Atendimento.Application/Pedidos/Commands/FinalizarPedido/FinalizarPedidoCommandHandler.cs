using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;

namespace FastFood.Atendimento.Application.Pedidos.Commands.FinalizarPedido;

public class FinalizarPedidoCommandHandler : ICommandHandler<FinalizarPedidoCommand, FinalizarPedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public FinalizarPedidoCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<FinalizarPedidoResponse> Handle(
        FinalizarPedidoCommand request, CancellationToken cancellationToken)
    {
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(request.PedidoId, cancellationToken);
        if (pedido is null)
            throw new PedidoNaoEncontradoDomainException();

        pedido.Finalizar();
        
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new(pedido.Id, pedido.Status.Descricao);
    }
}