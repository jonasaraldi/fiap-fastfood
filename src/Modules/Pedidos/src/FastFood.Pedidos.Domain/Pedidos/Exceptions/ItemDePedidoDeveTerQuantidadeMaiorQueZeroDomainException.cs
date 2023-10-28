using FastFood.Contracts.Abstractions.Exceptions;

namespace FastFood.Pedidos.Domain.Pedidos.Exceptions;

public class ItemDePedidoDeveTerQuantidadeMaiorQueZeroDomainException : InvalidOperationDomainException
{
    public ItemDePedidoDeveTerQuantidadeMaiorQueZeroDomainException() 
        : base("Item de pedido deve ter quantidade maior que zero.")
    {
    }
}