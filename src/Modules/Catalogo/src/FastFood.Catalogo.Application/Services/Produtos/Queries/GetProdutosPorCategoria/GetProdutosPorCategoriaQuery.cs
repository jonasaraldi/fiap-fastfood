using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Services.Produtos.Queries.GetProdutosPorCategoria;

public record GetProdutosPorCategoriaQuery(string Categoria) : ICommand<ProdutosPorCategoriaResponse>
{
}

public record ProdutosPorCategoriaResponse(
    ICollection<ProdutoPorCategoria> Produtos);

public record ProdutoPorCategoria(
    Ulid Id,
    string Nome,
    string Descricao,
    decimal Preco,
    string Categoria,
    string UrlDaImagem);