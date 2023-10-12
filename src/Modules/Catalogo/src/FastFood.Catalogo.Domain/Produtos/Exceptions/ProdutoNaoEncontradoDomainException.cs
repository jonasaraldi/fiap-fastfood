using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Catalogo.Domain.Produtos.Exceptions;

public sealed class ProdutoNaoEncontradoDomainException : NotFoundDomainException
{
    public ProdutoNaoEncontradoDomainException() 
        : base("Produto não encontrado")
    {
    }
}