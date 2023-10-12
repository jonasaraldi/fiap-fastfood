using FastFood.Catalogo.Application.Abstractions;
using FastFood.Catalogo.Domain.Produtos.Enums;

namespace FastFood.Catalogo.Application.Produtos.Queries.GetCategorias;

public class GetCategoriasQueryHandler : ICommandHandler<GetCategoriasQuery, GetCategoriasResponse>
{
    public async Task<GetCategoriasResponse> Handle(
        GetCategoriasQuery request, CancellationToken cancellationToken)
    {
        var categorias = CategoriaDeProduto.GetAll();
        
        return new GetCategoriasResponse(categorias
            .Select(c => new CategoriaResponse(c.Codigo, c.Nome))
            .ToList());
    }
}