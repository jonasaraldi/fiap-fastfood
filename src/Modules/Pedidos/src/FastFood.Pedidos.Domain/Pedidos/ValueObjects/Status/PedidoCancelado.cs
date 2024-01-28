namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoCancelado : StatusDePedido
{
    public PedidoCancelado() 
        : base(nameof(PedidoCancelado), "Cancelado", 2)
    {
    }
}