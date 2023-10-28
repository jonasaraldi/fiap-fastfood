using FastFood.Pedidos.Application.Abstractions;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Repositories;

namespace FastFood.Pedidos.Application.Services.Pedidos.Commands.CriarPedido;

public sealed class CriarPedidoCommandHandler : ICommandHandler<CriarPedidoCommand, CriarPedidoResponse>
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