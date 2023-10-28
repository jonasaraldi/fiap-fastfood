namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoEmPreparacao : StatusDePedido
{
    public PedidoEmPreparacao() 
        : base(nameof(PedidoEmPreparacao), "Em Preparação")
    {
    }
    
    public override void MarcarComoPronto(Pedido pedido)
    {
        pedido.SetStatus(new PedidoPronto());
    }
}