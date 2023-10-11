using FastFood.SharedKernel.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.Exceptions;

public sealed class CpfInvalidoDomainException : InvalidOperationDomainException
{
    public CpfInvalidoDomainException() 
        : base("CPF inválido")
    {
    }
}