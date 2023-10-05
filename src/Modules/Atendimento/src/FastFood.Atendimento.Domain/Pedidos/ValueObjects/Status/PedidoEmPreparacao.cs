namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoEmPreparacao : StatusDePedido
{
    public PedidoEmPreparacao() 
        : base(nameof(PedidoEmPreparacao), "Em Preparação")
    {
    }
    
    public override void Pronto(Pedido pedido)
    {
        pedido.SetStatus(new PedidoPronto());
    }
}