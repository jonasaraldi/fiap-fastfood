using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.CriarProduto;

public sealed record AdicionarProdutoCommand(
    string Nome,
    string Descricao,
    string Categoria,
    decimal Preco,
    string UrlDaImagem) 
    : ProdutoCommand(Nome, Descricao, Categoria, Preco, UrlDaImagem), ICommand<AdicionarProdutoResponse>
{
}

public record AdicionarProdutoResponse(Ulid Id); 