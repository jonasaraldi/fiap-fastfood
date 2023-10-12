using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Produtos.Queries.GetCategorias;

public record GetCategoriasQuery() : ICommand<GetCategoriasResponse>
{
}

public record GetCategoriasResponse(ICollection<CategoriaResponse> Categorias);
public record CategoriaResponse(string Codigo, string Descricao);