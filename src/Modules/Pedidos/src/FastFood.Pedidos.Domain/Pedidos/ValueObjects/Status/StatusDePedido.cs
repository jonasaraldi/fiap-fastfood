using FastFood.Pedidos.Domain.Pedidos.Exceptions;

namespace FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

public abstract class StatusDePedido
{
    public static StatusDePedido Cancelado = new PedidoCancelado();
    public static StatusDePedido Confirmado = new PedidoConfirmado();
    public static StatusDePedido Recebido = new PedidoRecebido();
    public static StatusDePedido EmPreparacao = new PedidoEmPreparacao();
    public static StatusDePedido Pronto = new PedidoPronto();
    public static StatusDePedido Finalizado = new PedidoFinalizado();
    
    public StatusDePedido(string codigo, string descricao, int ordem)
    {
        Codigo = codigo;
        Descricao = descricao;
        Ordem = ordem;
    }

    public string Codigo { get; private set; }
    public string Descricao { get; private set; }
    public int Ordem { get; private set; }
    public virtual void Cancelar(Pedido pedido) => RetornarErro(pedido, Cancelado);
    public virtual void Confirmar(Pedido pedido) => RetornarErro(pedido, Confirmado);
    public virtual void Receber(Pedido pedido) => RetornarErro(pedido, Recebido);
    public virtual void Preparar(Pedido pedido) => RetornarErro(pedido, EmPreparacao);
    public virtual void MarcarComoPronto(Pedido pedido) => RetornarErro(pedido, Pronto);
    public virtual void Finalizar(Pedido pedido) => RetornarErro(pedido, Finalizado);

    private void RetornarErro(Pedido pedido, StatusDePedido statusInformado) => 
        throw new TrocaDeStatusInvalidaDomainException(pedido.Status, statusInformado);
}