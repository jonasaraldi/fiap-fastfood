namespace FastFood.Catalogo.Application.Services.Produtos.Commands;

public abstract record ProdutoCommand(
    string Nome,
    string Descricao,
    string Categoria,
    decimal Preco,
    string UrlDaImagem)
{
}