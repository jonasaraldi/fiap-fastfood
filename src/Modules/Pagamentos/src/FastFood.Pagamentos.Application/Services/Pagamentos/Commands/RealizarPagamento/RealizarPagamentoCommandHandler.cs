using FastFood.Pagamentos.Application.Abstractions;
using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Domain;

namespace FastFood.Pagamentos.Application.Services.Pagamentos.Commands.RealizarPagamento;

public sealed class RealizarPagamentoCommandHandler : ICommandHandler<RealizarPagamentoCommand>
{
    private readonly IPagamentoRepository _pagamentoRepository;

    public RealizarPagamentoCommandHandler(
        IPagamentoRepository pagamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
    }
    
    public Task Handle(RealizarPagamentoCommand request, CancellationToken cancellationToken)
    {
        var pagamento = Pagamento.Criar(request.PedidoId);
        return _pagamentoRepository.AddAsync(pagamento, cancellationToken);
    }
}