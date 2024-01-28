namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoConfirmado : StatusDePedido
{
    public PedidoConfirmado() 
        : base(nameof(PedidoConfirmado), "Confirmado", 3)
    {
    }
    
    public override void Receber(Pedido pedido) => 
        pedido.SetStatus(new PedidoRecebido());
}