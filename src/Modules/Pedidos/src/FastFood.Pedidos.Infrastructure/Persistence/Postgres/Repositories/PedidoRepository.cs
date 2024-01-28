using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Pedidos.Infrastructure.Persistence.Postgres.Repositories;

public class PedidoRepository : IPedidoRespository
{
    private readonly IPedidoDbContext _dbContext;

    public PedidoRepository(IPedidoDbContext dbContext)
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

    public async Task<ICollection<Pedido>> GetConfirmadosDeHojeAsync(CancellationToken cancellationToken) =>
        await _dbContext.Pedidos
            .Include(p => p.Itens)
            .Where(p => p.Status.Equals(StatusDePedido.Confirmado) && p.UpdatedAt >= DateTime.UtcNow.Date && p.Pago)
            .OrderBy(p => p.UpdatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<ICollection<Pedido>> GetPorDataAsync(
        DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken) =>
        await _dbContext.Pedidos
            .Include(p => p.Itens)
            .Include(p => p.Historicos)
            .Where(p => p.CreatedAt >= dataInicial && p.CreatedAt <= dataFinal)
            .OrderByDescending(p => p.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<ICollection<Pedido>> GetPedidosEmOperacaoAsync(CancellationToken cancellationToken)
    {
        StatusDePedido[] statusDePedidosPossiveis =
        {
            StatusDePedido.Recebido, 
            StatusDePedido.EmPreparacao,
            StatusDePedido.Pronto
        };
        
        return await _dbContext.Pedidos
            .Include(p => p.Itens)
            .Where(p => statusDePedidosPossiveis.Contains(p.Status) && p.CreatedAt >= DateTime.UtcNow.Date && p.Pago)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}