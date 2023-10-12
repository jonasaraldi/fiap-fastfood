using FastFood.Contracts.Abstractions;

namespace FastFood.Contracts.Produtos;

public static class DomainEvents
{
    public record ProdutoCriadoDomainEvent(Ulid Id, string Nome, string Descricao, string Categoria, decimal Preco, string UrlDaImagem) : DomainEvent;
}