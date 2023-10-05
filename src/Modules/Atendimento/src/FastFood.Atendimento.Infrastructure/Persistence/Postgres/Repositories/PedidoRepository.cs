using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Contexts;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Repositories;

public class PedidoRepository : IPedidoRespository
{
    private readonly IAtendimentoDbContext _atendimentoDbContext;

    public PedidoRepository(IAtendimentoDbContext atendimentoDbContext)
    {
        _atendimentoDbContext = atendimentoDbContext;
    }
    
    public async Task AddAsync(Pedido pedido, CancellationToken cancellationToken) => 
        await _atendimentoDbContext.Pedidos.AddAsync(pedido, cancellationToken);
}