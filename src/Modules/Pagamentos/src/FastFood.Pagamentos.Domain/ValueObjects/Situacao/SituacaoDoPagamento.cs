using FastFood.Pagamentos.Domain.Exceptions;

namespace FastFood.Pagamentos.Domain.ValueObjects.Situacao;

public abstract class SituacaoDoPagamento
{
    public static SituacaoDoPagamento Pendente = new PagamentoPendente();
    public static SituacaoDoPagamento Aprovado = new PagamentoAprovado();
    public static SituacaoDoPagamento Recusado = new PagamentoRecusado();
    
    public SituacaoDoPagamento(string codigo, string descricao)
    {
        Codigo = codigo;
        Descricao = descricao;
    }

    public string Codigo { get; private set; }
    public string Descricao { get; private set; }

    public virtual void Aprovar(Pagamento pagamento) => RetornarErro(pagamento, Aprovado);
    public virtual void Recusar(Pagamento pagamento) => RetornarErro(pagamento, Recusado);
    private void RetornarErro(Pagamento pagamento, SituacaoDoPagamento situacao) => 
        throw new TrocaDeSituacaoInvalidaDomainException(pagamento.Situacao, situacao);
}