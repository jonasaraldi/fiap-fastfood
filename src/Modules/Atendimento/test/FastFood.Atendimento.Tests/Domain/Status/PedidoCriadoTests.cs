using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Events;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Atendimento.Tests.Domain.Status;

public class PedidoCriadoTests
{
    [Fact]
    public void CriarPedido_DeveMudarParaCriado()
    {
        var pedido = PedidoCriado();
        
        Assert.IsType<PedidoCriado>(pedido.Status);
        Assert.IsType<PedidoCriadoDomainEvent>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Confirmar_StatusCriado_DeveMudarParaConfirmado()
    {
        var pedido = PedidoCriado();

        pedido.Confirmar();
        
        Assert.IsType<PedidoConfirmado>(pedido.Status);
        Assert.IsType<PedidoConfirmadoDomainEvent>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Cancelar_StatusCriado_DeveMudarParaCancelado()
    {
        var pedido = PedidoCriado();

        pedido.Cancelar();
        
        Assert.IsType<PedidoCancelado>(pedido.Status);
        Assert.IsType<PedidoCanceladoDomainEvent>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Receber_StatusCriado_NaoDeveMudarParaRecebido()
    {
        var pedido = PedidoCriado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Receber());
    }
    
    [Fact]
    public void Preparar_StatusCriado_NaoDeveMudarParaEmPreparacao()
    {
        var pedido = PedidoCriado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Preparar());
    }
    
    [Fact]
    public void MarcarComoPronto_StatusCriado_NaoDeveMudarParaPronto()
    {
        var pedido = PedidoCriado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.MarcarComoPronto());
    }
    
    [Fact]
    public void Finalizar_StatusCriado_NaoDeveMudarParaFinalizado()
    {
        var pedido = PedidoCriado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Finalizar());
    }
    
    private Pedido PedidoCriado() => Pedido.Criar();
}