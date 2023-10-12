using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands.CriarProduto;

public record CriarProdutoCommand(
    string Nome, 
    string Descricao, 
    string Categoria, 
    decimal Preco, 
    string UrlDaImagem) 
    : ProdutoCommand(Nome, Descricao, Categoria, Preco, UrlDaImagem), ICommand<CriarProdutoResponse>
{
}

public record CriarProdutoResponse(Ulid Id); 