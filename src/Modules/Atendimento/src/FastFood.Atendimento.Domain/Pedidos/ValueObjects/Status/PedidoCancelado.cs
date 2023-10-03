namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoCancelado : StatusDePedido
{
    public PedidoCancelado() : base("Cancelado")
    {
    }
}