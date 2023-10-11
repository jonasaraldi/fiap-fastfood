using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects.Status;

namespace FastFood.Atendimento.Tests.Domain;

public class PedidoTests
{
    private const string CpfValido = "87995476000";
    
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
        var pedido = CriarPedidoConfirmado(item);

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusRecebido()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoRecebido(item);

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusEmPreparacao()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoEmPreparacao(item);

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusPronto()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoPronto(item);

        Assert.Throws<ItemDePedidoNaoPodeSerAdicionadoEmPedidoComStatusDomainException>(() =>
            pedido.AdicionarItem(item));
    }
    
    [Fact]
    public void AdicionarItem_NaoDeveAdicionarItemAoPedido_QuandoPedidoEstiverComStatusFinalizado()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoFinalizado(item);

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
    public void RemoverItem_NaoDeveRemoverItemDoPedido_QuandoPedidoEstiverComStatusCancelado()
    {
        var pedido = Pedido.Criar().Cancelar();
        var item = CriarItemDePedido();

        Assert.Throws<ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException>(() =>
            pedido.RemoverItem(item.Id));
    }
    
    [Fact]
    public void RemoverItem_NaoDeveRemoverItemDoPedido_QuandoPedidoEstiverComStatusConfirmado()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoConfirmado(item);

        Assert.Throws<ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException>(() =>
            pedido.RemoverItem(item.Id));
    }
    
    [Fact]
    public void RemoverItem_NaoDeveRemoverItemDoPedido_QuandoPedidoEstiverComStatusRecebido()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoRecebido(item);

        Assert.Throws<ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException>(() =>
            pedido.RemoverItem(item.Id));
    }
    
    [Fact]
    public void RemoverItem_NaoDeveRemoverItemDoPedido_QuandoPedidoEstiverComStatusEmPreparacao()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoEmPreparacao(item);

        Assert.Throws<ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException>(() =>
            pedido.RemoverItem(item.Id));
    }
    
    [Fact]
    public void RemoverItem_NaoDeveRemoverItemDoPedido_QuandoPedidoEstiverComStatusPronto()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoPronto(item);
        
        Assert.Throws<ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException>(() =>
            pedido.RemoverItem(item.Id));
    }
    
    [Fact]
    public void RemoverItem_NaoDeveRemoverItemDoPedido_QuandoPedidoEstiverComStatusFinalizado()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoFinalizado(item);

        Assert.Throws<ItemDePedidoNaoPodeSerRemovidoEmPedidoComStatusDomainException>(() =>
            pedido.RemoverItem(item.Id));
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
        var cpf = Cpf.Criar(CpfValido);

        pedido.SetCpf(cpf);

        Assert.Equal(cpf, pedido.Cpf);
    }
    
    [Fact]
    public void SetCpf_NaoDeveDefinirCpf_QuandoPedidoEstiverComStatusCancelado()
    {
        var pedido = Pedido.Criar().Cancelar();
        var cpf = Cpf.Criar(CpfValido);

        Assert.Throws<CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException>(() =>
            pedido.SetCpf(cpf));
    }
    
    [Fact]
    public void SetCpf_NaoDeveDefinirCpf_QuandoPedidoEstiverComStatusConfirmado()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoConfirmado(item);
        var cpf = Cpf.Criar(CpfValido);

        Assert.Throws<CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException>(() =>
            pedido.SetCpf(cpf));
    }
    
    [Fact]
    public void SetCpf_NaoDeveDefinirCpf_QuandoPedidoEstiverComStatusRecebido()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoRecebido(item);
        var cpf = Cpf.Criar(CpfValido);

        Assert.Throws<CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException>(() =>
            pedido.SetCpf(cpf));
    }
    
    [Fact]
    public void SetCpf_NaoDeveDefinirCpf_QuandoPedidoEstiverComStatusEmPreparacao()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoEmPreparacao(item);
        var cpf = Cpf.Criar(CpfValido);

        Assert.Throws<CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException>(() =>
            pedido.SetCpf(cpf));
    }
    
    [Fact]
    public void SetCpf_NaoDeveDefinirCpf_QuandoPedidoEstiverComStatusPronto()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoPronto(item);
        var cpf = Cpf.Criar(CpfValido);

        Assert.Throws<CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException>(() =>
            pedido.SetCpf(cpf));
    }
    
    [Fact]
    public void SetCpf_NaoDeveDefinirCpf_QuandoPedidoEstiverComStatusFinalizado()
    {
        var item = CriarItemDePedido();
        var pedido = CriarPedidoFinalizado(item);
        var cpf = Cpf.Criar(CpfValido);

        Assert.Throws<CpfNaoPodeSerAlteradoEmPedidoComStatusDomainException>(() =>
            pedido.SetCpf(cpf));
    }
    
    [Fact]
    public void CriarPedido_NaoDeveVirComClienteDefinido()
    {
        var pedido = Pedido.Criar();
        Assert.Null(pedido.Cliente);
    }
    
    [Fact]
    public void CriarPedido_NaoDeveVirComCpfDefinido()
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

    private Pedido CriarPedidoConfirmado(ItemDePedido item) =>
        Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar();
    
    private Pedido CriarPedidoRecebido(ItemDePedido item) =>
        Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber();
    
    private Pedido CriarPedidoEmPreparacao(ItemDePedido item) =>
        Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber()
            .Preparar();
    
    private Pedido CriarPedidoPronto(ItemDePedido item) =>
        Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber()
            .Preparar()
            .MarcarComoPronto();
    
    private Pedido CriarPedidoFinalizado(ItemDePedido item) =>
        Pedido.Criar()
            .AdicionarItem(item)
            .Confirmar()
            .Receber()
            .Preparar()
            .MarcarComoPronto()
            .Finalizar();
}