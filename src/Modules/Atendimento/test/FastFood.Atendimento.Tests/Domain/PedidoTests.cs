using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Atendimento.Tests.Domain;

public class PedidoTests
{
    [Fact]
    public void AdicionarItem_DeveAdicionarItemAoPedido()
    {
        var pedido = Pedido.Criar();
        var item = CriarItemDePedido();

        pedido.AdicionarItem(item);

        Assert.Contains(item, pedido.Itens);
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoQuantidadeForNegativaOuZero(int quantidade)
    {
        var pedido = Pedido.Criar();
        var item = CriarItemDePedido(quantidade);

        Assert.Throws<ItemDePedidoDeveTerQuantidadeMaiorQueZeroDomainException>(() =>
            pedido.AdicionarItem(item));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPrecoForNegativoOuZero(decimal preco)
    {
        var pedido = Pedido.Criar();
        var item = CriarItemDePedido(preco);

        Assert.Throws<ItemDePedidoDeveTerPrecoMaiorQueZeroDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusCancelado()
    {
        var pedido = Pedido.Criar().Cancelar();
        var item = CriarItemDePedido();

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusConfirmado()
    {
        var item = CriarItemDePedido();
        var pedido = Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar();

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusRecebido()
    {
        var item = CriarItemDePedido();
        var pedido = Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber();

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusEmPreparacao()
    {
        var item = CriarItemDePedido();
        var pedido = Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber()
            .Preparar();

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusPronto()
    {
        var item = CriarItemDePedido();
        var pedido = Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber()
            .Preparar()
            .MarcarComoPronto();

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusFinalizado()
    {
        var item = CriarItemDePedido();
        var pedido = Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber()
            .Preparar()
            .MarcarComoPronto()
            .Finalizar();

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }

    [Fact]
    public void RemoverItem_DeveRemoverItemDoPedido()
    {
        var pedido = Pedido.Criar();
        var item = CriarItemDePedido();
        pedido.AdicionarItem(item);

        pedido.RemoverItem(item.Id);

        Assert.DoesNotContain(item, pedido.Itens);
    }

    [Fact]
    public void SetCliente_DeveDefinirCliente()
    {
        var pedido = Pedido.Criar();
        var cliente = Cliente.Criar("John Doe", Email.Criar("john.doe@test.com"));

        pedido.SetCliente(cliente);

        Assert.Equal(cliente, pedido.Cliente);
    }
    
    [Fact]
    public void SetCpf_DeveDefinirCpf()
    {
        var pedido = Pedido.Criar();
        var cpf = Cpf.Criar("87995476000");

        pedido.SetCpf(cpf);

        Assert.Equal(cpf, pedido.Cpf);
    }
    
    [Fact]
    public void CriarPedido_NaoDeveDefinirCliente()
    {
        var pedido = Pedido.Criar();
        Assert.Null(pedido.Cliente);
    }
    
    [Fact]
    public void CriarPedido_NaoDeveDefinirCpf()
    {
        var pedido = Pedido.Criar();
        Assert.Null(pedido.Cpf);
    }

    private ItemDePedido CriarItemDePedido() => 
        ItemDePedido.Criar("Produto 1", "Produto 1", Dinheiro.Criar(10), 2);
    
    private ItemDePedido CriarItemDePedido(int quantidade) => 
        ItemDePedido.Criar("Produto 1", "Produto 1", Dinheiro.Criar(10), quantidade);
    
    private ItemDePedido CriarItemDePedido(decimal preco) => 
        ItemDePedido.Criar("Produto 1", "Produto 1", Dinheiro.Criar(preco), 1);
}