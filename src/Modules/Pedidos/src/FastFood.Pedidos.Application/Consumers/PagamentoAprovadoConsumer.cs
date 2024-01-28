using FastFood.Contracts.Abstractions;
using FastFood.Contracts.Pagamentos;
using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FastFood.Pedidos.Application.Consumers;

public class PagamentoAprovadoConsumer : INotificationHandler<DomainEvents.PagamentoAprovado>
{
    private readonly IPedidoRespository _pedidoRespository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PagamentoAprovadoConsumer> _logger;

    public PagamentoAprovadoConsumer(
        IPedidoRespository pedidoRespository,
        IUnitOfWork unitOfWork,
        ILogger<PagamentoAprovadoConsumer> logger)
    {
        _pedidoRespository = pedidoRespository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DomainEvents.PagamentoAprovado notification, CancellationToken cancellationToken)
    {
        Ulid pedidoId = notification.PedidoId;
        Pedido? pedido = await _pedidoRespository.GetByIdAsync(pedidoId, cancellationToken);
        
        if (pedido is null)
        {
            _logger.LogError("Pedido não foi encontrado ({PedidoId}).", pedidoId);
            throw new PedidoNaoEncontradoDomainException();
        }

        pedido.MarcarComoPago();
        _pedidoRespository.Update(pedido);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        _logger.LogInformation("Pedido {CodigoDoPedido} foi pago com sucesso ({PedidoId}).", pedido.Codigo, pedidoId);
    }
}
















