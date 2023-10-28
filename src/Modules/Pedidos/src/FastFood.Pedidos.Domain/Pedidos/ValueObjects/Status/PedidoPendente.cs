using FastFood.Pedidos.Domain.Pedidos.Exceptions;

namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public sealed class PedidoPendente : StatusDePedido
{
    public PedidoPendente() 
        : base(nameof(PedidoPendente), "Pendente")
    {
    }
    
    public override void Cancelar(Pedido pedido) =>
        pedido.SetStatus(new PedidoCancelado());

    public override void Confirmar(Pedido pedido)
    {
        if (!pedido.PossuiItens)
        {
            throw new PedidoNaoPodeSerConfirmadoSemItensDomainException();
        }

        pedido.SetStatus(new PedidoConfirmado());
    }
}