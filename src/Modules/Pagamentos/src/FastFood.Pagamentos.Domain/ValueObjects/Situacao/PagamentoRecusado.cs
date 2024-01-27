namespace FastFood.Pagamentos.Domain.ValueObjects.Situacao;

public class PagamentoRecusado : SituacaoDoPagamento
{
    public PagamentoRecusado() 
        : base(nameof(PagamentoRecusado), "Recusado")
    {
    }
}