using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.Repositories;

namespace FastFood.Atendimento.Application.Pedidos.Commands.ConfirmarPedido;

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
        
        return new ConfirmarPedidoResponse(
            pedido.Id, pedido.Status.Descricao, pedido.ValorTotal);
    }
}