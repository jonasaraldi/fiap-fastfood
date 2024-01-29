using FastFood.Pagamentos.Application.Abstractions;
using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Application.Gateways.UnitOfWorks;
using FastFood.Pagamentos.Domain;
using FastFood.Pagamentos.Domain.Exceptions;
using MediatR;

namespace FastFood.Pagamentos.Application.UseCases.Pagamentos.Commands.AtualizarSituacaoDoPagamento;

public class AtualizarSituacaoDoPagamentoCommandHandler : ICommandHandler<AtualizarSituacaoDoPagamentoCommand>
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public AtualizarSituacaoDoPagamentoCommandHandler(
        IPagamentoRepository pagamentoRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _pagamentoRepository = pagamentoRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
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
        
        var publishTasks = pagamento.GetDomainEvents()
            .Select(domainEvent => _publisher.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(publishTasks);
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