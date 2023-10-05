using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Application.Pedidos.Commands.CriarPedido;

public class CriarPedidoCommandHandler : ICommandHandler<CriarPedidoCommand, CriarPedidoResponse>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;

    public CriarPedidoCommandHandler(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CriarPedidoResponse> Handle(
        CriarPedidoCommand request, CancellationToken cancellationToken)
    {
        Pedido pedido = Pedido.Criar();

        await _pedidoRespository.AddAsync(pedido, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new(pedido.Id);
    }
}