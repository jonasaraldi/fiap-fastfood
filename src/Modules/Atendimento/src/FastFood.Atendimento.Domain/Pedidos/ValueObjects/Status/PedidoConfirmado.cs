namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoConfirmado : StatusDePedido
{
    public PedidoConfirmado() : base("Confirmado")
    {
    }
    
    public override void Receber(Pedido pedido) => 
        pedido.SetStatus(new PedidoRecebido());
}