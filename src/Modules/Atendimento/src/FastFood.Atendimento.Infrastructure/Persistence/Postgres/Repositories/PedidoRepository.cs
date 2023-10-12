using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Repositories;

public class PedidoRepository : IPedidoRespository
{
    private readonly IAtendimentoDbContext _dbContext;

    public PedidoRepository(IAtendimentoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Pedido pedido, CancellationToken cancellationToken) => 
        await _dbContext.Pedidos
            .AddAsync(pedido, cancellationToken);

    public Task<Pedido?> GetByIdAsync(Ulid id, CancellationToken cancellationToken) =>
        _dbContext.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

    public void Update(Pedido pedido) =>
        _dbContext.Pedidos.Update(pedido);
}