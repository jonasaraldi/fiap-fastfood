using FastFood.Pagamentos.Application.Abstractions;
using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Application.Gateways.UnitOfWorks;
using FastFood.Pagamentos.Domain;
using MediatR;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Commands.RealizarPagamento;

public sealed class RealizarPagamentoCommandHandler : ICommandHandler<RealizarPagamentoCommand>
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RealizarPagamentoCommandHandler(
        IPagamentoRepository pagamentoRepository,
        IUnitOfWork unitOfWork)
    {
        _pagamentoRepository = pagamentoRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(RealizarPagamentoCommand request, CancellationToken cancellationToken)
    {
        Pagamento pagamento = Pagamento.Criar(request.PedidoId);
        
        await _pagamentoRepository.AddAsync(pagamento, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}