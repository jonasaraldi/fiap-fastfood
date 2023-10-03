namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoPronto : StatusDePedido
{
    public PedidoPronto() : base("Pronto")
    {
    }
    
    public override void Finalizar(Pedido pedido)
    {
        pedido.SetStatus(new PedidoFinalizado());
    }
}