using FastFood.Atendimento.Domain.Pedidos;
using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;

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
}