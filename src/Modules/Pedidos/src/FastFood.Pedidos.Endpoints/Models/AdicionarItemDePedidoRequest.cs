namespace FastFood.Pedidos.Endpoints.Models;

public record AdicionarItemDePedidoRequest(
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade,
    string? Observacao)
{
}