namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoRecebido : StatusDePedido
{
    public PedidoRecebido() 
        : base(nameof(PedidoRecebido), "Recebido", 4)
    {
    }

    public override void Preparar(Pedido pedido)
    {
        pedido.SetStatus(new PedidoEmPreparacao());
    }
}