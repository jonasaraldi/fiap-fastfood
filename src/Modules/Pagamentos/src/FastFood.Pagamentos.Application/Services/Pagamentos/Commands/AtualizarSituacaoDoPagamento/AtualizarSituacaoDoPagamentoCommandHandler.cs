using FastFood.Pagamentos.Application.Abstractions;
using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Domain;
using FastFood.Pagamentos.Domain.Exceptions;
using FastFood.Pagamentos.Domain.ValueObjects.Situacao;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Commands.AtualizarSituacaoDoPagamento;

public class AtualizarSituacaoDoPagamentoCommandHandler : ICommandHandler<AtualizarSituacaoDoPagamentoCommand>
{
    private readonly IPagamentoRepository _pagamentoRepository;

    public AtualizarSituacaoDoPagamentoCommandHandler(
        IPagamentoRepository pagamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
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