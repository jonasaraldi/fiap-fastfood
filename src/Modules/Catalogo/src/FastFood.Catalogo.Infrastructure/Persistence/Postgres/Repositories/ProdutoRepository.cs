using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Repositories;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Contexts;

namespace FastFood.Catalogo.Infrastructure.Persistence.Postgres.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ICatalogoDbContext _dbContext;

    public ProdutoRepository(ICatalogoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Produto produto, CancellationToken cancellationToken) =>
        await _dbContext.Produtos
            .AddAsync(produto, cancellationToken);
}