namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoCriado : StatusDePedido
{
    public PedidoCriado() 
        : base(nameof(PedidoCriado), "Criado")
    {
    }
    
    public override void Cancelar(Pedido pedido) =>
        pedido.SetStatus(new PedidoCancelado());

    public override void Confirmar(Pedido pedido) => 
        pedido.SetStatus(new PedidoConfirmado());
}