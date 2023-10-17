using FastFood.Atendimento.Domain.Pedidos.Entities;
using FastFood.Atendimento.Domain.Pedidos.Exceptions;
using FastFood.Atendimento.Domain.Pedidos.ValueObjects;

namespace FastFood.Atendimento.Tests.Domain;

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