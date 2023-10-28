using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Tests.Domain.Status;

public class PedidoRecebidoTests
{
    [Fact]
    public void Cancelar_StatusRecebido_NaoDeveMudarParaCancelado()
    {
        var pedido = PedidoRecebido();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Cancelar());
    }
    
    [Fact]
    public void Confirmar_StatusRecebido_NaoDeveMudarParaConfirmado()
    {
        var pedido = PedidoRecebido();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Confirmar());
    }
    
    [Fact]
    public void Receber_StatusRecebido_NaoDeveMudarParaRecebido()
    {
        var pedido = PedidoRecebido();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Receber());
    }
    
    [Fact]
    public void Preparar_StatusRecebido_DeveMudarParaEmPreparacao()
    {
        var pedido = PedidoRecebido();
        
        pedido.Preparar();
        
        Assert.IsType<PedidoEmPreparacao>(pedido.Status);
    }
    
    [Fact]
    public void MarcarComoPronto_StatusRecebido_NaoDeveMudarParaPronto()
    {
        var pedido = PedidoRecebido();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.MarcarComoPronto());
    }
    
    [Fact]
    public void Finalizar_StatusRecebido_NaoDeveMudarParaFinalizado()
    {
        var pedido = PedidoRecebido();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Finalizar());
    }
    
    private Pedido PedidoRecebido()
    {
        Pedido pedido = Pedido.Criar();
        ItemDePedido itemDePedido = ItemDePedido.Criar("Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        pedido.AdicionarItem(itemDePedido);

        return pedido.Confirmar().Receber();
    }
}