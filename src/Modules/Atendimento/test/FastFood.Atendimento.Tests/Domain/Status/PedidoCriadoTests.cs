using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;
using FastFood.Contracts.Pedidos;

namespace FastFood.Atendimento.Tests.Domain.Status;

public class PedidoCriadoTests
{
    [Fact]
    public void CriarPedido_DeveMudarParaCriado()
    {
        var pedido = PedidoCriado();
        
        Assert.IsType<PedidoCriado>(pedido.Status);
        Assert.IsType<DomainEvents.PedidoCriadoDomainEvent>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Confirmar_StatusCriado_DeveMudarParaConfirmado()
    {
        var pedido = PedidoCriadoComItens();

        pedido.Confirmar();
        
        Assert.IsType<PedidoConfirmado>(pedido.Status);
        Assert.IsType<DomainEvents.PedidoConfirmadoDomainEvent>(pedido.GetDomainEvents().LastOrDefault());
    }
    
    [Fact]
    public void Confirmar_StatusCriado_NaoDeveMudarParaConfirmado_QuandoNaoHouverItens()
    {
        var pedido = PedidoCriado();
        Assert.Throws<PedidoNaoPodeSerConfirmadoSemItensDomainException>(() => pedido.Confirmar());
    }
    
    [Fact]
    public void Cancelar_StatusCriado_DeveMudarParaCancelado()
    {
        var pedido = PedidoCriado();

        pedido.Cancelar();
        
        Assert.IsType<PedidoCancelado>(pedido.Status);
        Assert.IsType<DomainEvents.PedidoCanceladoDomainEvent>(pedido.GetDomainEvents().LastOrDefault());
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
    
    private Pedido PedidoCriadoComItens()
    {
        Pedido pedido = Pedido.Criar();
        ItemDePedido itemDePedido = ItemDePedido.Criar("Coca-cola", "Bebida", Dinheiro.Criar(5), 1);
        pedido.AdicionarItem(itemDePedido);

        return pedido;
    }
}