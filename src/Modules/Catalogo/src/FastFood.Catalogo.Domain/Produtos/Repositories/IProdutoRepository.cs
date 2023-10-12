using FastFood.Catalogo.Domain.Produtos.Enums;

namespace FastFood.Catalogo.Domain.Produtos.Repositories;

public interface IProdutoRepository
{
    Task AddAsync(Produto produto, CancellationToken cancellationToken);
    Task<ICollection<Produto>> GetProdutosPorCategoriaAsync(CategoriaDeProduto categoria);
    Task<Produto?> GetByIdAsync(Ulid id, CancellationToken cancellationToken);
    void Update(Produto produto);
    void Remove(Produto produto);
}