using FastFood.Pedidos.Domain.Pedidos.Entities;
using FastFood.Pedidos.Domain.Pedidos.Exceptions;
using FastFood.Pedidos.Domain.Pedidos.ValueObjects;

namespace FastFood.Pedidos.Tests.Domain;

public class ItemDePedidoTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void CriarItemDePedido_NaoDeveCriarItem_QuandoQuantidadeForNegativaOuZero(int quantidade)
    {
        Assert.Throws<ItemDePedidoDeveTerQuantidadeMaiorQueZeroDomainException>(() =>
            ItemDePedido.Criar("Produto 1", "Produto 1", Dinheiro.Criar(10), quantidade));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void CriarItemDePedido_NaoDeveCriarItem_QuandoPrecoForNegativoOuZero(decimal preco)
    {
        Assert.Throws<ItemDePedidoDeveTerPrecoMaiorQueZeroDomainException>(() =>
            ItemDePedido.Criar("Produto 1", "Produto 1", Dinheiro.Criar(preco), 1));
    }
}