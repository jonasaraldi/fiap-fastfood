using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Pagamentos.Domain.Exceptions;

public class PagamentoNaoEncontradoDomainException : NotFoundDomainException
{
    public PagamentoNaoEncontradoDomainException() 
        : base("Pagamento não encontrado.")
    {
    }
}