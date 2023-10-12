using FastFood.Catalogo.Application.Abstractions;

namespace FastFood.Catalogo.Application.Services.Produtos.Commands;

public record ProdutoCommand(
    string Nome,
    string Descricao,
    string Categoria,
    decimal Preco,
    string UrlDaImagem) : ICommand
{
}