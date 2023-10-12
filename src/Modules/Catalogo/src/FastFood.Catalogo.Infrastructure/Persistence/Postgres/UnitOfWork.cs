using FastFood.Catalogo.Application.Abstractions.UnitsOfWork;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Contexts;

namespace FastFood.Catalogo.Infrastructure.Persistence.Postgres;

public class UnitOfWork : IUnitOfWork
{
    private readonly ICatalogoDbContext _dbContext;

    public UnitOfWork(ICatalogoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task CommitAsync(CancellationToken cancellationToken) => 
        _dbContext.SaveChangesAsync(cancellationToken);
}