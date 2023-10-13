using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.EditarProduto;

public sealed record EditarProdutoCommand(
    Ulid Id,
    string Nome,
    string Descricao,
    string Categoria,
    decimal Preco,
    string UrlDaImagem) 
    : ProdutoCommand(Nome, Descricao, Categoria, Preco, UrlDaImagem), ICommand<EditarProdutoResponse>
{
}

public sealed record EditarProdutoResponse(Ulid Id);
