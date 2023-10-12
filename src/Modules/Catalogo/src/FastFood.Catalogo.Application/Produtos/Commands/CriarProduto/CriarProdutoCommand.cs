using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Produtos.Commands.CriarProduto;

public record CriarProdutoCommand(string Nome, string Descricao, string Categoria, decimal Preco, string UrlDaImagem) 
    : ICommand<CriarProdutoResponse>;

public record CriarProdutoResponse(Ulid Id); 