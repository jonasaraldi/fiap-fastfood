using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.AtualizarProduto;

public sealed record AtualizarProdutoCommand(
    Ulid Id,
    string Nome,
    string Descricao,
    string Categoria,
    decimal Preco,
    string UrlDaImagem) 
    : ProdutoCommand(Nome, Descricao, Categoria, Preco, UrlDaImagem), ICommand<AtualizarProdutoResponse>
{
}

public sealed record AtualizarProdutoResponse(Ulid Id);
