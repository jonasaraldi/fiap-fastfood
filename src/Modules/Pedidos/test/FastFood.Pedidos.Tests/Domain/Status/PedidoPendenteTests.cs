using FastFood.Contracts.Pedidos;
using FastFood.Pedidos.Domain.Pedidos;
using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Pedidos.Tests.Domain.Status;

public class PedidoPendenteTests
{
    [Fact]
    public void CriarPedido_DeveMudarParaPendente()
    {
        var pedido = PedidoPendente();
        
        Assert.IsType<PedidoPendente>(pedido.Status);
        Assert.IsType<DomainEvents.PedidoCriado>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Confirmar_StatusPendente_DeveMudarParaConfirmado()
    {
        var pedido = PedidoPendenteComItens();

        pedido.Confirmar();
        
        Assert.IsType<PedidoConfirmado>(pedido.Status);
        Assert.IsType<DomainEvents.PedidoConfirmado>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Confirmar_StatusPendente_NaoDeveMudarParaConfirmado_QuandoNaoHouverItens()
    {
        var pedido = PedidoPendente();
        Assert.Throws<PedidoNaoPodeSerConfirmadoSemItensDomainException>(() => pedido.Confirmar());
    }
    
    [Fact]
    public void Cancelar_StatusPendente_DeveMudarParaCancelado()
    {
        var pedido = PedidoPendente();

        pedido.Cancelar();
        
        Assert.IsType<PedidoCancelado>(pedido.Status);
        Assert.IsType<DomainEvents.PedidoCancelado>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Receber_StatusPendente_NaoDeveMudarParaRecebido()
    {
        var pedido = PedidoPendente();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Receber());
    }
    
    [Fact]
    public void Preparar_StatusPendente_NaoDeveMudarParaEmPreparacao()
    {
        var pedido = PedidoPendente();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Preparar());
    }
    
    [Fact]
    public void MarcarComoPronto_StatusPendente_NaoDeveMudarParaPronto()
    {
        var pedido = PedidoPendente();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.MarcarComoPronto());
    }
    
    [Fact]
    public void Finalizar_StatusPendente_NaoDeveMudarParaFinalizado()
    {
        var pedido = PedidoPendente();
        Assert.Throws<TrocaDeStatusInvalidaDomainException>(() => pedido.Finalizar());
    }
    
    private Pedido PedidoPendente() => Pedido.Criar();
    
    private Pedido PedidoPendenteComItens()
    {
        Pedido pedido = Pedido.Criar();
        ItemDePedido itemDePedido = ItemDePedido.Criar("Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        pedido.AdicionarItem(itemDePedido);

        return pedido;
    }
}