using FastFood.Pagamentos.Application.Abstractions;
using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Domain;
using FastFood.Pagamentos.Domain.Exceptions;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Queries.GetSituacaoDoPagamento;

public class GetSituacaoDoPagamentoQueryHandler : ICommandHandler<GetSituacaoDoPagamentoQuery, GetPagamentosResponse>
{
    private readonly IPagamentoRepository _pagamentoRepository;

    public GetSituacaoDoPagamentoQueryHandler(
        IPagamentoRepository pagamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
    }
    
    public async Task<GetPagamentosResponse> Handle(
        GetSituacaoDoPagamentoQuery request, CancellationToken cancellationToken)
    {
        Pagamento? pagamento = await _pagamentoRepository.GetByPedidoIdAsync(request.PedidoId, cancellationToken);
        if (pagamento is null)
        {
            throw new PagamentoNaoEncontradoDomainException();
        }

        return new(
            pagamento.PedidoId, 
            pagamento.Situacao.Descricao);
    }
}