using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Contexts;

namespace FastFood.Pedidos.Infrastructure.Persistence.Postgres;

public class UnitOfWork : IUnitOfWork
{
    private readonly IPedidoDbContext _pedidoDbContext;

    public UnitOfWork(IPedidoDbContext pedidoDbContext)
    {
        _pedidoDbContext = pedidoDbContext;
    }

    public Task CommitAsync(CancellationToken cancellationToken) => 
        _pedidoDbContext.SaveChangesAsync(cancellationToken);
}