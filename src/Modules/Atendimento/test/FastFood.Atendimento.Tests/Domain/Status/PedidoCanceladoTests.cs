using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;

namespace FastFood.Atendimento.Tests.Domain.Status;

public class PedidoCanceladoTests
{
    [Fact]
    public void Cancelar_StatusCancelado_NaoDeveMudarParaCancelado()
    {
        var pedido = PedidoCancelado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Cancelar());
    }
    
    [Fact]
    public void Confirmar_StatusCancelado_NaoDeveMudarParaConfirmado()
    {
        var pedido = PedidoCancelado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Confirmar());
    }
    
    [Fact]
    public void Receber_StatusCancelado_NaoDeveMudarParaRecebido()
    {
        var pedido = PedidoCancelado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Receber());
    }
    
    [Fact]
    public void Preparar_StatusCancelado_NaoDeveMudarParaEmPreparacao()
    {
        var pedido = PedidoCancelado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Preparar());
    }
    
    [Fact]
    public void MarcarComoPronto_StatusCancelado_NaoDeveMudarParaPronto()
    {
        var pedido = PedidoCancelado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.MarcarComoPronto());
    }
    
    [Fact]
    public void Finalizar_StatusCancelado_NaoDeveMudarParaFinalizado()
    {
        var pedido = PedidoCancelado();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Finalizar());
    }
    
    private Pedido PedidoCancelado() => Pedido.Criar().Cancelar();
}