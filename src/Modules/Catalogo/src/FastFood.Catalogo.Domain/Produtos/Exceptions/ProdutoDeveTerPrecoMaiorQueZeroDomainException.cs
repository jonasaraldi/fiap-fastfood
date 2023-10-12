using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Catalogo.Domain.Produtos.Exceptions;

public sealed class ProdutoDeveTerPrecoMaiorQueZeroDomainException : InvalidOperationDomainException
{
    public ProdutoDeveTerPrecoMaiorQueZeroDomainException() 
        : base("O preço do produto deve ser maior que zero")
    {
    }
}