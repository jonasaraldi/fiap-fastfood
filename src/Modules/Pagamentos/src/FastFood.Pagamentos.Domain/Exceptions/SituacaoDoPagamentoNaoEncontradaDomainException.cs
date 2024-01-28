using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Pagamentos.Domain.Exceptions;

public class SituacaoDoPagamentoNaoEncontradaDomainException : DomainException
{
    public SituacaoDoPagamentoNaoEncontradaDomainException(string codigo) 
        : base($"Situação de pagamento não encontrada ({ codigo }).")
    {
    }
}