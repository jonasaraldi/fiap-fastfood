namespace FastFood.Catalogo.Endpoints.Models;

public record ProdutoRequest(
    string Nome, 
    string Descricao, 
    string Categoria, 
    decimal Preco, 
    string UrlDaImagem);