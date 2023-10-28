using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public sealed class CpfInvalidoDomainException : InvalidOperationDomainException
{
    public CpfInvalidoDomainException() 
        : base("CPF inválido")
    {
    }
}