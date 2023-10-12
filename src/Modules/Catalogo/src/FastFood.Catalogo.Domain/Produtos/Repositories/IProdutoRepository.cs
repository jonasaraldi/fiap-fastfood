namespace FastFood.Catalogo.Domain.Produtos.Repositories;

public interface IProdutoRepository
{
    Task AddAsync(Produto produto, CancellationToken cancellationToken);
}