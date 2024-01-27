using FastFood.Contracts.Abstractions.Exceptions;
using FastFood.Pagamentos.Domain.ValueObjects.Situacao;

namespace FastFood.Pagamentos.Domain.Exceptions;

public class TrocaDeSituacaoInvalidaDomainException : DomainException
{
    public TrocaDeSituacaoInvalidaDomainException(SituacaoDoPagamento atual, SituacaoDoPagamento informado) 
        : base($"Situação do pagamento não pode ser alterado de { atual.Descricao.ToLower() } para { informado.Descricao.ToLower() }.")
    {
    }
}