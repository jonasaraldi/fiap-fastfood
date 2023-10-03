using FastFood.Atendimento.Domain.Pedidos.Exceptions;

namespace FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

public abstract class StatusDePedido
{
    public StatusDePedido(string descricao)
    {
        Descricao = descricao;
    }
    
    public string Descricao { get; private set; }
    public virtual void Cancelar(Pedido pedido) => RetornarErro(pedido, new PedidoCancelado());
    public virtual void Confirmar(Pedido pedido) => RetornarErro(pedido, new PedidoConfirmado());
    public virtual void Receber(Pedido pedido) => RetornarErro(pedido, new PedidoRecebido());
    public virtual void Preparar(Pedido pedido) => RetornarErro(pedido, new PedidoEmPreparacao());
    public virtual void Pronto(Pedido pedido) => RetornarErro(pedido, new PedidoPronto());
    public virtual void Finalizar(Pedido pedido) => RetornarErro(pedido, new PedidoFinalizado());

    private void RetornarErro(Pedido pedido, StatusDePedido statusInformado)
    {
        throw new TrocaDeStatusInvalidaDomainException(pedido.Status, statusInformado);
    }
}