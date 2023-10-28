using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;

namespace FastFood.Pedidos.Tests.Domain.Status;

public class PedidoFinalizadoTests
{
    [Fact]
    public void Cancelar_StatusFinalizado_NaoDeveMudarParaCancelado()
    {
        var pedido = PedidoFinalizado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Cancelar());
    }
    
    [Fact]
    public void Confirmar_StatusFinalizado_NaoDeveMudarParaConfirmado()
    {
        var pedido = PedidoFinalizado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Confirmar());
    }
    
    [Fact]
    public void Receber_StatusFinalizado_NaoDeveMudarParaRecebido()
    {
        var pedido = PedidoFinalizado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Receber());
    }
    
    [Fact]
    public void Preparar_StatusFinalizado_NaoDeveMudarParaEmPreparacao()
    {
        var pedido = PedidoFinalizado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Preparar());
    }
    
    [Fact]
    public void MarcarComoPronto_StatusFinalizado_NaoDeveMudarParaPronto()
    {   
        var pedido = PedidoFinalizado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.MarcarComoPronto());
    }
    
    [Fact]
    public void Finalizar_StatusFinalizado_NaoDeveMudarParaFinalizado()
    {
        var pedido = PedidoFinalizado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Finalizar());
    }
    
    private Pedido PedidoFinalizado()
    {
        Pedido pedido = Pedido.Criar();
        ItemDePedido itemDePedido = ItemDePedido.Criar("Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        pedido.AdicionarItem(itemDePedido);

        return pedido.Confirmar().Receber().Preparar().MarcarComoPronto().Finalizar();
    }
}