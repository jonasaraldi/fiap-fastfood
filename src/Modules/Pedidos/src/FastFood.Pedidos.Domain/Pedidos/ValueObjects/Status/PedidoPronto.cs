namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoPronto : StatusDePedido
{
    public PedidoPronto() 
        : base(nameof(PedidoPronto), "Pronto")
    {
    }
    
    public override void Finalizar(Pedido pedido)
    {
        pedido.SetStatus(new PedidoFinalizado());
    }
}