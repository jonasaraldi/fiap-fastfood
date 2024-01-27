namespace FastFood.Pagamentos.Domain.ValueObjects.Situacao;

public class PagamentoPendente : SituacaoDoPagamento
{
    public PagamentoPendente() 
        : base(nameof(PagamentoPendente), "Pendente")
    {
    }

    public override void Aprovar(Pagamento pagamento) => 
        pagamento.SetSituacao(Aprovado);

    public override void Recusar(Pagamento pagamento) => 
        pagamento.SetSituacao(Recusado);
}