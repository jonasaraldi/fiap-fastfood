namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoFinalizado : StatusDePedido
{
    public PedidoFinalizado() 
        : base(nameof(PedidoFinalizado), "Finalizado", 7)
    {
    }
}