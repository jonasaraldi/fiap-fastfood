namespace FastFood.Catalogo.Endpoints.Models;

public record AtualizarProdutoRequest(
    string Nome, 
    string Descricao, 
    string Categoria, 
    decimal Preco, 
    string UrlDaImagem);