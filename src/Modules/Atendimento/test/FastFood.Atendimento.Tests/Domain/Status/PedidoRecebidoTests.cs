using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Atendimento.Tests.Domain.Status;

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
    
    private Pedido PedidoRecebido() => Pedido.Criar().Confirmar().Receber();
}