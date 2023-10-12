namespace FastFood.Catalogo.Application.Services.Produtos.Commands.AtualizarProduto;

public record AtualizarProdutoCommand(
    Ulid Id,
    string Nome, 
    string Descricao, 
    string Categoria, 
    decimal Preco, 
    string UrlDaImagem) 
    : ProdutoCommand(Nome, Descricao, Categoria, Preco, UrlDaImagem)
{
}
