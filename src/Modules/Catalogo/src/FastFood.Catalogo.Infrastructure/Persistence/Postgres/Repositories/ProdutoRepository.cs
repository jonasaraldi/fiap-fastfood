using FastFood.Catalogo.Domain.Produtos;
using FastFood.Catalogo.Domain.Produtos.Enums;
using FastFood.Catalogo.Domain.Produtos.Repositories;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Contexts;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ICollection<Produto>> GetProdutosPorCategoriaAsync(CategoriaDeProduto categoria) =>
        await _dbContext.Produtos
            .Where(p => p.Categoria.Equals(categoria))
            .OrderByDescending(p => p.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public Task<Produto?> GetByIdAsync(Ulid id, CancellationToken cancellationToken) => 
        _dbContext.Produtos.FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

    public void Update(Produto produto) => 
        _dbContext.Produtos.Update(produto);
}