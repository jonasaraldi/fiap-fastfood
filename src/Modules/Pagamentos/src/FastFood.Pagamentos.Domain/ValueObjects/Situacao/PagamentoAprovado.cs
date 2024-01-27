namespace FastFood.Pagamentos.Domain.ValueObjects.Situacao;

public class PagamentoAprovado : SituacaoDoPagamento
{
    public PagamentoAprovado() 
        : base(nameof(PagamentoAprovado), "Aprovado")
    {
    }
}