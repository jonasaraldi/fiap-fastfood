using FastFood.Pagamentos.Application.Gateways.UnitOfWorks;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Contexts;

namespace FastFood.Pagamentos.Infrastructure.Persistence.Postgres;

public class UnitOfWork : IUnitOfWork
{
    private readonly IPagamentoDbContext _pagamentoDbContext;

    public UnitOfWork(IPagamentoDbContext pagamentoDbContext)
    {
        _pagamentoDbContext = pagamentoDbContext;
    }
    
    public Task CommitAsync(CancellationToken cancellationToken) => 
        _pagamentoDbContext.SaveChangesAsync();
}