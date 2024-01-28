using FastFood.Pagamentos.Application.Abstractions;
using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Application.Gateways.UnitOfWorks;
using FastFood.Pagamentos.Domain;
using FastFood.Pagamentos.Domain.Exceptions;
using FastFood.Pagamentos.Domain.ValueObjects.Situacao;
using MediatR;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Commands.AtualizarSituacaoDoPagamento;

public class AtualizarSituacaoDoPagamentoCommandHandler : ICommandHandler<AtualizarSituacaoDoPagamentoCommand>
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AtualizarSituacaoDoPagamentoCommandHandler(
        IPagamentoRepository pagamentoRepository,
        IUnitOfWork unitOfWork)
    {
        _pagamentoRepository = pagamentoRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(
        AtualizarSituacaoDoPagamentoCommand request, CancellationToken cancellationToken)
    {
        Pagamento? pagamento = await _pagamentoRepository.GetByPedidoIdAsync(request.PedidoId, cancellationToken);
        if (pagamento is null)
        {
            throw new PagamentoNaoEncontradoDomainException();
        }
        
        AtualizarSituacao(request.CodigoDaSituacao, pagamento);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private void AtualizarSituacao(string codigo, Pagamento pagamento)
    {
        switch (codigo)
        {
            case "approved": pagamento.MarcarComoAprovado(); break;
            case "rejected": pagamento.MarcarComoReprovado(); break;
        }
    }
}