using FastFood.Catalogo.Application.Abstractions;
using FastFood.Catalogo.Domain.Produtos.Enums;
using FastFood.Catalogo.Domain.Produtos.Repositories;

namespace FastFood.Catalogo.Application.Produtos.Queries.GetProdutosPorCategoria;

public class GetProdutosPorCategoriaQueryHandler 
    : ICommandHandler<GetProdutosPorCategoriaQuery, ProdutosPorCategoriaResponse>
{
    private readonly IProdutoRepository _produtoRepository;

    public GetProdutosPorCategoriaQueryHandler(
        IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    
    public async Task<ProdutosPorCategoriaResponse> Handle(
        GetProdutosPorCategoriaQuery request, CancellationToken cancellationToken)
    {
        var categoria = CategoriaDeProduto.Get(request.Categoria)!;
        var produtos = await _produtoRepository.GetProdutosPorCategoriaAsync(categoria);

        return new ProdutosPorCategoriaResponse(
            produtos
                .Select(p => new ProdutoPorCategoria(
                    p.Nome, p.Descricao, p.Preco, p.Categoria.Nome, p.UrlDaImagem))
                .ToList());
    }
}