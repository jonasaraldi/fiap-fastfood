namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoRecebido : StatusDePedido
{
    public PedidoRecebido() : base("Recebido")
    {
    }

    public override void Preparar(Pedido pedido)
    {
        pedido.SetStatus(new PedidoEmPreparacao());
    }
}