using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Tests.Domain.Status;

public class PedidoEmPreparacaoTests
{
    [Fact]
    public void Cancelar_StatusEmPreparacao_NaoDeveMudarParaCancelado()
    {
        var pedido = PedidoEmPreparo();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Cancelar());
    }
    
    [Fact]
    public void Confirmar_StatusEmPreparacao_NaoDeveMudarParaConfirmado()
    {
        var pedido = PedidoEmPreparo();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Confirmar());
    }
    
    [Fact]
    public void Receber_StatusEmPreparacao_NaoDeveMudarParaRecebido()
    {
        var pedido = PedidoEmPreparo();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Receber());
    }
    
    [Fact]
    public void Preparar_StatusEmPreparacao_NaoDeveMudarParaEmPreparacao()
    {
        var pedido = PedidoEmPreparo();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Preparar());
    }
    
    [Fact]
    public void MarcarComoPronto_StatusEmPreparacao_DeveMudarParaPronto()
    {   
        var pedido = PedidoEmPreparo();
        
        pedido.MarcarComoPronto();
        
        Assert.IsType<PedidoPronto>(pedido.Status);
    }
    
    [Fact]
    public void Finalizar_StatusEmPreparacao_NaoDeveMudarParaFinalizado()
    {
        var pedido = PedidoEmPreparo();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Finalizar());
    }
    
    private Pedido PedidoEmPreparo()
    {
        Pedido pedido = Pedido.Criar();
        ItemDePedido itemDePedido = ItemDePedido.Criar("Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        pedido.AdicionarItem(itemDePedido);

        return pedido.Confirmar().Receber().Preparar();
    }
}