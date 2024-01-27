using FastFood.Contracts.Abstractions;
using FastFood.Contracts.Pagamentos;
using FastFood.Pagamentos.Domain.ValueObjects.Situacao;

namespace FastFood.Pagamentos.Domain;

public sealed class Pagamento : AggregateRoot
{
    private Pagamento(Ulid pedidoId)
    {
        PedidoId = pedidoId;
        Situacao = SituacaoDoPagamento.Pendente;
        RaiseDomainEvent(new DomainEvents.PagamentoCriado(pedidoId));
    }

    public Ulid PedidoId { get; set; }
    public SituacaoDoPagamento Situacao { get; set; }

    public void MarcarComoAprovado()
    {
        Situacao.Aprovar(this);
        RaiseDomainEvent(new DomainEvents.PagamentoAprovado(PedidoId));
    }
    
    public void MarcarComoReprovado()
    {
        Situacao.Recusar(this);
        RaiseDomainEvent(new DomainEvents.PagamentoRecusado(PedidoId));
    }

    internal void SetSituacao(SituacaoDoPagamento situacao)
    {
        Situacao = situacao;
    }
    
    public static Pagamento Criar(Ulid pedidoId) => new(pedidoId);
}