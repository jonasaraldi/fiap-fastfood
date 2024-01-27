using FastFood.Contracts.Abstractions;
using FastFood.Pagamentos.Domain.ValueObjects.Situacao;

namespace FastFood.Pagamentos.Domain;

public sealed class Pagamento : AggregateRoot
{
    private Pagamento(Ulid pedidoId)
    {
        PedidoId = pedidoId;
        Situacao = SituacaoDoPagamento.Pendente;
    }

    public Ulid PedidoId { get; set; }
    public SituacaoDoPagamento Situacao { get; set; }

    public void MarcarComoAprovado()
    {
        Situacao.Aprovar(this);
    }
    
    public void MarcarComoReprovado()
    {
        Situacao.Recusar(this);
    }

    internal void SetSituacao(SituacaoDoPagamento situacao)
    {
        Situacao = situacao;
    }
    
    public static void Criar(Ulid pedidoId) => 
        new Pagamento(pedidoId);
}