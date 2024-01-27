using FastFood.Pagamentos.Domain;

namespace FastFood.Pagamentos.Application.Gateways.Repositories;

public interface IPagamentoRepository
{
    Task AddAsync(Pagamento pagamento, CancellationToken cancellationToken);
    Task<Pagamento?> GetByPedidoIdAsync(Ulid pedidoId, CancellationToken cancellationToken);
    void Update(Pagamento pagamento);
}