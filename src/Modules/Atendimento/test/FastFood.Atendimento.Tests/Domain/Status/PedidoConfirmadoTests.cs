using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Atendimento.Tests.Domain.Status;

public class PedidoConfirmadoTests
{
    [Fact]
    public void Cancelar_StatusConfirmado_NaoDeveMudarParaCancelado()
    {
        var pedido = PedidoConfirmado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Cancelar());
    }
    
    [Fact]
    public void Confirmar_StatusConfirmado_NaoDeveMudarParaConfirmado()
    {
        var pedido = PedidoConfirmado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Confirmar());
    }
    
    [Fact]
    public void Receber_StatusConfirmado_DeveMudarParaRecebido()
    {
        var pedido = PedidoConfirmado();
        
        pedido.Receber();
        
        Assert.IsType<PedidoRecebido>(pedido.Status);
    }
    
    [Fact]
    public void Preparar_StatusConfirmado_NaoDeveMudarParaEmPreparacao()
    {
        var pedido = PedidoConfirmado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Preparar());
    }
    
    [Fact]
    public void MarcarComoPronto_StatusConfirmado_NaoDeveMudarParaPronto()
    {
        var pedido = PedidoConfirmado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.MarcarComoPronto());
    }
    
    [Fact]
    public void Finalizar_StatusConfirmado_NaoDeveMudarParaFinalizado()
    {
        var pedido = PedidoConfirmado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Finalizar());
    }
    
    private Pedido PedidoConfirmado()
    {
        Pedido pedido = Pedido.Criar();
        ItemDePedido itemDePedido = ItemDePedido.Criar("Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        pedido.AdicionarItem(itemDePedido);

        return pedido.Confirmar();
    }
}