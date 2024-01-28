using FastFood.Pagamentos.Domain.Exceptions;
using FastFood.Pagamentos.Domain.ValueObjects.Situacao;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Configs.Converters;

public class SituacaoDoPagamentoToStringConverter : ValueConverter<SituacaoDoPagamento, string>
{
    public SituacaoDoPagamentoToStringConverter()
        : base(
            convertToProviderExpression: statusDePedido => statusDePedido.Codigo,
            convertFromProviderExpression: codigo => GetByCodigo(codigo))
    {   
    }
    
    private static SituacaoDoPagamento GetByCodigo(string codigo) =>
        codigo switch
        {
            nameof(PagamentoPendente) => new PagamentoPendente(),
            nameof(PagamentoAprovado) => new PagamentoAprovado(),
            nameof(PagamentoRecusado) => new PagamentoRecusado(),
            _ => throw new SituacaoDoPagamentoNaoEncontradaDomainException(codigo)
        };
}