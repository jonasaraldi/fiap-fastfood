using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Domain;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly IPagamentoDbContext _pagamentoDbContext;

    public PagamentoRepository(
        IPagamentoDbContext pagamentoDbContext)
    {
        _pagamentoDbContext = pagamentoDbContext;
    }
    
    public async Task AddAsync(Pagamento pagamento, CancellationToken cancellationToken) => 
        await _pagamentoDbContext.Pagamentos
            .AddAsync(pagamento, cancellationToken);

    public Task<Pagamento?> GetByPedidoIdAsync(Ulid pedidoId, CancellationToken cancellationToken) =>
        _pagamentoDbContext.Pagamentos
            .FirstOrDefaultAsync(p => p.PedidoId.Equals(pedidoId), cancellationToken);

    public void Update(Pagamento pagamento) => 
        _pagamentoDbContext.Pagamentos
            .Update(pagamento);
}