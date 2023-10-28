using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class ItemDePedidoDeveTerPrecoMaiorQueZeroDomainException : InvalidOperationDomainException
{
    public ItemDePedidoDeveTerPrecoMaiorQueZeroDomainException() 
        : base("Item de pedido deve ter preço maior que zero.")
    {
    }
}