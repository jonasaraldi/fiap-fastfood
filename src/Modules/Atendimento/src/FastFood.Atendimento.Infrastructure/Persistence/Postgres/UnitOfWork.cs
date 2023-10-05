using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Contexts;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAtendimentoDbContext _atendimentoDbContext;

    public UnitOfWork(IAtendimentoDbContext atendimentoDbContext)
    {
        _atendimentoDbContext = atendimentoDbContext;
    }

    public Task CommitAsync() => 
        _atendimentoDbContext.SaveChangesAsync();
}